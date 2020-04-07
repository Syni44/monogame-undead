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
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellSize { get; private set; }
        public List<Cell> Cells { get; private set; } = new List<Cell>();
        public List<Indicator> Indicators { get; private set; } = new List<Indicator>();

        private Vector2 _origin = new Vector2();

        public Board(int width = 4, int height = 4, int cellSize = 64) {
            Width = width;
            Height = height;
            CellSize = cellSize;
        }

        /// <summary>
        /// For drawing any elements on screen that are related to the game's playing grid.
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb, Texture2D t, SpriteFont font, Vector2 scale) {
            // draws a white square for each cell
            foreach (Cell c in Cells) {
                c.Draw(sb, t, scale);
            }

            // TODO: draws text "hi" at every indicator point
            foreach (Indicator n in Indicators) {
                n.Draw(sb, font, this, CellSize);
            }
        }

        /// <summary>
        /// Fills the gameboard with empty Cells.
        /// </summary>
        public void CreateCells(int cellSize, int borderThickness) {
            for (int j = 0; j < Height; j++) {
                for (int i = 0; i < Width; i++) {
                    Cell c = new Cell(i, j, cellSize, borderThickness);
                    c.SetPosition(_origin);
                    Cells.Add(c);
                }
            }
        }

        /// <summary>
        /// Surrounds the game board with indicator points, used to count monsters along reflected path.
        /// </summary>
        public void CreateIndicators() {
            // TODO: implement logic that creates indicator points on either side of every row and column
            // Keep indicator points fairly close to the border of the grid (cellSize / 2?) and use SpriteFont

            for (int i = 0; i < Width; i++) {
                Indicators.Add(new Indicator(Indicator.Side.Top, i, this, CellSize));
                Indicators.Add(new Indicator(Indicator.Side.Bottom, i, this, CellSize));
            }

            for (int j = 0; j < Height; j++) {
                Indicators.Add(new Indicator(Indicator.Side.Left, j, this, CellSize));
                Indicators.Add(new Indicator(Indicator.Side.Right, j, this, CellSize));
            }
        }

        public void SetOrigin(Point centerPoint) 
            => _origin = new Vector2(centerPoint.X - ((Width * CellSize) / 2), centerPoint.Y - ((Height * CellSize) / 2));

        public Cell CellAtCoordinate(int x, int y)
            => Cells.Where(e => e.Coordinate.X == x && e.Coordinate.Y == y).FirstOrDefault();

        public Indicator IndicatorAt(Indicator.Side s, int index)
            => Indicators.Where(e => e.SideOfBoard == s && e.Index == index).FirstOrDefault();
    }
}
