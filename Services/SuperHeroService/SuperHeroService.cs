using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly DatabaseContext _context;
        public SuperHeroService(DatabaseContext context)
        {
            _context = context; 
        }
        async Task<List<SuperHero>> ISuperHeroService.GetAllHeros()
        {
            var heros = await _context.SuperHeroes.ToListAsync();
            return (heros); 
        }
        async Task<List<SuperHero>> ISuperHeroService.AddHero(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            //superHeros.Add(superHero);
            return await _context.SuperHeroes.ToListAsync();
        }



        async Task<SuperHero> ISuperHeroService.GetHero(int id)
        {
            SuperHero superHero = await _context.SuperHeroes.FindAsync(id);
            //SuperHero superHero = superHeros.Find(x => x.Id == id);
            if (superHero == null)
                return null;
            else
                return superHero;
        }

        async Task<SuperHero> ISuperHeroService.UpdateHero(int id, SuperHero request)
        {
            SuperHero superHero = await _context.SuperHeroes.FindAsync(id);
            //SuperHero superHero = superHeros.Find(x => x.Id == id);
            if (superHero == null) return null;

            superHero.Name = request.Name;
            superHero.FirstName = request?.FirstName;
            superHero.LastName = request?.LastName;
            superHero.Place = request?.Place;
            await _context.SaveChangesAsync();
            return (superHero);
        }
    }
}
