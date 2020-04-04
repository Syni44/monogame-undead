using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Undead_040220.Structures
{
    class Cell
    {
        public Vector2 Coordinate { get; private set; }
        public Vector2 Position { get; private set; }
        public int CellSize { get; private set; }
        public int BorderThickness { get; set; }

        public Cell(int x, int y, int cellSize = 64) {
            Coordinate = new Vector2(x, y);
        }

        // Draws a Cell to a position depending on it's coordinate within the game board.
        public void Draw(SpriteBatch sb, Texture2D t, Vector2 pos, int cellSize, Vector2 scale, int border) {
            Position = new Vector2(pos.X + (Coordinate.X * cellSize), pos.Y + (Coordinate.Y * cellSize));
            CellSize = cellSize;
            BorderThickness = border;

            sb.Draw(texture: t,
                position: Position,
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: scale * ((cellSize / 4) - border),
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
