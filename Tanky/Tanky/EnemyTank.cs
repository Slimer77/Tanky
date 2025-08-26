using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class EnemyTank : Tank
    {
        
        private static readonly Random rnd = new Random();
        private int moveIntervalMs = 400;
        private int lastDecisionMs = 0;

        public EnemyTank(int x, int y, GameState state) : base(x, y, state)
        {
            HP = 1;
            Dir = Direction.Down;
        }

        public override void Update(int nowMs)
        {
            if (nowMs - lastDecisionMs > moveIntervalMs)
            {
                lastDecisionMs = nowMs;
                var p = State.Player;
                if (p != null && p.Alive)
                {
                    
                    if (p.Pos.X == Pos.X && !IsWallBetween(Pos.X, Pos.Y, p.Pos.X, p.Pos.Y))
                    {
                        Dir = p.Pos.Y < Pos.Y ? Direction.Up : Direction.Down;
                        Shoot();
                        return;
                    }

                    
                    if (p.Pos.Y == Pos.Y && !IsWallBetween(Pos.X, Pos.Y, p.Pos.X, p.Pos.Y))
                    {
                        Dir = p.Pos.X < Pos.X ? Direction.Left : Direction.Right;
                        Shoot();
                        return;
                    }
                }

                
                var attempts = 4;
                while (attempts-- > 0)
                {
                    var d = rnd.Next(4);
                    Direction nd = (Direction)d;
                    int nx = Pos.X, ny = Pos.Y;
                    switch (nd)
                    {
                        case Direction.Up: ny--; break;
                        case Direction.Down: ny++; break;
                        case Direction.Left: nx--; break;
                        case Direction.Right: nx++; break;
                    }
                    Dir = nd;
                    if (CanMoveTo(nx, ny))
                    {
                        Pos = new Vec(nx, ny);
                        break;
                    }
                }
            }
        }

        
        private bool IsWallBetween(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                int start = Math.Min(y1, y2) + 1;
                int end = Math.Max(y1, y2) - 1;
                for (int y = start; y <= end; y++)
                    if (!State.Map.BulletsCanPass(x1, y)) return true;
                return false;
            }
            else if (y1 == y2)
            {
                int start = Math.Min(x1, x2) + 1;
                int end = Math.Max(x1, x2) - 1;
                for (int x = start; x <= end; x++)
                    if (!State.Map.BulletsCanPass(x, y1)) return true;
                return false;
            }            
            return true;
        }
    }
}
