using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.SuperHeroService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var result = await _superHeroService.GetAllHeros();
            return Ok(result);
        }

        [HttpGet("{id}"),Authorize]        
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var result = await _superHeroService.GetHero(id);
            if (result == null) return NotFound("Hero Not Found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero([FromBody]SuperHero superHero)
        {
            var result = await _superHeroService.AddHero(superHero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero request)
        {
            var result = await _superHeroService.UpdateHero(id, request);
            if (result == null) return NotFound("Wrong Entry");
            return Ok(result);
        }

    }

}
