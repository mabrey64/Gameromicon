using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Console
    {
        public int ID { get; set; } // Unique identifier for the console
        public string Name { get; set; } // Name of the console
        public Console()
        {
            // Default constructor
        }
        /*
         * Method: ToString
         * Returns a string representation of the console object.
         * This can be used for debugging or logging purposes.
         */
        public override string ToString()
        {
            return $"{ID} {Name}";
        }
    }
}
