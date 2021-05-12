using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Undead_040220.Structures
{
    class Legend
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        bool initLegendDrawn = false;
        public int ZombieStock { get; private set; }
        public int VampireStock { get; private set; }
        public int GhostStock { get; private set; }
        public int StockSize { get; private set; }
        public int StockSpacing { get; private set; }
        public Texture2D ZombieTexture { get; private set; }
        public Texture2D VampireTexture { get; private set; }
        public Texture2D GhostTexture { get; private set; }
        private Rectangle _zombieRect;
        private Rectangle _vampireRect;
        private Rectangle _ghostRect;
        public List<LegendMonster> LegendMonsters { get; private set; }

        public Legend(int height, int cellSize, int cellSpacing) {
            Height = height;
            Width = (cellSize * 3) + (cellSpacing * 2);
            StockSpacing = cellSpacing;
            StockSize = cellSize;
            LegendMonsters = new List<LegendMonster>();
        }

        /// <summary>
        /// Checked every in-game tick for mouse and keyboard activity in the legend.
        /// </summary>
        /// <param name="mouse"></param>
        /// <param name="keyboard"></param>
        public void Update(MouseState mouse, KeyboardState keyboard) {
            // initiate LegendMonster objects only after everything is drawn and calculated, and only once
            if (LegendMonsters.Count == 0 && initLegendDrawn) {
                LegendMonsters = new List<LegendMonster>()
                {
                    new LegendMonster("Zombie", ZombieTexture, _zombieRect, ZombieStock),
                    new LegendMonster("Vampire", VampireTexture, _vampireRect, VampireStock),
                    new LegendMonster("Ghost", GhostTexture, _ghostRect, GhostStock)
                };

                Debug.WriteLine($"our monsters: {LegendMonsters[0].MonsterName}, {LegendMonsters[1].MonsterName}, {LegendMonsters[2].MonsterName}");
                Debug.WriteLine($"textures?: {LegendMonsters[0].Texture}, {LegendMonsters[1].Texture}, {LegendMonsters[2].Texture}");
                Debug.WriteLine($"rects?: {LegendMonsters[0].Rect}, {LegendMonsters[1].Rect}, {LegendMonsters[2].Rect}");
                Debug.WriteLine($"stocks?: {LegendMonsters[0].Stock}, {LegendMonsters[1].Stock}, {LegendMonsters[2].Stock}");
            }

            if (initLegendDrawn) {
                for (int i = 0; i < LegendMonsters.Count; i++) {
                    LegendMonsters[i].Update(mouse, keyboard);
                }
            }
        }

        public void Draw(SpriteBatch sb, Texture2D z_t, Texture2D v_t, Texture2D g_t, Rectangle r) {
            Point _zombiePos = new Point(r.X, r.Y);
            Point _vampirePos = new Point(r.X + StockSize + StockSpacing, r.Y);
            Point _ghostPos = new Point(r.X + (StockSize * 2) + (StockSpacing * 2), r.Y);

            // initializing these into the class, and yet we continue to use those passed-in for the rest of this method?
            ZombieTexture = z_t;
            VampireTexture = v_t;
            GhostTexture = g_t;

            _zombieRect = new Rectangle(_zombiePos, new Point(StockSize, StockSize));
            _vampireRect = new Rectangle(_vampirePos, new Point(StockSize, StockSize));
            _ghostRect = new Rectangle(_ghostPos, new Point(StockSize, StockSize));

            // zombie
            sb.Draw( // Texture2D, Rectangle, Color
                    z_t,
                    _zombieRect,
                    Color.White
                );

            // vampire
            sb.Draw( // Texture2D, Rectangle, Color
                    v_t,
                    _vampireRect,
                    Color.White
                );

            // ghost
            sb.Draw( // Texture2D, Rectangle, Color
                    g_t,
                    _ghostRect,
                    Color.White
                );
    }

        /// <summary>
        /// Draws the most up-to-date counts of each monster available to use.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        public void DrawStocks(SpriteBatch sb, SpriteFont font, string z_count, string v_count, string g_count) {
            ZombieStock = Convert.ToInt32(z_count);
            VampireStock = Convert.ToInt32(v_count);
            GhostStock = Convert.ToInt32(g_count);

            int nudgeAmount = StockSize / 2;

            // zombie
            sb.DrawString(font, z_count, new Vector2(_zombieRect.X + (StockSize / 2.5f), _zombieRect.Y - nudgeAmount), Color.Aquamarine);

            // vampire
            sb.DrawString(font, v_count, new Vector2(_vampireRect.X + (StockSize / 2.5f), _vampireRect.Y - nudgeAmount), Color.Aquamarine);

            // ghost
            sb.DrawString(font, g_count, new Vector2(_ghostRect.X + (StockSize / 2.5f), _ghostRect.Y - nudgeAmount), Color.Aquamarine);

            initLegendDrawn = true;
        }
    }
}
