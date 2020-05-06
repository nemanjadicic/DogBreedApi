using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Logic
{
    public static class FolderLocator
    {
        public static string ImagesFolderLocation(string currentPath = null)
        {
            DirectoryInfo directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            string imagesLocation = directory.ToString() + @"\WebApi\wwwroot\images\";

            return imagesLocation;
        }
    }
}
