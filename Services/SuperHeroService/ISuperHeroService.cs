using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        Task<List<SuperHero>> GetAllHeros();

        Task<SuperHero> GetHero(int id);

        Task<List<SuperHero>?> AddHero([FromBody] SuperHero superHero);

        Task<SuperHero?> UpdateHero(int id, SuperHero request);
    }
    
}
