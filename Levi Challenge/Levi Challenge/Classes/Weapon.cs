using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Weapon
    {
        Texture2D Texture;
        int ProjectileDamage;
        float ProjectileSpeed;
        float RefireRate;
        float OffsetX;
        float OffsetY;
        TimeSpan FireTime;
        TimeSpan PreviousFireTime;

        public Weapon(Texture2D texture, int damage, float moveSpeed, float refireRate, float offsetX, float offsetY)
        {
            Texture = texture;
            ProjectileDamage = damage;
            ProjectileSpeed = moveSpeed;
            RefireRate = refireRate;
            OffsetX = offsetX;
            OffsetY = offsetY;
            FireTime = TimeSpan.FromSeconds(RefireRate);
        }

        public void fire(GameTime gameTime, ProjectileManager projectileManager, Vector2 postition)
        {
            if (gameTime.TotalGameTime - PreviousFireTime > FireTime)
            {
                Projectile myProjectile = new Projectile(Texture, postition, ProjectileDamage, ProjectileSpeed);
                myProjectile.Position.X += OffsetX;
                myProjectile.Position.Y += OffsetY;
                PreviousFireTime = gameTime.TotalGameTime;
                projectileManager.AddProjectile(myProjectile);
            }
        }
    }
}
