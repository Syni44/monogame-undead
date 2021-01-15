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
    /// <summary>
    /// Determined after mirrors are drawn, Routes define the travel paths through the board from one Indicator to another.
    /// </summary>
    class Route
    {
        public Indicator PointA { get; private set; }
        public Indicator PointB { get; private set; }
        public List<Cell> CellsOnRouteAToB { get; private set; } = new List<Cell>();

        public Route(Indicator i1, Indicator i2, List<Cell> cells) {
            PointA = i1;
            PointB = i2;
            CellsOnRouteAToB = cells;
        }
    }
}
