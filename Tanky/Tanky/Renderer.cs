using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    static class Renderer
    {
        public static void Draw(GameState state)
        {
            
            state.Map.DrawBase();

            
            foreach (var b in state.Bullets.ToList())
            {
                if (!state.Map.InBounds(b.Pos.X, b.Pos.Y)) continue;
                try
                {
                    Console.SetCursorPosition(b.Pos.X, b.Pos.Y);
                    Console.Write(Glyphs.Bullet);
                }
                catch {  }
            }

            
            foreach (var e in state.Enemies.Where(x => x.Alive))
            {
                if (!state.Map.InBounds(e.Pos.X, e.Pos.Y)) continue;
                try
                {
                    Console.SetCursorPosition(e.Pos.X, e.Pos.Y);
                    Console.Write(Glyphs.Enemy);
                }
                catch { }
            }

            
            if (state.Player != null && state.Player.Alive && state.Map.InBounds(state.Player.Pos.X, state.Player.Pos.Y))
            {
                try
                {
                    Console.SetCursorPosition(state.Player.Pos.X, state.Player.Pos.Y);
                    Console.Write(Glyphs.Player);
                }
                catch { }
            }

            
            int hudY = state.Map.Height + 1;
            try
            {
                Console.SetCursorPosition(0, hudY);
                string hud = $"Level: {state.Level}  Enemies: {state.Enemies.Count(e => e.Alive)}  PlayerHP: {(state.Player?.HP ?? 0)}   (Arrows to move, Space to shoot)";
                if (hud.Length < state.Map.Width) hud = hud.PadRight(state.Map.Width);
                Console.Write(hud);

                
                Console.SetCursorPosition(0, hudY + 1);
                Console.Write(new string(' ', Math.Min(state.Map.Width, Console.BufferWidth)));
            }
            catch { }
        }
    }
}
