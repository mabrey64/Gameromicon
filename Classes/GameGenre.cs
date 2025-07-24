using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class GameGenre
    {
        public int GameGenreID { get; set; } // Unique identifier for the game genre
        public int GameID { get; set; } // Foreign key to the game
        public int GenreID { get; set; } // Foreign key to the genre
        public GameGenre()
        {
            // Default constructor
        }
    }
}
