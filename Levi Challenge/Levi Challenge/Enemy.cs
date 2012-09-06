using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Enemy
    {
        Texture2D Texture { get; set; }
        public Vector2 Position;
        public Rectangle CollisionBox;
        public bool Active;
        public int Health { get; set; }
        public int Value { get; set; }

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        float enemyMoveSpeed { get; set; }

        public Enemy(Texture2D texture, int health, int value, float enemymovespeed)
        {
            Texture = texture;
            Health = health;
            Value = value;
            enemyMoveSpeed = enemymovespeed;
        }

        public void Initialize(Vector2 position)
        {
            Position = position;
            Active = true;
        }

        public void Update(GameTime gametime)
        {
            Position.X -= enemyMoveSpeed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (Position.X < -Width || Health <= 0)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
