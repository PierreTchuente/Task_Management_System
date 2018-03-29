using BlackJackGame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Model
{
    public class Delear : GeneralPlayer, IBlackJack
    {
        public Delear(string name, Hand hand) : base(name, hand)
        {

        }

        /// <summary>
        /// Delear reveal the face down card of their hand.
        /// </summary>
        public void Reveal()
        {

        }

        /// <summary>
        /// Procedure to pay out winner of a Game turn.
        /// </summary>
        /// <returns></returns>
        public int Payout()
        {
            int amountPaid = 0;
            return amountPaid;
        }
    }
}
