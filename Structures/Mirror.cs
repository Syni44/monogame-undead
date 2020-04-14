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
    internal class Mirror
    {
        public enum Direction
        {
            Left,
            Right
        }

        public Direction DirectionOfMirror { get; private set; }
        public Vector2 Coordinate { get; private set; } = new Vector2();

        public Mirror(Direction d, Vector2 coordinate) {
            DirectionOfMirror = d;
            Coordinate = coordinate;
        }
    }
}
