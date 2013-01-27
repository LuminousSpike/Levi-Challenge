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
            Create(content, @"Backgrounds\Stars\Stars1", Game1.ViewPortWidth, -0.1f, 0);
            Create(content, @"Backgrounds\Stars\Stars2", Game1.ViewPortWidth, -0.3f, 1);
            Create(content, @"Backgrounds\Stars\Stars3", Game1.ViewPortWidth, -1f, 2);
            Create(content, Cloud1, Game1.ViewPortWidth, -0.2f, 3);
            Create(content, Cloud2, Game1.ViewPortWidth, -0.6f, 4);
            if (PlanetEnabled)
                Planet.Initialize(content, @"Backgrounds\Planets\Planet3-game", -0.1f);
        }

        public void Create(ContentManager content, String texturePath, float screenWidth, float speed, int Layer)
        {
            ParallaxBackground NewPB = new ParallaxBackground();
            NewPB.Initialize(content, texturePath, speed);
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

        public void Draw(SpriteBatch spriteBatch, Matrix spriteScale)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, spriteScale);
            Backgrounds[0].Draw(spriteBatch);
            if(PlanetEnabled)
                Planet.Draw(spriteBatch);
            for (int i = 3; i < 5; i++)
            {
                Backgrounds[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, spriteScale);
            for (int i = 1; i < 3; i++)
            {
                Backgrounds[i].Draw(spriteBatch);
            }
            spriteBatch.End();

        }

        private void Add(ParallaxBackground PB, int Layer)
        {
            Backgrounds[Layer] = PB;
        }
    }
}
