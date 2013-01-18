using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solar;
using System;

namespace Levi_Challenge
{
    class HudManager
    {
        Player playerRef;

        GuiBox boxLifeBars;

        GuiTextBox txtBoxFlamoca;
        GuiTextBox txtBoxScore;
        GuiTextBox txtBoxWaves;

        GuiLifeBar lfbHealthBar;
        GuiLifeBar lfbShieldBar;

        SpriteFont Font;

        public HudManager(Player player)
        {
            playerRef = player;
        }

        public void Initialize()
        {
            txtBoxScore = new GuiTextBox(new Vector2(10, 10), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Score: " + Player.Score);
            txtBoxFlamoca = new GuiTextBox(new Vector2(10, 49), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Flamoca: " + Player.Flamoca);
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Texture2D HealthbarTexture = content.Load<Texture2D>(@"GUI\Health-Bar");
            Texture2D ShieldbarTexture = content.Load<Texture2D>(@"GUI\Shield-Bar");

            Font = content.Load<SpriteFont>(@"GUIFont");
            txtBoxScore.LoadContent(graphicsDevice, null, Font);
            txtBoxFlamoca.LoadContent(graphicsDevice, null, Font);
            txtBoxWaves = new GuiTextBox(new Vector2(graphicsDevice.Viewport.Width - 147, graphicsDevice.Viewport.Height - 39), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Waves: " + SpawnManager.WaveCount);
            txtBoxWaves.LoadContent(graphicsDevice, null, Font);
            boxLifeBars = new GuiBox(new Vector2(10, graphicsDevice.Viewport.Height - 54), 162, 44, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, graphicsDevice);
            //lfbHealthBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 45), playerRef.myShip.Health, 126, 9, 0, new Color(72, 102, 0), Color.White, 100f, 100f);
            //lfbShieldBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 29), playerRef.myShip.Shield, 126, 9, 0, new Color(0, 94, 121), Color.White, 100f, 100f);
            lfbHealthBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 45), playerRef.myShip.Health);
            lfbHealthBar.LoadContent(HealthbarTexture, graphicsDevice);
            lfbShieldBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 29), playerRef.myShip.Shield);
            lfbShieldBar.LoadContent(ShieldbarTexture, graphicsDevice);
        }

        public void UnloadContent()
        {
            txtBoxScore.UnloadContent();
            txtBoxFlamoca.UnloadContent();
            txtBoxWaves.UnloadContent();
            boxLifeBars.UnloadContent();
            lfbHealthBar.UnloadContent();
            lfbShieldBar.UnloadContent();
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            txtBoxScore.UpdateText("Score: " + Player.Score, graphicsDevice);
            txtBoxFlamoca.UpdateText("Flamoca: " + Math.Round((double)Player.Flamoca, 2), graphicsDevice);
            txtBoxWaves.UpdateText("Wave: " + SpawnManager.WaveCount, graphicsDevice);
            lfbHealthBar.Update(playerRef.myShip.Health);
            lfbShieldBar.Update((int)playerRef.myShip.Shield);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            txtBoxScore.Draw(spritebatch);
            txtBoxFlamoca.Draw(spritebatch);
            txtBoxWaves.Draw(spritebatch);
            boxLifeBars.Draw(spritebatch);
            lfbHealthBar.Draw(spritebatch);
            lfbShieldBar.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
