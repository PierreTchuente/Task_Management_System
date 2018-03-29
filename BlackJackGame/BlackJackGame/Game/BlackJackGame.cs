using BlackJackGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Game
{
    public class BlackJackGame
    {
        private List<Player> PlayerList = null;
        private Delear TheDelear = null;

        public void StartGame(string dealerName, Hand delearHand)
        {
            PlayerList = new List<Player>(GameRules.MAXIMUM_NUMBER_OF_PAYERS);
            TheDelear = new Delear(dealerName, delearHand);
        }

        public void AddPlayerToGame(List<Player> players)
        {
            if (players.Count <= GameRules.MAXIMUM_NUMBER_OF_PAYERS)
            {
                PlayerList.AddRange(players);
            }
        }

        public void ProceedWithPlay()
        {

        }

        public void EndOfGame()
        {
            TheDelear.Payout();
        }

    }
}
