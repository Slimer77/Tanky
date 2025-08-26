using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class Cell
    {
        public char Symbol;
        public int Durability;

        public bool IsWalkable => Symbol == Glyphs.Empty || Symbol == Glyphs.Water;
        public bool BulletsPass => Symbol == Glyphs.Empty || Symbol == Glyphs.Water;
        public bool IsWallDestructible => Symbol == Glyphs.WallDestructible;
        public bool IsWallIndestructible => Symbol == Glyphs.WallIndestructible;

        public Cell(char symbol)
        {
            Symbol = symbol;
            Durability = symbol == Glyphs.WallDestructible ? 3 :
                         (symbol == Glyphs.WallIndestructible ? int.MaxValue : 0);
        }
    }
}
