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
    // All the stuff that a player expects to be on a start menu: New Game, Continue, Options, and Exit Game.
    class StartMenu
    {
        private GraphicsDeviceManager graphics;

        private BackgroundManager backgroundManager = new BackgroundManager(false);

        private GuiSystem guiSystem = new GuiSystem();
        private GuiBox boxHeaderLine;
        private GuiButton btnNewGame, btnContinue, btnOptions, btnExit;

        private SpriteFont HeaderFont;
        private Vector2 HeaderPosition;
        private KeyboardState CurrentKeyboardState, PreviousKeyboardState;
        private int ButtonIndex = 0;

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
            backgroundManager.LoadContent(Content, graphicsDevice, @"Backgrounds\Clouds\Cloud-Blue-1", @"Backgrounds\Clouds\Cloud-Blue-2");

            HeaderFont = Content.Load<SpriteFont>(@"Fonts\StartMenuHeaderFont");
            
            HeaderPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - (HeaderFont.MeasureString("Levi Challenge").X / 2), graphicsDevice.Viewport.Height / 20);
            
            boxHeaderLine = new GuiBox(new Vector2(graphicsDevice.Viewport.Width / 2 - (float)((HeaderFont.MeasureString("Levi Challenge").X * 1.3) / 2), graphicsDevice.Viewport.Height / 6), (int)(HeaderFont.MeasureString("Levi Challenge").X * 1.3), 2, 0, Color.White * 0.4f, Color.White, graphicsDevice);

            btnNewGame = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 168, graphicsDevice.Viewport.Height / 4), 336, 69, 0, Color.Black * 0.3f, Color.YellowGreen * 0.3f, Color.White, Color.White * 0.6f, "New Game", @"Fonts\StartMenuButtonFont");
            btnContinue = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 168, graphicsDevice.Viewport.Height / 4 + 69), 336, 69, 0, Color.Black * 0.15f, Color.YellowGreen * 0.15f, Color.White, Color.White * 0.6f, "Continue", @"Fonts\StartMenuButtonFont");
            btnOptions = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 168, graphicsDevice.Viewport.Height / 4 + 138), 336, 69, 0, Color.Black * 0.3f, Color.YellowGreen * 0.3f, Color.White, Color.White * 0.6f, "Options", @"Fonts\StartMenuButtonFont");
            btnExit = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 168, graphicsDevice.Viewport.Height / 4 + 207), 336, 69, 0, Color.Black * 0.15f, Color.YellowGreen * 0.15f, Color.White, Color.White * 0.6f, "Exit", @"Fonts\StartMenuButtonFont");
            
            guiSystem.Add(boxHeaderLine);
            guiSystem.Add(btnNewGame);
            guiSystem.Add(btnContinue);
            guiSystem.Add(btnOptions);
            guiSystem.Add(btnExit);

            guiSystem.LoadContent(Content, graphicsDevice);
            guiSystem.ButtonIndexUpdate(ButtonIndex);
        }

        public void UnloadContent()
        {
            guiSystem.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            backgroundManager.Update();
            CurrentKeyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (btnNewGame.IsSelected)
                    Game1.gameState = Game1.GameState.Playing;
            }

            if ((CurrentKeyboardState.IsKeyUp(Keys.Down) && PreviousKeyboardState.IsKeyDown(Keys.Down)) || (CurrentKeyboardState.IsKeyUp(Keys.S) && PreviousKeyboardState.IsKeyDown(Keys.S)))
            {
                if (ButtonIndex < guiSystem.GuiButtonCount - 1)
                    ButtonIndex++;

                guiSystem.ButtonIndexUpdate(ButtonIndex);
            }

            if ((CurrentKeyboardState.IsKeyUp(Keys.Up) && PreviousKeyboardState.IsKeyDown(Keys.Up)) || (CurrentKeyboardState.IsKeyUp(Keys.W) && PreviousKeyboardState.IsKeyDown(Keys.W)))
            {
                if (ButtonIndex > 0)
                    ButtonIndex--;

                guiSystem.ButtonIndexUpdate(ButtonIndex);
            }
            PreviousKeyboardState = CurrentKeyboardState;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            backgroundManager.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(HeaderFont, "Levi Challenge", HeaderPosition, Color.White * 0.6f);
            guiSystem.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
