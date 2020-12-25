using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public List<Route> Routes { get; private set; } = new List<Route>();

        private Vector2 _origin = new Vector2();
        private Random _rng = new Random();

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
                    int direction = _rng.Next(2);

                    // mirror frequency rate. _rng.Next(3) would mean 33.3% chance
                    // new cases should be added to reflect chances
                    switch (_rng.Next(3)) {
                        case 0: break;
                        case 1: break;
                        default:
                            // todo: attach mirror to Cell in cell data.
                            Cells[i].HasMirror = true;
                            Mirrors.Add(new Mirror((Mirror.Direction)direction, new Vector2(i % Width, i / Width)));
                            break;
                    }
                }
            } while (Mirrors.Count <= (Width * Height / 4) || Mirrors.Count > (Width * Height / 1.8));
        }

        /// <summary>
        /// Uses placement of mirrors to determine the start and endpoints for sightlines.
        /// </summary>
        public void DetermineRoutes() {
            for (int i = 0; i < Indicators.Count; i++) {
                if (!Routes.Any(e => e.PointA == Indicators[i] || e.PointB == Indicators[i])) {
                    // indicator n = point A
                    Indicator n1 = Indicators[i];
                    Indicator n2;

                    // get indicator n2: indicator n's coordinates (side of board, index), iterate along the axis determined by
                    // side of board, evaluating each cell until a mirror cell is reached. use mirror type (left vs right) to determine
                    // new route vector. when any coordinate exceeds board size, indicator n2's coordinates as last fetched

                    // iterating from SideOfBoard: if n1.SideOfBoard is even, we iterate along the x axis, if it's odd, y axis
                    // var yetUnknown = n1.SideOfBoard;

                    Vector2 routeDirection;
                    if (n1.SideOfBoard == Indicator.Side.Left) {
                        // moving right
                        routeDirection = new Vector2(1, 0);
                    }
                    else if (n1.SideOfBoard == Indicator.Side.Top) {
                        // moving down
                        routeDirection = new Vector2(0, 1);
                    }
                    else if (n1.SideOfBoard == Indicator.Side.Right) {
                        // moving left
                        routeDirection = new Vector2(-1, 0);
                    }
                    else if (n1.SideOfBoard == Indicator.Side.Bottom) {
                        // moving up
                        routeDirection = new Vector2(0, -1);
                    }
                    else {
                        throw new Exception("something went wrong with route directions");
                    }

                    Vector2 currentCellCoordinate = n1.AttachedCell.Coordinate;

                    // iterate from starting point cell in the route direction by one. First, check if the new cell exists.
                    // if it's outside the bounds of the array / can't get cell at coordinate, we've reached the second
                    // indicator point. If not, check if the cell has a mirror, and reflect by changing route direction.

                    while (true) {
                        currentCellCoordinate += routeDirection;

                        if (currentCellCoordinate.X >= Width || currentCellCoordinate.X < 0
                            || currentCellCoordinate.Y >= Height || currentCellCoordinate.Y < 0) {
                            currentCellCoordinate -= routeDirection;

                            // TODO: get the indicator from Indicators list that corresponds to the right index on the right side
                            // n2 = CellAtCoordinate(currentCellCoordinate.X, currentCellCoordinate.Y).

                            break;
                        }
                        else {
                            if (CellAtCoordinate(currentCellCoordinate.X, currentCellCoordinate.Y).HasMirror) {
                                if (Mirrors.Where(e => e.Coordinate == currentCellCoordinate).FirstOrDefault().DirectionOfMirror == Mirror.Direction.Left) {
                                    // if mirror faces left
                                    routeDirection = new Vector2(routeDirection.Y, routeDirection.X);
                                }
                                else {
                                    // if mirror faces right
                                    routeDirection = new Vector2(-routeDirection.Y, -routeDirection.X);
                                }
                            }
                        }
                    }

                    // Route r = new Route(n1, n2);
                }
            }
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

                CellAtCoordinate(m.Coordinate.X, m.Coordinate.Y)
                    .DrawCellSprite(sb, chosenDirectionTexture);
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
