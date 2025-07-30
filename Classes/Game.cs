using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Game
    {
        public int ID { get; set; } // Unique identifier for the game class
        // Core Game Properties
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Boxart { get; set; }
        // Collection-Specific Properties
        public DateTime CollectedDate { get; set; }
        public bool CiB { get; set; } // Complete in Box
        public string Condition { get; set; } // Condition of the game
        public string Region { get; set; } // Region of the game
        public int NumberOfCopies { get; set; } // Number of copies owned
        // Game-Specific Properties
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool IsBeaten { get; set; } // Whether the game has been beaten
        public bool Is100Completed { get; set; } // Whether the game has been 100% completed
        // Foreign keys for relationships
        public int PublisherID { get; set; } // Foreign key to Publisher
        public int? SeriesID { get; set; } // Foreign key to Series, nullable as a game may not belong to a series
        public int ProfileID { get; set; } // Foreign key to Profile

        public Game()
        {
            CollectedDate = DateTime.Now; // Default to current date
            NumberOfCopies = 1; // Default to one copy
            IsBeaten = false; // Default to not beaten
            Is100Completed = false; // Default to not 100% completed
            CiB = false; // Default to not complete in box
        }

        /*
         * Method: Validation
         * Currently validates the Name property.
         * More complex validation logic can be added later as needed.
         */
        public bool Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                System.Console.WriteLine("Validation error: Name cannot be empty.");
                return false;
            }
            return true;
        }

        /*
         * Method: ToString
         * Returns a string representation of the game object.
         * This can be used for debugging or logging purposes.
         */
        public string ToString()
        {
            return $"{Name} (ID:{ID})";
        }
    }
}
