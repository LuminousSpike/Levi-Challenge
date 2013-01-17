using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solar;
using System;

namespace Levi_Challenge
{
    class HudManager
    {
        GuiTextBox txtBoxFlamoca;
        GuiTextBox txtBoxScore;

        SpriteFont Font;

        public void Initialize()
        {
            txtBoxScore = new GuiTextBox(new Vector2(10, 10), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Score: " + Player.Score);
            txtBoxFlamoca = new GuiTextBox(new Vector2(10, 49), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Flamoca: " + Player.Flamoca);
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Font = content.Load<SpriteFont>(@"GUIFont");
            txtBoxScore.LoadContent(graphicsDevice, null, Font);
            txtBoxFlamoca.LoadContent(graphicsDevice, null, Font);
        }

        public void UnloadContent()
        {
            txtBoxScore.UnloadContent();
            txtBoxFlamoca.UnloadContent();
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            txtBoxScore.UpdateText("Score: " + Player.Score, graphicsDevice);
            txtBoxFlamoca.UpdateText("Flamoca: " + Math.Round((double)Player.Flamoca, 2), graphicsDevice);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            txtBoxScore.Draw(spritebatch);
            txtBoxFlamoca.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
