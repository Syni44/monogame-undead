using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Undead_040220.System;

namespace Undead_040220.Structures
{
    class LegendMonster : IDraggable
    {
        public bool Dragging { get; set; }
        public bool Hovering { get; set; }
        public int Stock { get; private set; }
        public string MonsterName { get; private set; }
        public Texture2D Texture { get; private set; }
        public Color Color { get; private set; }
        public Rectangle Rect { get; private set; }


        public LegendMonster(string name, Texture2D texture, Rectangle rect, int stock) {
            MonsterName = name;
            Texture = texture;
            Rect = rect;
            Stock = stock;
        }

        public void Update(MouseState mouse, KeyboardState keyboard) {
            if (Rect.Contains(mouse.Position)) {
                Hovering = true;
                Debug.WriteLine($"mouse hovering {MonsterName} rect!");
            }
            else {
                Hovering = false;
            }

            if (Hovering && mouse.LeftButton == ButtonState.Pressed && !Dragging) {
                Dragging = true;
            }

            if (Dragging && mouse.LeftButton == ButtonState.Released) {
                Dragging = false;
            }

            if (Dragging) {
                // todo: draw the texture of the monster here when dragging -- may require referencing SpriteBatch down to this level
                // (perhaps use a fetch method to snag it from Undead.cs?)
            }
        }
    }
}
