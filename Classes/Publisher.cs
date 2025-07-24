using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Publisher
    {
        public int ID { get; set; } // Unique identifier for the publisher
        public string Name { get; set; } // Name of the publisher
        public Publisher()
        {
            // Default constructor
        }

        /*
         * Method: ToString
         * Returns a string representation of the publisher object.
         * This can be used for debugging or logging purposes.
         */
        public override string ToString()
        {
            return $"Publisher ID: {ID}, Name: {Name}";
        }
    }
}
