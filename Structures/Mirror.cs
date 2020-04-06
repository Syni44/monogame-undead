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
    internal class Mirror : Cell
    {
        public Mirror(int coordX, int coordY, int cellSize, int borderThickness)
            : base(coordX, coordY, cellSize, borderThickness) {

        }
    }
}
