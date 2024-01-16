using Dominio.Models;

namespace Application.Interfaces
{
    public interface IPokemonQueryRepository
    {
        Task<PokemonInfoModel> GetPokemonInfoModel(PokemonTypePokemonModel pokemonTypePokemonModel);
        Task<PokemonTypeResultModel> GetPokemonTypeModel(string type);
    }
}
