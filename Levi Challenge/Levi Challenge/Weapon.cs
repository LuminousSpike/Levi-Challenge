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
        int Damage;
        float MoveSpeed;
        float RefireRate;
        float OffsetX;
        float OffsetY;
        TimeSpan FireTime;
        TimeSpan PreviousFireTime;

        public Weapon(Texture2D texture, int damage, float moveSpeed, float refireRate, float offsetX, float offsetY)
        {
            Texture = texture;
            Damage = damage;
            MoveSpeed = moveSpeed;
            RefireRate = refireRate;
            OffsetX = offsetX;
            OffsetY = offsetY;
            FireTime = TimeSpan.FromSeconds(RefireRate);
        }

        public void fire(GameTime gameTime, ProjectileManager projectileManager, Vector2 postition)
        {
            if (gameTime.TotalGameTime - PreviousFireTime > FireTime)
            {
                Projectile myProjectile = new Projectile(Texture, postition, Damage, MoveSpeed);
                myProjectile.Position.X += OffsetX;
                myProjectile.Position.Y += OffsetY;
                PreviousFireTime = gameTime.TotalGameTime;
                projectileManager.AddProjectile(myProjectile);
            }
        }
    }
}
