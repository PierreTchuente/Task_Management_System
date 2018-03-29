using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Model
{
    public abstract class GeneralPlayer
    {
        private string name;
        protected List<Hand> hands;

        /// <summary>
        /// Contructor to add many hands at a time
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handsList"></param>
        public GeneralPlayer(string name, List<Hand> handsList)
        {
            this.name = name;
            hands = new List<Hand>();
            hands.AddRange(handsList); // add all element of handsList.
        }

        /// <summary>
        /// Constructor that only add one hand
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hand"></param>
        public GeneralPlayer(string name, Hand hand)
        {
            this.name = name;
            hands = new List<Hand>
            {
                hand
            };
        }

        /// <summary>
        /// Get Set  the General Player Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }     

        /// <summary>
        /// add another hand to the user hand.
        /// </summary>
        /// <param name="hand"></param>
        public void AddHand(Hand hand)
        {
            hands.Add(hand);
        }

        /// <summary>
        /// get player or dealer hand(s)
        /// </summary>
        /// <returns></returns>
        public List<Hand> GetHands()
        {
            return hands;
        }

        /// <summary>
        /// Get the hand at a specif position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public  Hand GetHand(int index)
        {
             return hands.ElementAt(index);
        }

    }
}
