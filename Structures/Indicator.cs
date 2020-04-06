using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Undead_040220.Structures
{
    class Indicator
    {
        public enum Side
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public Side SideOfBoard { get; private set; }
        public int Index { get; private set; }

        public Indicator(Side side, int rowOrColumnIndex) {
            SideOfBoard = side;
            Index = rowOrColumnIndex;
        }

        internal void DrawIndicatorText(SpriteBatch sb, SpriteFont font, List<Cell> cells) {
            // TODO: draw indicator position logic

            //Vector2 pos = new Vector2();
            //pos = cells.Where(e => e.Coordinate.X == 0 && e.Coordinate.Y == Index).Select(e => e.Position).FirstOrDefault() - Vector2.One;

            //sb.DrawString(font, "hi", pos, Color.White);
        }
    }
}
