using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Levi_Challenge
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Global Varibles and Constants
        public enum GameState
        {
            StartMenu,
            OptionsMenu,
            Loading,
            Playing,
            Paused,
            Exiting
        }
        public static Keys MoveUpKey = Keys.W, MoveDownKey = Keys.S, MoveLeftKey = Keys.A, MoveRightKey = Keys.D;

        private GameState LoadedGameState = new GameState();

        public static GameState gameState = new GameState();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartMenuScreen startMenu;
        OptionsMenuScreen optionsMenuScreen;
        GameScreen gameScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameState.StartMenu;
            LoadedGameState = GameState.StartMenu;

            startMenu = new StartMenuScreen(graphics);
            startMenu.Initialize();

            //optionsMenuScreen = new OptionsMenuScreen();
            //optionsMenuScreen.Initialize();

            //gameScreen = new GameScreen(graphics);
            // Move all code which requires Content to LoadContent
            //gameScreen.Initialize(Content, GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startMenu.LoadContent(Content, GraphicsDevice);
            //optionsMenuScreen.LoadContent(Content, GraphicsDevice);
            //gameScreen.LoadContent(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            startMenu.UnloadContent();
            //optionsMenuScreen.UnloadContent();
            //gameScreen.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState = GameState.StartMenu;

            // TODO: Add your update logic here
            if (gameState == GameState.StartMenu)
            {
                if (LoadedGameState != GameState.StartMenu)
                {
                    Content.Unload();
                    startMenu = new StartMenuScreen(graphics);
                    startMenu.Initialize();
                    startMenu.LoadContent(Content, GraphicsDevice);
                    LoadedGameState = GameState.StartMenu;
                }
                startMenu.Update(gameTime);
            }

            if (gameState == GameState.OptionsMenu)
            {
                if (LoadedGameState != GameState.OptionsMenu)
                {
                    Content.Unload();
                    optionsMenuScreen = new OptionsMenuScreen();
                    optionsMenuScreen.Initialize();
                    optionsMenuScreen.LoadContent(Content, GraphicsDevice);
                    LoadedGameState = GameState.OptionsMenu;
                }
                optionsMenuScreen.Update(gameTime);
            }

            if (gameState == GameState.Playing)
            {
                if (LoadedGameState != GameState.Playing)
                {
                    Content.Unload();
                    gameScreen = new GameScreen(graphics);
                    gameScreen.Initialize(Content, GraphicsDevice);
                    gameScreen.LoadContent(Content, GraphicsDevice);
                    LoadedGameState = GameState.Playing;
                }
                gameScreen.Update(gameTime, GraphicsDevice);
            }


            if (gameState == GameState.Exiting)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gameState == GameState.StartMenu && LoadedGameState == GameState.StartMenu)
                startMenu.Draw(gameTime, spriteBatch);

            if (gameState == GameState.OptionsMenu && LoadedGameState == GameState.OptionsMenu)
                optionsMenuScreen.Draw(spriteBatch);

            if (gameState == GameState.Playing && LoadedGameState == GameState.Playing)
                gameScreen.Draw(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
