using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Undead_040220.Structures.Monsters
{
    internal class Ghost : Monster
    {
        internal override Vector2 Coordinate { get; set; }
        internal override Texture2D Texture { get; set; }

        public Ghost(Vector2 coordinate) : base(coordinate) {
            Coordinate = coordinate;
        }

        internal override bool IsSeen(Indicator originIndicator, Route route) {
            List<Cell> cellsOnRoute = route.CellsOnRouteAToB.ToList();  // why is adding .ToList() necessary? without it the counts are wrong!

            if (route.PointB.Position == originIndicator.Position) {    // the route should be interpreted "backwards"
                cellsOnRoute.Reverse();
            }

            int thisIndex = cellsOnRoute.FindIndex(e => e.Coordinate == Coordinate);
            int firstMirrorIndex = cellsOnRoute.FindIndex(e => e.HasMirror);

            // no mirror on route
            if (firstMirrorIndex == -1) return false;

            // mirror found
            if (firstMirrorIndex < thisIndex) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
