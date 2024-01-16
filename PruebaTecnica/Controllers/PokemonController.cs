using Application.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonQueryService _pokemonQueryService;
        public PokemonController(IPokemonQueryService pokemonQueryService)
        {
            _pokemonQueryService = pokemonQueryService;
        }

        [HttpGet("Brahm")]
        public async Task<ActionResult<List<PokemonInfoModel>>> Get()
        {
            try
            {
                List<PokemonInfoModel> result = await _pokemonQueryService.GetPokemons();
                return result;
            }
            catch (Exception e)
            {
                var ex = e.InnerException ?? new Exception(e.Message);
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            string BseUrl = "https://pokeapi.co/api/v2/";
            HttpClient HtpCli = new ();
            HtpCli.BaseAddress = new Uri(BseUrl);
            List<string> tps = new List<string> {
                "Fire", "Electric"
            };

            List<PokemonInfoModel> ap = new List<PokemonInfoModel>();
            foreach (var t in tps)
            {
                try
                {
                    string aUrl = $"{BseUrl}type/{t.ToLower()}/";
                    HttpResponseMessage rsp = HtpCli.GetAsync(aUrl).Result;
                    rsp.EnsureSuccessStatusCode();
                    string aRsp = rsp.Content.ReadAsStringAsync().Result;
                    var ptRes = JsonConvert.DeserializeObject<PokemonTypeResultModel>(aRsp);
                    foreach (var p in ptRes.Pokemon)
                    {
                        string pUrl = p.Pokemon.Url;
                        HttpResponseMessage prsp = HtpCli.GetAsync(pUrl).Result;
                        prsp.EnsureSuccessStatusCode();
                        string prspRsp = prsp.Content.ReadAsStringAsync().Result;
                        var pInfo = JsonConvert.DeserializeObject<PokemonInfoModel>(prspRsp);
                        ap.Add(pInfo);
                    }
                }
                catch (Exception)
                {               
                    Console.WriteLine($"¡Err al btr ls Pkmn dl tpo '{t}'!");
                }
            }
            return Ok(JsonConvert.SerializeObject(ap));
        }
    }
    
}
