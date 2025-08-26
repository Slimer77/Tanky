using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class Bullet
    {
        public Vec Pos;              // поз пули
        public Direction Dir;        // направление движения
        public bool FromPlayer;      // true = пуля игрока, false = пуля врага
        public bool Alive = true;    // "жива" ли пуля

        public Bullet(int x, int y, Direction dir, bool fromPlayer)
        {
            Pos = new Vec(x, y);
            Dir = dir;
            FromPlayer = fromPlayer;
        }

        
        public void Move()
        {
            switch (Dir)
            {
                case Direction.Up: Pos.Y--; break;
                case Direction.Down: Pos.Y++; break;
                case Direction.Left: Pos.X--; break;
                case Direction.Right: Pos.X++; break;
            }
        }
    }
}
