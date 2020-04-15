using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Undead_040220.Structures.Monsters;

namespace Undead_040220.Structures
{
    internal abstract class Monster {
        internal abstract Vector2 Coordinate { get; set; }
        internal abstract Texture2D Texture { get; set; }

        public Monster(Vector2 coordinate) {
            Coordinate = coordinate;
        }

        /// <summary>
        /// Whether the Monster is seen by the given indicator.
        /// </summary>
        /// <param name="originIndicator"></param>
        /// <returns></returns>
        internal abstract bool IsSeen(Indicator originIndicator);
    }
}
