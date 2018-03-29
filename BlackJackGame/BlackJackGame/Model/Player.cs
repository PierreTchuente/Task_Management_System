using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Model
{
    public class Player : GeneralPlayer
    {
        private int money;

        /// <summary>
        /// Get Set the player's money
        /// </summary>
        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public Player(string name, List<Hand> handsList, int money) : base(name, handsList)
        {
            this.money = money;
        }

        /// <summary>
        /// Player place a bet for a specific hand.
        /// </summary>
        public bool PlaceBet(Hand hand, int amountBet)
        {
            bool isbetPlace = false;
            if (money >= amountBet)
            {
                hand.Bet = amountBet;
                isbetPlace = true;
            }
            else
            {
                isbetPlace = false;
            }
            return isbetPlace;
        }

        /// <summary>
        /// Place an Assurance bet on a specific hands. insurance bet is half the initial
        /// bet of the hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public bool PlaceInsuranceBet(ref Hand hand)
        {
            bool isbetPlace = false;
            if (money >= hand.Bet / 2)
            {
                hand.Bet = hand.Bet / 2;
                isbetPlace = true;
            }
            else
            {
                isbetPlace = false;
            }
            return isbetPlace;
        }

        /// <summary>
        /// Assert if the Player can Stand for this hand.
        /// the total hand must be less or equal to 21 in order for
        /// the player to stand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public bool Stand(Hand hand)
        {
            if (hand.GetHandTotalValue() <= 21)
                return true;
            else
                return false;
        }

        /// <summary>
        /// add one more card at a time to the player hand.
        /// if the hand total value is less than 21.
        /// </summary>
        /// <param name="hand"></param>
        public void Hit(ref Hand hand, Card newCard = null)
        {
            if ((hand.GetHandTotalValue() < 21) && (newCard != null))
            {
                hand.AddCardToHand(newCard);
            }
            else if (hand.GetHandTotalValue() == 21)
            {
                Stand(hand); // automatically Stand for this Hit.
            }
            else if (hand.GetHandTotalValue() > 21) // it is a bust. player immedialy looses the hand bet.
            {
                hand.Bet = 0;
            }
        }

        /// <summary>
        /// Increase the hand's bet by the initial amount bet and 
        /// the player is given one more card
        /// </summary>
        /// <param name="hand"></param>
        public void DoubleDown(ref Hand hand, Card newCard)
        {
            hand.Bet += hand.Bet;
            hand.AddCardToHand(newCard);
            Stand(hand); // after a double down the player is force to stand.
        }

        /// <summary>
        /// Split the player hand if the two cards present in hand
        /// have the same rank or the same value count.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public int[] Split(ref Hand hand)
        {
            int currentPositionInList = this.hands.IndexOf(hand);
            int[] listOfSplitHandPosition = new int[2];
            listOfSplitHandPosition[0] = currentPositionInList;
            List<Card> cards = hand.GetListOfCard();

            if ((cards[0].Rank == cards[1].Rank) || (cards[0].ValueCount == cards[1].ValueCount))
            {
                Card removedCard = hand.RemoveHand(1);
                Hand newHand = new Hand();
                newHand.AddCardToHand(removedCard);
                newHand.AddCardToHand(new Card()); // adding a new instance of a card for later deal
                newHand.Bet = hand.Bet; // place a bet for the new hand equal to the origal bet.
                hand.AddCardToHand(new Card());
                if (currentPositionInList < this.hands.Count)
                {
                    this.hands.Insert(currentPositionInList + 1, newHand);
                }
                else if (currentPositionInList == this.hands.Count)
                {
                    this.hands.Add(newHand);
                }
                listOfSplitHandPosition[1] = currentPositionInList + 1;
            }
            return listOfSplitHandPosition;
        }

        /// <summary>
        /// When player take back half of their bet and giving up their hand.
        /// </summary>
        /// <param name="hand"></param>
        public void Surrender(Hand hand)
        {
            this.money += hand.Bet / 2;
            hand.Bet = 0; 
        }

    }
}
