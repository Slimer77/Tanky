using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class PlayerTank : Tank
    {
        private int moveIntervalMs = 150;
        private int lastActionMs = 0;
        public PlayerTank(int x, int y, GameState state) : base(x, y, state)
        {
            HP = 3;
            Dir = Direction.Up;
        }

        public override void Update(int nowMs)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                int nx = Pos.X, ny = Pos.Y;
                switch (key)
                {
                    case ConsoleKey.UpArrow: Dir = Direction.Up; ny--; break;
                    case ConsoleKey.DownArrow: Dir = Direction.Down; ny++; break;
                    case ConsoleKey.LeftArrow: Dir = Direction.Left; nx--; break;
                    case ConsoleKey.RightArrow: Dir = Direction.Right; nx++; break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        Shoot();
                        break;
                }
                if (nowMs - lastActionMs > moveIntervalMs)
                {
                    if (nx != Pos.X || ny != Pos.Y)
                    {
                        if (CanMoveTo(nx, ny)) Pos = new Vec(nx, ny);
                    }
                    lastActionMs = nowMs;
                }
            }
        }

        protected override void OnDestroyed()
        {
            State.PlayerDead = true;
        }
    }
}
