using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Undead_040220.Structures
{
    class Legend
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ZombieStock { get; private set; }
        public int VampireStock { get; private set; }
        public int GhostStock { get; private set; }
        public int StockSize { get; private set; }
        public int StockSpacing { get; private set; }
        private Point _zombiePos;
        private Point _vampirePos;
        private Point _ghostPos;

        public Legend(int height, int cellSize, int cellSpacing) {
            Height = height;
            Width = (cellSize * 3) + (cellSpacing * 2);
            StockSpacing = cellSpacing;
            StockSize = cellSize;
        }

        public void Draw(SpriteBatch sb, Texture2D z_t, Texture2D v_t, Texture2D g_t, Rectangle r) {
            _zombiePos = new Point(r.X, r.Y);
            _vampirePos = new Point(r.X + StockSize + StockSpacing, r.Y);
            _ghostPos = new Point(r.X + (StockSize * 2) + (StockSpacing * 2), r.Y);

            // zombie
            sb.Draw( // Texture2D, Rectangle, Color
                    z_t,
                    new Rectangle(_zombiePos, new Point(StockSize, StockSize)),
                    Color.White
                );

            // vampire
            sb.Draw( // Texture2D, Rectangle, Color
                    v_t,
                    new Rectangle(_vampirePos, new Point(StockSize, StockSize)),
                    Color.White
                );

            // ghost
            sb.Draw( // Texture2D, Rectangle, Color
                    g_t,
                    new Rectangle(_ghostPos, new Point(StockSize, StockSize)),
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
            int nudgeAmount = StockSize / 2;

            // zombie
            sb.DrawString(font, z_count, new Vector2(_zombiePos.X + (StockSize / 2.5f), _zombiePos.Y - nudgeAmount), Color.Aquamarine);

            // vampire
            sb.DrawString(font, v_count, new Vector2(_vampirePos.X + (StockSize / 2.5f), _vampirePos.Y - nudgeAmount), Color.Aquamarine);

            // ghost
            sb.DrawString(font, g_count, new Vector2(_ghostPos.X + (StockSize / 2.5f), _ghostPos.Y - nudgeAmount), Color.Aquamarine);
        }
    }
}
