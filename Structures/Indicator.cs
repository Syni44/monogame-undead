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
        public Vector2 Position { get; private set; } = new Vector2();

        public Indicator(Side side, int rowOrColumnIndex) {
            SideOfBoard = side;
            Index = rowOrColumnIndex;
        }

        /// <summary>
        /// Draws Indicator default text based on its position relative to the rows and columns of the board. Drawing once
        /// is necessary to set the proper position of the Indicator.
        /// </summary>
        internal void Draw(SpriteBatch sb, SpriteFont font, Board gameBoard, int cellSize) {
            // TODO: the math in this function are really just hardcoded to look centered with certain gameBoard cellSize and
            // spritefont size settings; they don't actually calculate center of row/column!

            // TODO: move indicator Position-getting logic out of this drawing method

            float nudgeAmount = cellSize / 2;

            switch (SideOfBoard) {
                case Side.Left:
                    Position = gameBoard.Cells.Where(e => e.Coordinate.X == 0 && e.Coordinate.Y == Index)
                        .FirstOrDefault().Position + new Vector2(-nudgeAmount, cellSize / 3);
                    break;
                case Side.Top:
                    Position = gameBoard.Cells.Where(e => e.Coordinate.X == Index && e.Coordinate.Y == 0)
                        .FirstOrDefault().Position + new Vector2((cellSize / 3), -nudgeAmount);
                    break;
                case Side.Right:
                    Position = gameBoard.Cells.Where(e => e.Coordinate.X == gameBoard.Width - 1 && e.Coordinate.Y == Index)
                        .FirstOrDefault().Position + new Vector2(cellSize + (cellSize / 6), cellSize / 3);
                    break;
                case Side.Bottom:
                    Position = gameBoard.Cells.Where(e => e.Coordinate.X == Index && e.Coordinate.Y == gameBoard.Height - 1)
                        .FirstOrDefault().Position + new Vector2((cellSize / 3), cellSize + (cellSize / 6));
                    break;
                default:
                    throw new Exception("Couldn't draw Indicator init text! are you sure you initialized Indicator.Size correctly?");
            }

            sb.DrawString(font, "hi", Position, Color.White);
        }

        internal void DrawIndicatorText(SpriteBatch sb, SpriteFont font, List<Cell> cells) {
            // TODO: draw indicator position logic

            //Vector2 pos = new Vector2();
            //pos = cells.Where(e => e.Coordinate.X == 0 && e.Coordinate.Y == Index).Select(e => e.Position).FirstOrDefault() - Vector2.One;

            //sb.DrawString(font, "hi", pos, Color.White);
        }
    }
}
