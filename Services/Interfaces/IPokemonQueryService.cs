using Dominio.Models;

namespace Application.Interfaces
{
    public interface IPokemonQueryService
    {
        Task<List<PokemonInfoModel>> GetPokemons();
    }
}
