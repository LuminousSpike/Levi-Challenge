using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class ParallaxPlanet : ParallaxBackground
    {
        public override void Initialize(Microsoft.Xna.Framework.Content.ContentManager content, string texturePath, int screenWidth, float speed)
        {
            base.Initialize(content, texturePath, screenWidth, speed);
            positions = new Vector2[1];
            positions[0] = new Vector2(screenWidth - 20, 200);
        }

        public override void Update()
        {
            positions[0].X += speed;
        }
    }
}
