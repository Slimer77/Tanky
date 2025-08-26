using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    abstract class Tank
    {
        public Vec Pos;
        public Direction Dir;
        public int HP;
        protected GameState State;
        public bool Alive => HP > 0;

        public Tank(int x, int y, GameState state)
        {
            Pos = new Vec(x, y);
            State = state;
            HP = 1;
        }

        public virtual void Update(int nowMs) { }

        public bool CanMoveTo(int nx, int ny)
        {
            return State.Map.CanWalk(nx, ny) && !State.IsTankAt(nx, ny);
        }

        public void TakeDamage()
        {
            HP--;
            if (HP <= 0) OnDestroyed();
        }

        protected virtual void OnDestroyed() { }

        public void Shoot()
        {
            var (bx, by) = (Pos.X, Pos.Y);
            switch (Dir)
            {
                case Direction.Up: by--; break;
                case Direction.Down: by++; break;
                case Direction.Left: bx--; break;
                case Direction.Right: bx++; break;
            }

            if (!State.Map.InBounds(bx, by)) return;
            var b = new Bullet(bx, by, Dir, this is PlayerTank);
            State.Bullets.Add(b);
        }
    }
}
