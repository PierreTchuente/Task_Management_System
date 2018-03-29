using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Game
{
    public class GameRules
    {
        /// <summary>
        /// Set or Get the maximum number of card per hand for a specific game.
        /// default value is 2.
        /// </summary>
        public static int MAXIMUM_CARD_PERHAND { get; set; }  = 2;
        /// <summary>
        /// Set or get the maximum number of players for a game 
        /// </summary>
        public static int MAXIMUM_NUMBER_OF_PAYERS { get; set; } = 4;
        public static int MAXIMUM_HANDS { get; set; } = 4;

    }
}
