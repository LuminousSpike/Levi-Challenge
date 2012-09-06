using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class ProjectileManager
    {
        public List<Projectile> Projectiles;

        public void AddProjectile(Projectile projectile)
        {
            Projectiles.Add(projectile);
        }

        public void Initialize()
        {
            Projectiles = new List<Projectile>();
        }

        public void Update(Viewport viewport)
        {
            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                Projectiles[i].Update(viewport);
                if (Projectiles[i].Active == false)
                {
                    Projectiles.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Draw(spriteBatch);
            }
        }
    }
}
