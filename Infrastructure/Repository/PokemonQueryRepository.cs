using Application.Interfaces;
using Dominio.Models;
using Newtonsoft.Json;

namespace Infrastructure.Repository
{
    public class PokemonQueryRepository : IPokemonQueryRepository
    {
        string BseUrl = "https://pokeapi.co/api/v2/";
        public HttpClient GetHttpClient()
        {
            HttpClient HtpCli = new()
            {
                BaseAddress = new Uri(BseUrl)
            };
            return HtpCli;
        }

        public async Task<string> GetHttpResponseMessageAsync(HttpClient HtpCli, string url)
        {
            HttpResponseMessage prsp = await HtpCli.GetAsync(url);
            prsp.EnsureSuccessStatusCode();
            string prspRsp = await prsp.Content.ReadAsStringAsync();
            return prspRsp;
        }

        public async Task<PokemonInfoModel> GetPokemonInfoModel(PokemonTypePokemonModel pokemonTypePokemonModel)
        {
            HttpClient HtpCli = GetHttpClient();
            string pUrl = pokemonTypePokemonModel.Pokemon.Url;
            var res = await GetHttpResponseMessageAsync(HtpCli, pUrl);
            var pInfo = JsonConvert.DeserializeObject<PokemonInfoModel>(res) ?? throw new Exception("");
            return pInfo;
        }

        public async Task<PokemonTypeResultModel> GetPokemonTypeModel(string type)
        {
            HttpClient HtpCli = GetHttpClient();
            string aUrl = $"{BseUrl}type/{type.ToLower()}/";
            string aRsp = await GetHttpResponseMessageAsync(HtpCli, aUrl);
            var ptRes = JsonConvert.DeserializeObject<PokemonTypeResultModel>(aRsp) ?? throw new Exception("");
            return ptRes;
        }
    }
}
