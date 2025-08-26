using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class GameState
    {
        public GameMap Map;
        public PlayerTank Player;
        public List<Tank> Enemies = new List<Tank>();
        public List<Bullet> Bullets = new List<Bullet>();
        public bool PlayerDead = false;
        public int Level = 1;

        public GameState()
        {
            Map = new GameMap();
        }

        public bool IsTankAt(int x, int y)
        {
            if (Player != null && Player.Alive && Player.Pos.X == x && Player.Pos.Y == y) return true;
            return Enemies.Any(e => e.Alive && e.Pos.X == x && e.Pos.Y == y);
        }

        public Tank GetTankAt(int x, int y)
        {
            if (Player != null && Player.Alive && Player.Pos.X == x && Player.Pos.Y == y) return Player;
            return Enemies.FirstOrDefault(e => e.Alive && e.Pos.X == x && e.Pos.Y == y);
        }

        public void SpawnPlayerAtDefault()
        {
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    if (Map.CanWalk(x, y))
                    {
                        Player = new PlayerTank(x, y, this);
                        return;
                    }
                }
            }
        }

        public void SpawnEnemies(int count)
        {
            Enemies.Clear();
            var rnd = new System.Random(123 + Level);
            int attempts = 500;
            while (Enemies.Count < count && attempts-- > 0)
            {
                int x = rnd.Next(Map.Width), y = rnd.Next(Map.Height);
                if (!Map.CanWalk(x, y)) continue;
                if (IsTankAt(x, y)) continue;
                var e = new EnemyTank(x, y, this);
                Enemies.Add(e);
            }
        }
    }
}
