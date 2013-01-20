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
using Solar;

namespace Levi_Challenge
{
    class StartMenu
    {
        GraphicsDeviceManager graphics;

        GuiSystem guiSystem = new GuiSystem();
        GuiButton btnStartGame;

        public StartMenu(GraphicsDeviceManager graphicsDeviceManager)
        {
            graphics = graphicsDeviceManager;
        }

        public void Initialize()
        {
            guiSystem.Initialize();
        }

        public void LoadContent(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            btnStartGame = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 100, graphicsDevice.Viewport.Height / 2 - 20), 200, 40, 1, Color.Orange, Color.White, 100, 100, "Start Game", @"Fonts\GUIFont");
            guiSystem.Add(btnStartGame);

            guiSystem.LoadContent(Content, graphicsDevice);
        }

        public void UnloadContent()
        {
            guiSystem.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Game1.gameState = Game1.GameState.Playing;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            guiSystem.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
