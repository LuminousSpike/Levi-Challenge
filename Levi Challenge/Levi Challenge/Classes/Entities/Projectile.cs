﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Projectile
    {
        public Rectangle CollisionBox;
        public Vector2 Position;
        public int ProjectileDamage;
        public bool Active = true;
        public Ship ShooterShip;

        private Texture2D ProjectileTexture;
        private string ProjectileType;
        private float ProjectileSpeed;

        public int Width
        {
            get { return ProjectileTexture.Width; }
        }

        public int Height
        {
            get { return ProjectileTexture.Height; }
        }

        public Projectile(Texture2D projectileTexture, string projectileType, float projectileSpeed, int projectileDamage, Vector2 projectilePostition, Ship ship)
        {
            ProjectileTexture = projectileTexture;
            ProjectileType = projectileType;
            ProjectileSpeed = projectileSpeed;
            ProjectileDamage = projectileDamage;
            Position = projectilePostition;
            ShooterShip = ship;
        }

        public void Update(Viewport viewport)
        {
            Position.X += ProjectileSpeed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (Position.X + Width / 2 > viewport.Width)
                Active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProjectileTexture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}