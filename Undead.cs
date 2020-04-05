using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Undead_040220.Structures;

namespace Undead_040220
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Undead : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // game window resolution
        int resWidth = 1600;
        int resHeight = 900;

        Texture2D white_s;
        Texture2D zombie_s;
        Texture2D vampire_s;
        Texture2D ghost_s;
        Texture2D mirrorL_s;
        Texture2D mirrorR_s;

        int cellSize = 128;
        Vector2 scale;

        // game board fields
        Board gameBoard;
        int boardWidth = 6;
        int boardHeight = 5;
        int cellBorderThickness = 3;

        bool initGameDrawn = false;
        System.Random rng = new System.Random();

        public Undead() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = resWidth;
            graphics.PreferredBackBufferHeight = resHeight;

            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // spawn a new game board here and populate with cells and indicators
            gameBoard = new Board(boardWidth, boardHeight, cellSize);
            gameBoard.CreateCells();
            gameBoard.CreateIndicators();

            // define center point when drawing game board to the screen
            gameBoard.SetOrigin(new Point(resWidth / 2, resHeight / 2));

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            white_s = Content.Load<Texture2D>("white_1x1");
            zombie_s = Content.Load<Texture2D>("zombie");
            vampire_s = Content.Load<Texture2D>("vampire");
            ghost_s = Content.Load<Texture2D>("ghost");
            mirrorL_s = Content.Load<Texture2D>("mirror_l");
            mirrorR_s = Content.Load<Texture2D>("mirror_r");

            // determined by defining 64 as the default texture size and dividing it by the width and height
            // of the texture2ds
            // TODO: consider making the texture size a variable and using that to determine cellSize or vice versa
            scale = new Vector2(64 / (float)zombie_s.Width, 64 / (float)zombie_s.Height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null
            );



            if (!initGameDrawn) {
                GraphicsDevice.Clear(Color.Black);

                gameBoard.Draw(spriteBatch, white_s, scale, cellBorderThickness);

                var spriteList = new List<Texture2D>() { zombie_s, vampire_s, ghost_s, mirrorL_s, mirrorR_s };

                for (int i = 0; i < gameBoard.Cells.Count; i++) {
                    Cell c = gameBoard.CellAtCoordinate(i % gameBoard.Width, i / gameBoard.Width);
                    c.DrawCellSprite(spriteBatch, spriteList[rng.Next(spriteList.Count())]);
                }

                initGameDrawn = true;
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
