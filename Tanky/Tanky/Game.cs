using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanky
{
    class Game
    {
        GameState state = new GameState();
        int tickDelayMs = 80;
        int nowMs => Environment.TickCount & Int32.MaxValue;

        public void Run()
        {
            Console.Clear();
            Console.Title = "Tanks - Level 1 (Console)";
            state.SpawnPlayerAtDefault();
            state.SpawnEnemies(3);

            while (true)
            {
                var start = nowMs;
                state.Player?.Update(nowMs);
                foreach (var e in state.Enemies.Where(x => x.Alive).ToList())
                    e.Update(nowMs);

                
                for (int i = state.Bullets.Count - 1; i >= 0; i--)
                {
                    var b = state.Bullets[i];
                    if (!b.Alive) { state.Bullets.RemoveAt(i); continue; }
                    b.Move();

                    if (!state.Map.InBounds(b.Pos.X, b.Pos.Y)) { state.Bullets.RemoveAt(i); continue; }

                    var t = state.GetTankAt(b.Pos.X, b.Pos.Y);
                    if (t != null)
                    {
                        t.TakeDamage();
                        state.Bullets.RemoveAt(i);
                        continue;
                    }

                    if (state.Map.ApplyBulletHit(b.Pos.X, b.Pos.Y))
                    {
                        state.Bullets.RemoveAt(i);
                        continue;
                    }
                }

                state.Enemies.RemoveAll(e => !e.Alive);

                Renderer.Draw(state);

                if (state.PlayerDead)
                {
                    Console.SetCursorPosition(0, state.Map.Height + 2);
                    Console.WriteLine("Ты проиграл. Нажми R чтобы рестартовать или Q для выхода.");
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.R)
                    {
                        state = new GameState();
                        state.SpawnPlayerAtDefault();
                        state.SpawnEnemies(3);
                        Console.Clear();
                        continue;
                    }
                    else break;
                }
                if (state.Enemies.Count == 0)
                {
                    state.Level++;
                    int newEnemies = Math.Min(8, 3 + state.Level);
                    state.SpawnEnemies(newEnemies);
                    if (state.Player == null || !state.Player.Alive) state.SpawnPlayerAtDefault();
                    Console.SetCursorPosition(0, state.Map.Height + 2);
                    Console.WriteLine($"Переход на уровень {state.Level}. Нажми любую клавишу...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }

                int elapsed = nowMs - start;
                int wait = tickDelayMs - elapsed;
                if (wait > 0) Thread.Sleep(wait);
            }

            Console.SetCursorPosition(0, state.Map.Height + 3);
            Console.WriteLine("Выход. Спасибо за игру.");
        }
    }
}
