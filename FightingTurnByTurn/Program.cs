using System;

namespace FightingTurnByTurn
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();

            while (!game.HasEnded)
            {
                game.Turn();
            }
        }
    }
}
