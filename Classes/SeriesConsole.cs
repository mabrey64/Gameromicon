using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class SeriesConsole
    {
        public int SeriesConsoleID { get; set; } // Unique identifier for the series console relationship
        public int SeriesID { get; set; } // Foreign key to the series
        public int ConsoleID { get; set; } // Foreign key to the console

        public SeriesConsole() 
        {
            // Default constructor
        }
    }
}
