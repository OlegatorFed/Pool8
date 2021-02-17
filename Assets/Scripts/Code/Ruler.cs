using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool8
{
    public enum Player
    {
        red,
        blu,
        Makoto,
        Joker
    }
    public enum Pattern
    {
        striped,
        solid,
        white,
        black
    }

    class Ruler
    {
        
        public Player firstPlayer;

        public Pattern firstPattern = Pattern.solid;
        public Pattern secondPattern = Pattern.striped;

        public bool OneMore(Player player, List<Pattern> scoredBalls)
        {
            Pattern targetPattern = player == firstPlayer ? firstPattern : secondPattern;

            return scoredBalls.All((Pattern ballPattern) => ballPattern == targetPattern);
        }

        public bool IsPlayerWinner(Player player, List<Pattern> remainBalls)
        {
            Pattern targetPattern = player == firstPlayer ? firstPattern : secondPattern;

            bool isBlack = remainBalls.All((Pattern ballPattern) => ballPattern != Pattern.black);
            bool noTargets = remainBalls.All((Pattern ballPattern) => ballPattern != targetPattern);

            return isBlack && noTargets;
        }
    }
}
