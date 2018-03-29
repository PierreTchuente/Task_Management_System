using BlackJackGame.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Model
{
    public class Hand
    {
        private List<Card> ListofCards = null;
        private int cardsToatalValue = 0;
        private int AmountBet = 0;
        private int insuranceBet = 0;

        /// <summary>
        /// Create a new hand for the player with an empty list of Card.
        /// </summary>
        public Hand()
        {
            ListofCards = new List<Card>();  // a hand is considered to have two cards. List capacity is set to 2. 2 in the future can be a setting value
                                              // depending of the house policy.
        }

        /// <summary>
        /// Get or Set the hand insurance bet.
        /// </summary>
        public int InsuranceBet
        {
            get { return insuranceBet; }
            set { insuranceBet = value; }
        }

        /// <summary>
        /// Get or set the Money bet by the Player for this hand
        /// 
        /// </summary>
        public int Bet
        {
            get { return AmountBet; }
            set { AmountBet = value; }
        }


        /// <summary>
        /// Get all cards in the hand
        /// </summary>
        public List<Card> GetListOfCard()
        {
            return ListofCards;
        }

        public int GetTotalCard ()
        {
            return ListofCards.Count;
        }

        /// <summary>
        /// add one more card in the Hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToHand(Card card)
        {
            ListofCards.Add(card);
        }

        public Card RemoveHand(int index)
        {
            try
            {
                Card card = new Card(ListofCards[index].Rank, ListofCards[index].Suit, ListofCards[index].ValueCount);
                ListofCards.RemoveAt(index);
                return card;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException(ex.Message);
            }
        }

        /// <summary>
        /// Get the total hand value.
        /// </summary>
        /// <returns></returns>
        public int GetHandTotalValue()
        {
            if (cardsToatalValue > 0)
            {
                return cardsToatalValue;
            }
            else
            {
                foreach (var card in ListofCards)
                {
                    cardsToatalValue += card.ValueCount;
                }
                return cardsToatalValue;
            }
        }

        /// <summary>
        /// Determine if the Current hand is a blacjack.
        /// </summary>
        /// <returns></returns>
        public bool IsBlacJack()
        {
            bool isblackjack = false;
            foreach (var card in ListofCards)
            {
                if ((card.Rank == (int)EnumRank.ACE && card.ValueCount == 11) || (card.Rank == (int)EnumRank.TEN && card.ValueCount == 10))
                {
                    isblackjack = true;
                }
                else
                {
                    isblackjack = false;
                    break;
                }

            }
            return isblackjack;
        }

    }
}
