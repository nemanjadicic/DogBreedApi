# Dog Breed API
**Dog Breed API** is a service that provides usable data about all 200+ existing dog breeds. This repository, within the same Visual Studio Solution, also contains a Web Scrapper that "borrows" the data from the [source website](http://www.vetstreet.com/dogs) that is being used for seeding.

### Key Features:
* Simple .NET Core Console application that ensures the Database is created on first use and "scrapes" every dog breed data from the [source website](http://www.vetstreet.com/dogs), also downloading images for each individual dog breed in the process.
* ASP .NET Core Web API service that seeds the data that the database had been populated with. It supports multiple endpoints and, by default, returns data in Json format, except for the dog breed image.
* A simple ASP .NET Core Blazor Server-Side website that acts as a presentation for the Web API service, as well as the Web Scrapper project.

## Getting Started
To be able to use this app you'll need:
* [Visual Studio 2019 Community Edition.](https://visualstudio.microsoft.com/downloads/) The app hasn't been tested on older VS versions.
* [Sql Server Data Tools for Visual Studio 2019](https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-2017)

### How to use
1. Clone the repository to your computer and open DogBreedApi solution in Visual Studio.
2. For first use, make sure to run the WebScrapper project before the WebApi project. This will ensure that DogBreedDB database is created and populated with data on your local machine. <u>You can do this by navigating the "WebScrapper" project in the VS Solution, right clicking on it and choosing "Set as Startup Project" from the dropdown Menu</u>.
3. Press F5 to start the WebScrapper.
  * Mind that the Web Scrapper needs around 3-4 minutes to gather data for every 200+ dog breeds covered in the [source website](http://www.vetstreet.com/dogs). However, you can stop this process at any time. It is advised to let the Scrapper gather data for at least 20 dog breeds, just so you would have every kind of dog category stored in the database. <u>You can stop the Web Scrapper by clicking on the VS "Stop Debugging" button</u>.
4. After your local database is populated with data, navigate and switch to "WebApi" project by right clicking and choosing Set as Startup Project" once again.
5. You're good to go! Press F5 to run the website and tryout some of the endpoints provided.
  * You can use the [Postman](https://www.postman.com/downloads/) app to experiment with different endpoints.
