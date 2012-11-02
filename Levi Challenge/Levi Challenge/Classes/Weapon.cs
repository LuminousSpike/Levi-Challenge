using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Weapon
    {
        string WeaponType = null;
        public string WeaponName = null;
        int WeaponClass = 0;
        float WeaponRefireRate = 0f;

        public string ProjectileTexturePath = null;
        string ProjectileType = null;
        int ProjectileDamage = 0;
        float ProjectileSpeed = 0f;

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        TimeSpan FireTime;
        TimeSpan PreviousFireTime;
        public Texture2D ProjectileTexture { get; set; }

        public Weapon(string weaponType, string weaponName, int weaponClass, float weaponRefireRate, string projectileTexture,
            string projectileType, int projectileDamage, float projectileSpeed)
        {
            WeaponType = weaponType;
            WeaponName = weaponName;
            WeaponClass = weaponClass;
            WeaponRefireRate = weaponRefireRate;

            ProjectileTexturePath = projectileTexture;
            ProjectileType = projectileType;
            ProjectileDamage = projectileDamage;
            ProjectileSpeed = projectileSpeed;

            FireTime = TimeSpan.FromSeconds((double)WeaponRefireRate);
        }


        public void fire(GameTime gameTime, ProjectileManager projectileManager, Vector2 postition)
        {
            if (gameTime.TotalGameTime - PreviousFireTime > FireTime)
            {
                Projectile myProjectile = new Projectile(ProjectileTexture, ProjectileType, ProjectileSpeed, ProjectileDamage, postition);
                myProjectile.Position.X += OffsetX;
                myProjectile.Position.Y += OffsetY;
                PreviousFireTime = gameTime.TotalGameTime;
                projectileManager.AddProjectile(myProjectile);
            }
        }
    }
}
