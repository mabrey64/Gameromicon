using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameromicon.Classes
{
    public class GameConsole
    {
        public int GameConsoleID { get; set; } // Unique identifier for the game console relationship
        public int GameID { get; set; } // Foreign key to the game
        public int ConsoleID { get; set; } // Foreign key to the console
        public GameConsole() 
        {
            // Default constructor
        }
    }
}
