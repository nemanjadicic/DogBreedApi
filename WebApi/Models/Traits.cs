using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Traits
    {
        public int Id { get; set; }
        public int BreedId { get; set; }
        public Breed Breed { get; set; }


        //  1st row
        public int Adaptability { get; set; }
        public int DogFriendly { get; set; }
        public int SheddingLevel { get; set; }


        //  2nd row
        public int AffectionLevel { get; set; }
        public int ExerciseNeeds { get; set; }
        public int SocialNeeds { get; set; }


        //  3rd row
        public int AppartmentFriendly { get; set; }
        public int Grooming { get; set; }
        public int StrangerFriendly { get; set; }


        //  4th row
        public int BarkingTendencies { get; set; }
        public int HealthIssues { get; set; }
        public int Territorial { get; set; }


        //  5th row
        public int CatFriendly { get; set; }
        public int Intelligence { get; set; }
        public int Trainability { get; set; }


        //  6th row
        public int ChildFriendly { get; set; }
        public int Playfulness { get; set; }
        public int WatchdogAbility { get; set; }
    }
}
