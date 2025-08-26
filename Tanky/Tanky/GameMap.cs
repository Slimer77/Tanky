using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class GameMap
    {
        public int Width { get; }
        public int Height { get; }
        private Cell[,] cells;

        private static readonly string[] mapData = new[]
        {
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓                            ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓                            ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓████    ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓████    ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓            ▓▓▓▓            ▓▓▓▓            ▓▓▓▓",
          "▓▓▓▓            ▓▓▓▓            ▓▓▓▓            ▓▓▓▓",
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓████    ████████████▓▓▓▓▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓████    ████████████▓▓▓▓▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓            ▓▓▓▓            ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓            ▓▓▓▓            ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓████    ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓████    ▓▓▓▓    ▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓                    ▓▓▓▓            ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓                    ▓▓▓▓            ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓                            ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓    ▓▓▓▓                            ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓",
          "▓▓▓▓                    ▓▓▓▓                    ▓▓▓▓",
          "▓▓▓▓                    ▓▓▓▓                    ▓▓▓▓",
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
          "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"
        };

        public GameMap()
        {
            Height = mapData.Length;
            Width = mapData[0].Length;
            cells = new Cell[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                var row = mapData[y];
                for (int x = 0; x < Width; x++)
                {
                    char ch = x < row.Length ? row[x] : Glyphs.Empty;
                    if (ch == Glyphs.WallDestructible || ch == Glyphs.WallIndestructible || ch == Glyphs.Water)
                        cells[x, y] = new Cell(ch);
                    else
                        cells[x, y] = new Cell(Glyphs.Empty);
                }
            }
        }

        public Cell Get(int x, int y) => InBounds(x, y) ? cells[x, y] : new Cell(Glyphs.WallIndestructible);
        public bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
        public bool CanWalk(int x, int y) => InBounds(x, y) && Get(x, y).IsWalkable;
        public bool BulletsCanPass(int x, int y) => InBounds(x, y) && Get(x, y).BulletsPass;

        public bool ApplyBulletHit(int x, int y)
        {
            if (!InBounds(x, y)) return true;
            var c = Get(x, y);
            if (c.IsWallIndestructible) return true;
            if (c.IsWallDestructible)
            {
                c.Durability--;
                if (c.Durability <= 0)
                    cells[x, y] = new Cell(Glyphs.Empty);
                else
                    cells[x, y].Durability = c.Durability;
                return true;
            }
            return false;
        }

        public void DrawBase()
        {
            Console.CursorVisible = false;
            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < Width; x++)
                {
                    var c = Get(x, y);
                    if (c.IsWallDestructible) Console.Write(Glyphs.WallDestructible);
                    else if (c.IsWallIndestructible) Console.Write(Glyphs.WallIndestructible);
                    else if (c.Symbol == Glyphs.Water) Console.Write(Glyphs.Water);
                    else Console.Write(Glyphs.Empty);
                }
            }
        }
    }
}
