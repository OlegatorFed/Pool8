using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool8
{
    class Logic
    {
        public Player currentPlayer;
        public Player otherPlayer;
        private Ruler ruler;
        private Field field;

        public Logic(Ruler ruler, Field field)
        {
            field.OnAllBallsStoped += tookTurn;
            this.ruler = ruler;
            this.field = field;
        }

        public event Action<Player> OnWin;
        public event Action<Player> OnTurn;
        public event Action<Player> OnTurnPass;
        public event Action<Player> OnOneMore;

        public void tookTurn()
        {
            if ( ruler.IsPlayerWinner(currentPlayer, field.remainBalls) )
            {
                OnWin(currentPlayer);
            }
            else if ( field.scoredBalls.Any((ballPattern) => ballPattern == Pattern.black ) )
            {
                OnWin(otherPlayer);
            }
            else
            {
                MadeTurn();
            }
        }

        private void MadeTurn()
        {
            OnTurn(currentPlayer);
            if (!ruler.OneMore(currentPlayer, field.scoredBalls))
            {
                OnTurnPass(otherPlayer);
                (currentPlayer, otherPlayer) = (otherPlayer, currentPlayer);
            }
            else
            {
                OnOneMore(currentPlayer);
            }
        }
    }
}
