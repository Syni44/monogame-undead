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
        public List<Mirror> Mirrors { get; private set; } = new List<Mirror>();
        public List<Monster> Monsters { get; private set; } = new List<Monster>();

        private Vector2 _origin = new Vector2();
        private Random rng = new Random();

        public Board(int width = 4, int height = 4, int cellSize = 64) {
            Width = width;
            Height = height;
            CellSize = cellSize;
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

        /// <summary>
        /// Determines the locations to spawn mirrors.
        /// </summary>
        public void CreateMirrors() {
            do {
                Mirrors.Clear();

                for (int i = 0; i < Cells.Count; i++) {
                    int direction = rng.Next(2);

                    switch (rng.Next(3)) {
                        case 0: break;
                        case 1: break;
                        default:
                            Mirrors.Add(new Mirror((Mirror.Direction)direction, new Vector2(i % Width, i / Width)));
                            break;
                    }
                }
            } while (Mirrors.Count <= (Width * Height / 4) || Mirrors.Count > (Width * Height / 1.8));
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

        public void DrawMirrors(SpriteBatch sb, Texture2D mL_t, Texture2D mR_t) {
            foreach (Mirror m in Mirrors) {
                // use rng to determine the angle of the mirror; which texture to use
                Texture2D chosenDirectionTexture = (m.DirectionOfMirror == 0) ? mL_t : mR_t;

                CellAtCoordinate(m.Coordinate.X, m.Coordinate.Y).DrawCellSprite(sb, chosenDirectionTexture);
            }
        }

        public void SetOrigin(Point centerPoint) 
            => _origin = new Vector2(centerPoint.X - ((Width * CellSize) / 2), centerPoint.Y - ((Height * CellSize) / 2));

        public Cell CellAtCoordinate(float x, float y)
            => Cells.Where(e => e.Coordinate.X == x && e.Coordinate.Y == y).FirstOrDefault();

        public Indicator IndicatorAt(Indicator.Side s, int index)
            => Indicators.Where(e => e.SideOfBoard == s && e.Index == index).FirstOrDefault();
    }
}
