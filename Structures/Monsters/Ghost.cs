﻿using Microsoft.Xna.Framework;
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

        internal override bool IsSeen(Indicator originIndicator) {
            throw new NotImplementedException();
        }
    }
}
