using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Series
    {
        public int ID { get; set; } // Unique identifier for the series
        public string Name { get; set; } // Name of the series
        public int PublisherID { get; set; } // Foreign key to Publisher
        public int ProfileID { get; set; } // Foreign key to Profile

        public Series()
        {

        }

        /*
         * Method: Validate
         * Validates the Series object.
         * Currently checks if the Name property is not empty.
         */
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                System.Console.WriteLine("Validation error: Name cannot be empty.");
                return false;
            }
            return true;
        }

        /*
         * Method: ToString
         * Returns a string representation of the series object.
         * This can be used for debugging or logging purposes.
         */

        public override string ToString()
        {
            return $"Series ID: {ID}, Name: {Name}, Publisher ID: {PublisherID}, Profile ID: {ProfileID}";

        }
    }
}
