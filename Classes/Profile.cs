using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class Profile
    {
        public int ID { get; set; } // Unique identifier for the profile
        public string Name { get; set; } // Name of the profile
        public string Email { get; set; } // Email associated with the profile
        public string? Username { get; set; } // Username for the profile, nullable if not set
        public string PasswordHash { get; set; } // Password for the profile, stored as a hash for security (and to test how it works)
        public Profile()
        {
            // Initializing default values
        }

        /*
         * Method: ToString
         * Returns a string representation of the profile object.
         * This can be used for debugging or logging purposes.
         */
        public override string ToString()
        {
            return $"{Name} {Email}";
        }
    }
}
