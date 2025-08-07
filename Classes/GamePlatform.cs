using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    // This is the GamePlatform class, which represents the relationship between a game and a gaming console.
    public class GamePlatform
    {
        public int GamePlatformID { get; set; } // Unique identifier for the game console relationship
        public int GameID { get; set; } // Foreign key to the game
        public int GameConsoleID { get; set; } // Foreign key to the platform
        public GamePlatform() 
        {
            // Default constructor
        }
    }
}
