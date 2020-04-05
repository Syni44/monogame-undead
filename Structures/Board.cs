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
        public void Draw(SpriteBatch sb, Texture2D t, Vector2 scale, int cellBorder) {
            foreach (Cell c in Cells) {
                c.Draw(sb, t, _origin + c.Coordinate, CellSize, scale, cellBorder);
            }
        }

        /// <summary>
        /// Fills the gameboard with empty Cells.
        /// </summary>
        public void CreateCells() {
            for (int j = 0; j < Height; j++) {
                for (int i = 0; i < Width; i++) {
                    Cells.Add(new Cell(i, j));
                }
            }
        }

        public void CreateIndicators() {
            // TODO: implement logic that creates indicator points on either side of every row and column
            // Keep indicator points fairly close to the border of the grid (cellSize / 2?) and use SpriteFont
        }

        public void SetOrigin(Point centerPoint) 
            => _origin = new Vector2(centerPoint.X - ((Width * CellSize) / 2), centerPoint.Y - ((Height * CellSize) / 2));

        public Cell CellAtCoordinate(int x, int y)
            => Cells.Where(e => e.Coordinate.X == x && e.Coordinate.Y == y).FirstOrDefault();
    }
}
