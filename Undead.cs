using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Point screenCenter;

        Texture2D white_s;
        Texture2D zombie_s;
        Texture2D vampire_s;
        Texture2D ghost_s;
        Texture2D mirrorL_s;
        Texture2D mirrorR_s;

        SpriteFont indicator_font;

        int cellSize = 96;
        Vector2 scale = new Vector2(4, 4);

        // game board fields
        Board gameBoard;
        int boardWidth = 6;
        int boardHeight = 5;
        int cellBorderThickness = 2;

        // legend fields
        Legend gameLegend;
        int legendHeight = 100;
        int vSpacing = 60;

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
            if (boardWidth > 10) boardWidth = 10;
            if (boardHeight > 6) boardHeight = 6;

            if (boardWidth < 3) boardWidth = 3;
            if (boardHeight < 3) boardHeight = 3;

            gameBoard = new Board(boardWidth, boardHeight, cellSize);

            // define center point when drawing game board to the screen
            screenCenter = new Point(resWidth / 2, resHeight / 2);
            gameBoard.SetOrigin(screenCenter);

            // spawn legend
            gameLegend = new Legend(legendHeight, cellSize, 16);

            gameBoard.CreateCells(cellSize, cellBorderThickness, legendHeight, vSpacing);
            gameBoard.CreateIndicators();
            gameBoard.CreateMirrors();
            gameBoard.SpawnMonsters();
            gameBoard.DetermineRoutes();

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

            indicator_font = Content.Load<SpriteFont>("indicator_font");
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

            // TODO: when updating legend stocks...
            // re-calculate the rectangle/point where DrawString should write the numbers!
            // if the number is 2 digits+, it becomes off-centered. Need a centering algorithm for that

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

                gameBoard.Draw(spriteBatch, white_s, indicator_font, scale);
                gameLegend.Draw(spriteBatch, zombie_s, vampire_s, ghost_s,
                    new Rectangle(screenCenter.X - (gameLegend.Width / 2),
                        screenCenter.Y - ((gameBoard.Height * cellSize) / 2) - ((gameLegend.Height / 2) + vSpacing),
                        gameLegend.Width,
                        gameLegend.Height)
                    );

                gameBoard.DrawMirrors(spriteBatch, mirrorL_s, mirrorR_s);

                // vvv this is DEBUG -- we shouldn't show the monster sprites, that's the point of the game!
                gameBoard.DrawMonsters(spriteBatch, zombie_s, vampire_s, ghost_s);

                gameLegend.DrawStocks(
                    spriteBatch,
                    indicator_font,
                    gameBoard.Monsters.Count(e => e.GetType().Name == "Zombie").ToString(),
                    gameBoard.Monsters.Count(e => e.GetType().Name == "Vampire").ToString(),
                    gameBoard.Monsters.Count(e => e.GetType().Name == "Ghost").ToString()
                );

                initGameDrawn = true;
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
