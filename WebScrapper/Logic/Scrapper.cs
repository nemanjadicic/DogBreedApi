using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebScrapper.Data;
using WebScrapper.Models;

namespace WebScrapper.Logic
{
    public class Scrapper
    {
        private static Breed currentBreed = new Breed();
        private static Traits currentTraits = new Traits();
        private static BreedDbContext context = new BreedDbContext();



        private static async Task<List<string>> GetBreedLinks()
        {
            List<string> breedLinks = new List<string>();
            string baseUrl = "http://www.vetstreet.com";



            HttpClient client = new HttpClient();
            HtmlDocument pageDocument = new HtmlDocument();

            string html = await client.GetStringAsync("http://www.vetstreet.com/dogs/breeds#all-breeds");
            pageDocument.LoadHtml(html);

            var breedListHtml = pageDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("id", "").Equals("breed-links")).ToList();

            var unsortedLists = breedListHtml[0].Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "").Contains("filtered_list")).ToList();



            foreach (var list in unsortedLists)
            {
                var links = list.Descendants("li").ToList();

                foreach (var link in links)
                {
                    string suffix = link.Descendants("a")
                    .FirstOrDefault().GetAttributeValue("href", "");

                    breedLinks.Add(baseUrl + suffix);
                }
            }

            return breedLinks;
        }



        public static async void ScrapeDogInfo()
        {
            List<string> links = GetBreedLinks().GetAwaiter().GetResult();

            foreach (string link in links)
            {
                HttpClient client = new HttpClient();
                HtmlDocument pageDocument = new HtmlDocument();

                string html = await client.GetStringAsync(link);
                pageDocument.LoadHtml(html);

                // html for dog profile
                var breedDetailHtml = pageDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("breed-detail")).ToList();
                // html for dog traits
                var breedTraitsHtml = pageDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("breed-characteristics")).ToList();
                // html for dog image
                var breedImageHtml = pageDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("id", "").Equals("current-breed-list")).ToList();



                // create Breed object
                currentBreed = new Breed();



                //                      POPULATE THE BREED PROFILE
                // name
                string name = Regex.Replace(breedDetailHtml[0].Descendants("h1").FirstOrDefault().InnerText, @"\s", "");
                name = Regex.Replace(name, "[A-Z]", " $0").Trim();
                name = Regex.Replace(name, "-", " $0").Trim();
                currentBreed.Name = name;

                // get necessary html and put it in a list
                var innerBreedDetailHtml = pageDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("id", "").Equals("breed-detail")).ToList();
                var traitList = innerBreedDetailHtml[0].Descendants("li").ToList();

                // category
                currentBreed.Category = traitList[0].Descendants("a").FirstOrDefault().InnerText;

                // height
                currentBreed.Height = traitList[1].InnerText.Replace("Height:&nbsp;", "");

                // weight
                currentBreed.Weight = traitList[2].InnerText.Replace("Weight:&nbsp;", "");

                // life span
                currentBreed.LifeSpan = traitList[3].InnerText.Replace("Life Span:&nbsp;", "");

                // summary
                currentBreed.Summary = innerBreedDetailHtml[0].Descendants("p").FirstOrDefault().InnerText;

                // download image
                string imageUrl = breedImageHtml[0].Descendants("img").FirstOrDefault().GetAttributeValue("src", "");
                DownloadDogImage(imageUrl, currentBreed.Name);



                // save breed object to the DB
                context.Breeds.Add(currentBreed);
                context.SaveChangesAsync().GetAwaiter().GetResult();



                //                      POPULATE THE BREED TRAITS
                // get necessary html and put it in a list
                var tableRows = breedTraitsHtml[0].Descendants("tr").ToList();

                List<int> ratings = new List<int>();

                foreach (var row in tableRows)
                {
                    var ratingCells = row.Descendants("td")
                        .Where(node => node.GetAttributeValue("class", "").Contains("rating")).ToList();

                    foreach (var cell in ratingCells)
                    {
                        ratings.Add(int.Parse(Regex.Match(cell.InnerText, @"\d").Value));
                    }
                }

                // create Traits object
                currentTraits = new Traits();

                // assign values from ratings list to the Traits properties
                var propertyList = currentTraits.GetType().GetProperties().ToList();
                propertyList.RemoveAt(0);
                propertyList.RemoveAt(0);
                propertyList.RemoveAt(0);

                for (int index = 0; index < ratings.Count; index++)
                {
                    propertyList[index].SetValue(currentTraits, ratings[index]);
                }

                currentTraits.BreedId = currentBreed.Id;
                currentTraits.Breed = currentBreed;

                // save Traits object to the DB
                context.Add(currentTraits);
                context.SaveChangesAsync().GetAwaiter().GetResult();

                // display progress message
                Console.WriteLine($"Scrapping data for {currentBreed.Name}...");
            }

            Console.WriteLine();
            Console.WriteLine("DONE!!! Press any key to exit.");
        }



        private static void DownloadDogImage(string url, string dogName)
        {
            using (WebClient client = new WebClient())
            {
                Uri uriAddress = new Uri(url);

                string fullFilePath = $@"{FolderLocator.ImagesFolderLocation()}{dogName.Replace(" ", "")}.jpg";

                if (!File.Exists(fullFilePath))
                {
                    client.DownloadFileAsync(uriAddress, fullFilePath);
                }
            }
        }
    }
}
