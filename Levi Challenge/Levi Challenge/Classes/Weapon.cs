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
        int fired = 0;

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        private TimeSpan FireTime;
        private TimeSpan PreviousFireTime = new TimeSpan();
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

        public Weapon ShallowCopy()
        {
            Weapon other = (Weapon)this.MemberwiseClone();
            return other;
        }


        public void fire(GameTime gameTime, ProjectileManager projectileManager, Vector2 postition, bool isplayer, Ship ship)
        {
            if (gameTime.TotalGameTime - this.PreviousFireTime > FireTime)
            {
                float projectileSpeed;
                if (isplayer)
                    projectileSpeed = ProjectileSpeed;
                else
                    projectileSpeed = -ProjectileSpeed;
                Projectile myProjectile = new Projectile(ProjectileTexture, ProjectileType, projectileSpeed, ProjectileDamage, postition, ship);
                myProjectile.Position.X += OffsetX;
                myProjectile.Position.Y += OffsetY;
                this.PreviousFireTime = gameTime.TotalGameTime;
                projectileManager.AddProjectile(myProjectile);
                fired++;
            }
        }
    }
}
