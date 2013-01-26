using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Projectile
    {
        public Rectangle CollisionBox;
        public Color[] TextureData;
        public Vector2 Position;
        public int ProjectileDamage;
        public bool Active = true, EnemyProjectile;
        public Ship ShooterShip;
        public Matrix myTransform;

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

        public Projectile(Texture2D projectileTexture, string projectileType, float projectileSpeed, int projectileDamage, Vector2 projectilePostition, Ship ship, bool enemyProjectile = false)
        {
            ProjectileTexture = projectileTexture;
            ProjectileType = projectileType;
            ProjectileSpeed = projectileSpeed;
            ProjectileDamage = projectileDamage;
            Position = projectilePostition;
            ShooterShip = ship;
            EnemyProjectile = enemyProjectile;
            TextureData = new Color[ProjectileTexture.Width * ProjectileTexture.Height];
            ProjectileTexture.GetData(TextureData);
        }

        public void Update(Viewport viewport)
        {
            Position.X += ProjectileSpeed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            myTransform = Matrix.CreateTranslation(new Vector3(Position, 0.0f));
            if (Position.X - Width / 2 > viewport.Width || Position.X + Width / 2 < 0)
                Active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProjectileTexture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
