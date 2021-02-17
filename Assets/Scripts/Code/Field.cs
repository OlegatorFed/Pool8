using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool8
{
    class Field
    {
        public event Action OnAllBallsStoped;

        public List<Pattern> remainBalls { get; set; }
        public List<Pattern> scoredBalls { get; set; }

        internal void stopAllBalls()
        {
            OnAllBallsStoped();
        }
    }
}
