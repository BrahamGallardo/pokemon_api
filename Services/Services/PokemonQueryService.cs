using Application.Common;
using Application.Interfaces;
using Dominio.Models;
using Newtonsoft.Json;

namespace Application.Services
{
    public class PokemonQueryService : IPokemonQueryService
    {
        private readonly IPokemonQueryRepository _pokemonQueryRepository;
        public PokemonQueryService(IPokemonQueryRepository pokemonQueryRepository)
        {
            _pokemonQueryRepository = pokemonQueryRepository;
        }
        public async Task<List<PokemonInfoModel>> GetPokemons()
        {
            List<Types> types = Lk_Types.Types.ToList();
            List<PokemonInfoModel> ap = new ();

            foreach (var t in types)
            {
                PokemonTypeResultModel pokemonTypeResultModel = await _pokemonQueryRepository.GetPokemonTypeModel(t.Name);
                foreach (var item in pokemonTypeResultModel.Pokemon)
                {
                    PokemonInfoModel pokemonInfoModel = await _pokemonQueryRepository.GetPokemonInfoModel(item);
                    ap.Add(pokemonInfoModel);
                }
            }
            return ap;
        }
    }
}
