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

        GuiSystem guiSystem = new GuiSystem();

        GuiBox boxLifeBars;

        GuiTextBox txtBoxFlamoca;
        GuiTextBox txtBoxScore;
        GuiTextBox txtBoxWaves;

        GuiLifeBar lfbHealthBar;
        GuiLifeBar lfbShieldBar;

        public HudManager(Player player)
        {
            playerRef = player;
        }

        public void Initialize()
        {
            guiSystem.Initialize();
            txtBoxScore = new GuiTextBox(new Vector2(10, 10), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Score: " + Player.Score, @"GUIFont");
            txtBoxFlamoca = new GuiTextBox(new Vector2(10, 49), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Flamoca: " + Player.Flamoca, @"GUIFont");
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            txtBoxWaves = new GuiTextBox(new Vector2(graphicsDevice.Viewport.Width - 147, graphicsDevice.Viewport.Height - 39), 137, 29, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, "Waves: " + SpawnManager.WaveCount, @"GUIFont");
            boxLifeBars = new GuiBox(new Vector2(10, graphicsDevice.Viewport.Height - 54), 162, 44, 1, new Color(0, 0, 0), new Color(98, 0, 0), 39f, 100f, graphicsDevice);
            lfbHealthBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 45), playerRef.myShip.Health, @"GUI\Health-Bar");
            lfbShieldBar = new GuiLifeBar(new Vector2(28, graphicsDevice.Viewport.Height - 29), playerRef.myShip.Shield, @"GUI\Shield-Bar");

            guiSystem.Add(txtBoxScore);
            guiSystem.Add(txtBoxFlamoca);
            guiSystem.Add(txtBoxWaves);
            guiSystem.Add(boxLifeBars);
            guiSystem.Add(lfbHealthBar);
            guiSystem.Add(lfbShieldBar);

            guiSystem.LoadContent(content, graphicsDevice);
        }

        public void UnloadContent()
        {
            guiSystem.UnloadContent();
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
            guiSystem.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
