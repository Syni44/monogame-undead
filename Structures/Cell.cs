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
        public Rectangle Rect { get; private set; }
        public int CellSize { get; private set; }
        public int BorderThickness { get; private set; }
        public bool HasMirror { get; set; } = false;
        public bool HasMonster { get; set; } = false;
        public Monster MonsterInCell { get; set; }

        public Cell(int coordX, int coordY, int cellSize, int borderThickness) {
            Coordinate = new Vector2(coordX, coordY);
            CellSize = cellSize;
            BorderThickness = borderThickness;
        }

        internal void SetPosition(Vector2 boardOriginPoint, int legendHeight, int spacing) {
            Vector2 tempPosition = boardOriginPoint + Coordinate + (Coordinate * CellSize);
            Position = tempPosition += new Vector2(0, (legendHeight / 2) + (spacing / 2));
        }

        // Draws a Cell to a position depending on it's coordinate within the game board.
        internal void Draw(SpriteBatch sb, Texture2D t, Vector2 scale) {
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
            Rect = new Rectangle(Position.ToPoint(), new Point(CellSize - (BorderThickness * 4), CellSize - (BorderThickness * 4)));

            sb.Draw( // Texture2D, Rectangle, Color
                t, 
                Rect, 
                Color.White
            );
        }
    }
}
