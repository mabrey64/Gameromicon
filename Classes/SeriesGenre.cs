using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class SeriesGenre
    {
        public int SeriesGenreID { get; set; } // Unique identifier for the series genre relationship
        public int SeriesID { get; set; } // Foreign key to the series
        public int GenreID { get; set; } // Foreign key to the genre
        public SeriesGenre() 
        {
            // Default constructor
        }
    }
}
