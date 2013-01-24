using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solar;

namespace Levi_Challenge
{
    class OptionsMenuScreen
    {
        GuiSystem guiSystem = new GuiSystem();
        private GuiButton btnControlUp, btnControlDown, btnControlLeft, btnControlRight, btnReturn;
        private KeyboardState CurrentKeyboardState, PreviousKeyboardState;
        

        public void Initialize()
        {
            guiSystem.Initialize();
        }

        public void LoadContent(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            btnControlUp = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 25, 10), 50, 50, 0, Color.Black * 0.3f, Color.YellowGreen * 0.3f, Color.White, Color.White * 0.6f, Game1.MoveUpKey.ToString(), @"Fonts\StartMenuButtonFont");
            btnReturn = new GuiButton(new Vector2(graphicsDevice.Viewport.Width / 2 - 168, graphicsDevice.Viewport.Height / 2 + 69), 336, 69, 0, Color.Black * 0.3f, Color.YellowGreen * 0.3f, Color.White, Color.White * 0.6f, "Return", @"Fonts\StartMenuButtonFont");

            guiSystem.Add(btnControlUp);
            guiSystem.Add(btnReturn);
            guiSystem.LoadContent(Content, graphicsDevice);
            guiSystem.ButtonIndexUpdate(0);
        }

        public void UnloadContent()
        {
            guiSystem.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            guiSystem.Update();
            CurrentKeyboardState = Keyboard.GetState();
            if ((CurrentKeyboardState.IsKeyUp(Keys.Enter) && PreviousKeyboardState.IsKeyDown(Keys.Enter)) || (CurrentKeyboardState.IsKeyUp(Keys.E) && PreviousKeyboardState.IsKeyDown(Keys.E)))
            {
                if (btnControlUp.IsSelected)


                if (btnReturn.IsSelected)
                    Game1.gameState = Game1.GameState.StartMenu;
            }
            PreviousKeyboardState = Keyboard.GetState();
        }

        //private Keys GetKey()
        //{
            // Code to get the key the user is pressing
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            guiSystem.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
