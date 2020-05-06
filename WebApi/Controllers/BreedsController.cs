using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private readonly BreedDbContext _context;

        public BreedsController(BreedDbContext context)
        {
            _context = context;
        }





        // GET all: api/breeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Breed>>> GetBreeds()
        {
            List<Breed> breeds = await _context.Breeds.ToListAsync();

            foreach (var breed in breeds)
            {
                breed.ImageUrl = $"https://{HttpContext.Request.Host}/api/images/{breed.Name.Replace(" ", "")}";
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();
            }

            return breeds;
        }





        // GET by ID: api/breeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Breed>> GetBreed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if (breed == null)
            {
                return NotFound();
            }

            breed.ImageUrl = $"https://{HttpContext.Request.Host}/api/images/{breed.Name.Replace(" ", "")}";
            breed.BreedTraits = _context.Traits.Where(t => t.BreedId == id).FirstOrDefault();

            return breed;
        }





        // GET Random: api/breeds/random
        [HttpGet("random")]
        public async Task<ActionResult<Breed>> GetRandomBreed()
        {
            List<Breed> breeds = await _context.Breeds.ToListAsync();
            Random rnd = new Random();
            int index = rnd.Next(1, breeds.Count + 1);

            var breed = breeds[index];

            breed.ImageUrl = $"https://{HttpContext.Request.Host}/api/images/{breed.Name.Replace(" ", "")}";
            breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();

            return breed;
        }





        // GET by Category: api/breeds/category/toy
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<Breed>>> GetSpecificCategory(string categoryName)
        {
            List<Breed> specificCategory = new List<Breed>();
            List<Breed> allBreeds = await _context.Breeds.ToListAsync();

            foreach (var breed in allBreeds)
            {
                if (breed.Category.ToLower() == categoryName.ToLower())
                {
                    specificCategory.Add(breed);
                }
            }

            foreach (var breed in specificCategory)
            {
                breed.ImageUrl = $"https://{HttpContext.Request.Host}/api/images/{breed.Name.Replace(" ", "")}";
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();
            }

            return specificCategory;
        }





        // GET Friendly Breeds: api/breeds/friendly
        [HttpGet("friendly")]
        public async Task<ActionResult<IEnumerable<Breed>>> GetFriendlyBreeds()
        {
            List<Breed> friendlyBreeds = new List<Breed>();
            List<Breed> allBreeds = await _context.Breeds.ToListAsync();

            foreach (var breed in allBreeds)
            {
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();

                bool isFriendly = (breed.BreedTraits.CatFriendly + breed.BreedTraits.ChildFriendly
                    + breed.BreedTraits.DogFriendly + breed.BreedTraits.StrangerFriendly) >= 16;

                if (isFriendly)
                {
                    friendlyBreeds.Add(breed);
                }
            }

            return friendlyBreeds;
        }





        // GET Watchdog Breeds: api/breeds/protector
        [HttpGet("protector")]
        public async Task<ActionResult<IEnumerable<Breed>>> GetProtectorBreeds()
        {
            List<Breed> protectorBreeds = new List<Breed>();
            List<Breed> allBreeds = await _context.Breeds.ToListAsync();

            foreach (var breed in allBreeds)
            {
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();

                bool isProtector = (breed.BreedTraits.BarkingTendencies + breed.BreedTraits.Territorial
                    + breed.BreedTraits.WatchdogAbility) >= 14;

                if (isProtector)
                {
                    protectorBreeds.Add(breed);
                }
            }

            return protectorBreeds;
        }





        // GET Low-Maitenance Breeds: api/breeds/low-maitenance
        [HttpGet("low-maitenance")]
        public async Task<ActionResult<IEnumerable<Breed>>> GetLowMaitenanceBreeds()
        {
            List<Breed> lowMaitenanceBreeds = new List<Breed>();
            List<Breed> allBreeds = await _context.Breeds.ToListAsync();

            foreach (var breed in allBreeds)
            {
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();

                bool isLowMaitenance = (breed.BreedTraits.Adaptability >= 4) &&
                    ((breed.BreedTraits.ExerciseNeeds + breed.BreedTraits.SocialNeeds +
                    breed.BreedTraits.Grooming + breed.BreedTraits.SheddingLevel + breed.BreedTraits.HealthIssues)) <= 12;

                if (isLowMaitenance)
                {
                    lowMaitenanceBreeds.Add(breed);
                }
            }

            return lowMaitenanceBreeds;
        }





        // GET Inteligent Breeds: api/breeds/intelligent
        [HttpGet("intelligent")]
        public async Task<ActionResult<IEnumerable<Breed>>> GetIntelligentBreeds()
        {
            List<Breed> intelligentBreeds = new List<Breed>();
            List<Breed> allBreeds = await _context.Breeds.ToListAsync();

            foreach (var breed in allBreeds)
            {
                breed.BreedTraits = _context.Traits.Where(t => t.BreedId == breed.Id).FirstOrDefault();

                bool isIntelligent = breed.BreedTraits.Intelligence + breed.BreedTraits.Trainability == 10;

                if (isIntelligent)
                {
                    intelligentBreeds.Add(breed);
                }
            }

            return intelligentBreeds;
        }
    }
}
