using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Undead_040220.Structures
{
    class Cell
    {
        // relative to grid, ie {0, 2}
        public Vector2 Coordinate { get; private set; }
        // pixel position within game window, ie {440, 80}
        public Vector2 Position { get; private set; }
        public int CellSize { get; private set; }
        public int BorderThickness { get; private set; }

        public Cell(int coordX, int coordY, int cellSize, int borderThickness) {
            Coordinate = new Vector2(coordX, coordY);
            CellSize = cellSize;
            BorderThickness = borderThickness;
        }

        // Draws a Cell to a position depending on it's coordinate within the game board.
        internal void Draw(SpriteBatch sb, Texture2D t, Vector2 pos, Vector2 scale) {
            // TODO: ensure newer Position assignment is just as effective as this old one
            //Position = new Vector2(pos.X + (Coordinate.X * CellSize), pos.Y + (Coordinate.Y * CellSize));
            Position = pos + (Coordinate * CellSize);

            sb.Draw(texture: t,
                position: Position,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: scale * ((CellSize / 4) - BorderThickness),
                effects: SpriteEffects.None,
                layerDepth: 1f
            );
        }

        public void DrawCellSprite(SpriteBatch sb, Texture2D t) {
            sb.Draw( // Texture2D, Rectangle, Color
                t, 
                new Rectangle(Position.ToPoint(), new Point(CellSize - (BorderThickness * 4), CellSize - (BorderThickness * 4))), 
                Color.White
            );
        }
    }
}
