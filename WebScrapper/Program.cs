using System;
using WebScrapper.Data;
using WebScrapper.Logic;

namespace WebScrapper
{
    class Program
    {
        private static BreedDbContext context = new BreedDbContext();

        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            Scrapper.ScrapeDogInfo();
            Console.ReadKey();
        }
    }
}
