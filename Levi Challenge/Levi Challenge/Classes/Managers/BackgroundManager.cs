using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class BackgroundManager
    {
        private ParallaxBackground[] Backgrounds = new ParallaxBackground[6];
        private ParallaxPlanet Planet = new ParallaxPlanet();
        private bool PlanetEnabled;

        public BackgroundManager(bool planetEnabled)
        {
            PlanetEnabled = planetEnabled;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphics, String Cloud1, String Cloud2)
        {
            Create(content, @"Backgrounds\Stars\Stars1", graphics.Viewport.Width, -0.1f, 0);
            Create(content, @"Backgrounds\Stars\Stars2", graphics.Viewport.Width, -0.3f, 1);
            Create(content, @"Backgrounds\Stars\Stars3", graphics.Viewport.Width, -1f, 2);
            Create(content, Cloud1, graphics.Viewport.Width, -0.2f, 3);
            Create(content, Cloud2, graphics.Viewport.Width, -0.6f, 4);
            if (PlanetEnabled)
                Planet.Initialize(content, @"Backgrounds\Planets\Planet3-game", graphics.Viewport.Width, -0.1f);
        }

        public void Create(ContentManager content, String texturePath, int screenWidth, float speed, int Layer)
        {
            ParallaxBackground NewPB = new ParallaxBackground();
            NewPB.Initialize(content, texturePath, screenWidth, speed);
            Add(NewPB, Layer);
        }

        public void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                Backgrounds[i].Update();
            }
            if (PlanetEnabled)
                Planet.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Backgrounds[0].Draw(spriteBatch);
            if(PlanetEnabled)
                Planet.Draw(spriteBatch);
            spriteBatch.End();
            for (int i = 1; i < 3; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                Backgrounds[i].Draw(spriteBatch);
                spriteBatch.End();
            }
            for (int i = 3; i < 5; i++)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                Backgrounds[i].Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        private void Add(ParallaxBackground PB, int Layer)
        {
            Backgrounds[Layer] = PB;
        }
    }
}
