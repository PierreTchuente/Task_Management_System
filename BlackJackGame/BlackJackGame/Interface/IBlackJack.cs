using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Interface
{
    public interface IBlackJack
    {
        /// <summary>
        /// Procedure to pay out winners.
        /// </summary>
        /// <returns></returns>
       int Payout();
    }
}
