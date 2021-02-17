using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool8
{
    class Program
    {
        static void Main(string[] args)
        {
            Ruler ruler = new Ruler();
            Field field = new Field();
            Logic logic = new Logic(ruler, field);

            ruler.firstPlayer = Player.Makoto;

            logic.OnWin += (Player currentPlayer) => Console.WriteLine($"{currentPlayer} is winner");
            logic.OnTurn += (Player currentPlayer) => Console.WriteLine($"{currentPlayer} made their turn");
            logic.OnTurnPass += (Player otherPlayer) => Console.WriteLine($"{otherPlayer} gets turned on");
            logic.OnOneMore += (Player currentPlayer) => Console.WriteLine($"One More! for {currentPlayer}");

            logic.currentPlayer = Player.Joker;
            logic.otherPlayer = Player.Makoto;

            //red gets turned on
            field.scoredBalls = new List<Pattern> { Pattern.white };
            field.remainBalls = new List<Pattern> { Pattern.striped };
            field.stopAllBalls();

            //red
            field.scoredBalls = new List<Pattern> { Pattern.black };
            field.remainBalls = new List<Pattern> { Pattern.solid };
            field.stopAllBalls();

            //red
            field.scoredBalls = new List<Pattern> { Pattern.solid };
            field.remainBalls = new List<Pattern> { Pattern.solid };
            field.stopAllBalls();




            Console.ReadKey();
        }

        internal static void Winner(Player currentPlayer)
        {
            Console.WriteLine(currentPlayer);
        }
    }
}
