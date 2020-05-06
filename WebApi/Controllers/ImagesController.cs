using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebScrapper.Logic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly BreedDbContext _context;

        public ImagesController(BreedDbContext context)
        {
            _context = context;
        }



        // GET: api/images/dalmatian
        [HttpGet("{name}")]
        public ActionResult GetImage(string name)
        {
            var breed = _context.Breeds.Where(b => b.Name.Replace(" ", "") == name).FirstOrDefault();
            var imagePath = System.IO.File.OpenRead($"{FolderLocator.ImagesFolderLocation()}{breed.Name.Replace(" ", "")}.jpg");

            return File(imagePath, "image/jpeg");
        }
    }
}