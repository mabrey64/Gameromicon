using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Genre ()
        {
            // Default constructor
        }

        /*
         * Method: ToString
         * Returns a string representation of the genre object.
         * This can be used for debugging or logging purposes.
         */
        public override string ToString()
        {
            return $"{ID} {Name}";
        }
    }
}
