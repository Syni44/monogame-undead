using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Undead_040220.System;

namespace Undead_040220.Structures.Legends
{
    class VampireLegend : IDraggable
    {
        public bool Dragging { get; set; }
        public int Stock { get; private set; }
    }
}
