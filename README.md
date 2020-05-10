# Dog Breed API
**Dog Breed API** is a service that provides usable data about all 200+ existing dog breeds. This repository, within the same Visual Studio Solution, also contains a Web Scrapper that "borrows" the data from the [source website](http://www.vetstreet.com/dogs) that is being used for seeding.

### Key Features:
* Simple .NET Core Console application that ensures the Database is created on first use and "scrapes" every dog breed data from the source website](http://www.vetstreet.com/dogs), also downloading images for each individual dog breed.
* ASP .NET Core Web API service that seeds the data that the database had been populated with. It supports multiple endpoints and, by default, returns data in Json format, except for the breed image.
* A simple ASP .NET Core Blazor Server-Side WebSite that acts as a presentation for the Web API service, as well as the Web Scrapper project.
