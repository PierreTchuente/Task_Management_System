using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Enumeration
{
    public enum EnumRank
    {
        ACE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING

    }

    public enum EnumSuit
    {
        Clubs, Diamonds, Hearts, Spades
    }


    public class UtilityEnum
    {
        /// <summary>
        /// Retun the actuel name rank of the card.
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static string GetCardRank(int rank)
        {
            string rankName = "";
            switch (rank)
            {
                case (int)EnumRank.ACE:
                    rankName = "Ace";
                    break;

                case (int)EnumRank.TWO:
                    rankName = "Two";
                    break;

                case (int)EnumRank.THREE:
                    rankName = "Three";
                    break;

                case (int)EnumRank.FOUR:
                    rankName = "Four";
                    break;

                case (int)EnumRank.FIVE:
                    rankName = "Five";
                    break;

                case (int)EnumRank.SIX:
                    rankName = "Six";
                    break;

                case (int)EnumRank.SEVEN:
                    rankName = "Seven";
                    break;

                case (int)EnumRank.EIGHT:
                    rankName = "Eight";
                    break;

                case (int)EnumRank.NINE:
                    rankName = "Nine";
                    break;

                case (int)EnumRank.TEN:
                    rankName = "Ten";
                    break;

                case (int)EnumRank.JACK:
                    rankName = "Jack";
                    break;

                case (int)EnumRank.QUEEN:
                    rankName = "Queen";
                    break;

                case (int)EnumRank.KING:
                    rankName = "King";
                    break;

                default:
                    rankName = "UNKNOWN";
                    break;
            }
            return rankName;

        }


        /// <summary>
        /// Utility funtion to return the Suit name of a Card.
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        public static string GetCardSuit(int suit)
        {
            string suitName = "";
            switch (suit)
            {
                case (int)EnumSuit.Clubs:
                    suitName = "Clubs";
                    break;

                case (int)EnumSuit.Diamonds:
                    suitName = "Diamonds";
                    break;

                case (int)EnumSuit.Hearts:
                    suitName = "Hearts";
                    break;

                case (int)EnumSuit.Spades:
                    suitName = "Spades";
                    break;

                default:
                    suitName = "UNKNOWN";
                    break;
            }
            return suitName;
        }


        /// <summary>
        /// Utility Method to get the value of each card base on it rank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static int GetCardValue(int rank)
        {
            if (rank == (int)EnumRank.ACE)
            {
                return 11;
            }
            else if (rank == (int)EnumRank.TWO)
            {
                return 2;
            }
            else if (rank == (int)EnumRank.THREE)
            {
                return 3;
            }
            else if (rank == (int)EnumRank.FOUR)
            {
                return 4;
            }
            else if (rank == (int)EnumRank.FIVE)
            {
                return 5;
            }
            else if (rank == (int)EnumRank.SIX)
            {
                return 6;
            }
            else if (rank == (int)EnumRank.SEVEN)
            {
                return 7;
            }
            else if (rank == (int)EnumRank.EIGHT)
            {
                return 8;
            }
            else if (rank == (int)EnumRank.NINE)
            {
                return 9;
            }
            else if (rank == (int)EnumRank.TEN)
            {
                return 10;
            }
            else if (rank == (int)EnumRank.JACK)
            {
                return 10;
            }
            else if (rank == (int)EnumRank.QUEEN)
            {
                return 10;
            }
            else if (rank == (int)EnumRank.KING)
            {
                return 10;
            }
            return -1; // if we get this far it means wrong rank has been provided.
        }
    }

}
