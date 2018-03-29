using BlackJackGame.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Model
{
    /// <summary>
    /// Represent a Card object in the game.
    /// </summary>
    public class Card
    {
        private string name;
        private int rank;
        private int suit;
        private int valueCount;

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="valuecount"></param>
        public Card(int rank, int suit, int valuecount)
        {
            this.name = UtilityEnum.GetCardRank(rank) + " of " + UtilityEnum.GetCardSuit(suit);
            this.rank = rank;
            this.suit = suit;
            this.valueCount = valuecount;
        }

        /// <summary>
        /// Parameter less constructor
        /// </summary>
        public Card()
        {
            this.name = "unknown";
            this.rank = -1;
            this.suit = -1;
            this.valueCount = 0;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// get and set the rank of the card
        /// </summary>
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        /// <summary>
        /// get and set the suit of the card
        /// </summary>
        public int Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        /// <summary>
        /// get and set the value of the card.
        /// eg: the natural  card 2 has it face value
        /// </summary>
        public int ValueCount
        {
            get { return valueCount; }
            set { valueCount = value; }
        }
    }
}
