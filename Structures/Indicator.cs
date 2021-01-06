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
        public Cell AttachedCell { get; private set; }

        public Indicator(Side side, int rowOrColumnIndex, Board board, int cellSize) {
            SideOfBoard = side;
            Index = rowOrColumnIndex;

            float nudgeAmount = cellSize / 2;

            // stores the adjacent cell and sets position on screen for the indicator
            // TODO: the math in this function are really just hardcoded to look centered with certain gameBoard cellSize (96) and
            // spritefont size (16) settings; they don't actually calculate center of row/column!

            switch (SideOfBoard) {
                case Side.Left:
                    AttachedCell = board.Cells.Where(e => e.Coordinate.X == 0 && e.Coordinate.Y == Index).FirstOrDefault();
                    Position = AttachedCell.Position + new Vector2(-nudgeAmount, cellSize / 3);
                    break;
                case Side.Top:
                    AttachedCell = board.Cells.Where(e => e.Coordinate.X == Index && e.Coordinate.Y == 0).FirstOrDefault();
                    Position = AttachedCell.Position + new Vector2((cellSize / 3), -nudgeAmount);
                    break;
                case Side.Right:
                    AttachedCell = board.Cells.Where(e => e.Coordinate.X == board.Width - 1 && e.Coordinate.Y == Index).FirstOrDefault();
                    Position = AttachedCell.Position + new Vector2(cellSize + (cellSize / 6), cellSize / 3);
                    break;
                case Side.Bottom:
                    AttachedCell = board.Cells.Where(e => e.Coordinate.X == Index && e.Coordinate.Y == board.Height - 1).FirstOrDefault();
                    Position = AttachedCell.Position + new Vector2((cellSize / 3), cellSize + (cellSize / 6));
                    break;
                default:
                    throw new Exception("Couldn't set Indicator position! are you sure you initialized Indicator.Size correctly?");
            }
        }

        /// <summary>
        /// Draws Indicator default text based on its position relative to the rows and columns of the board. Drawing once
        /// is necessary to set the proper position of the Indicator.
        /// </summary>
        internal void Draw(SpriteBatch sb, SpriteFont font, Board gameBoard, int cellSize) {
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
