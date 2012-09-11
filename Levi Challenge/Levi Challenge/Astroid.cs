using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Levi_Challenge
{
    class Astroid
    {
        public Texture2D Texture;
        Texture2D Texture1 { get; set; }
        Texture2D Texture2 { get; set; }
        Texture2D Texture3 { get; set; }
        Texture2D Texture4 { get; set; }
        public Vector2 Position;
        public Rectangle CollisionBox;
        public bool Active;
        public float Value;
        public bool Splitting = false;

        public Astroid(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4)
        {
            Texture1 = texture1;
            Texture2 = texture2;
            Texture3 = texture3;
            Texture4 = texture4;
        }

        public int Width;
        public int Height;

        Random random = new Random();
        float enemyMoveSpeed { get; set; }
        public int Size;
        int MaxHealth;
        public int Health;
        float Circle = (float)Math.PI * 2;
        float Rotation;
        float RotationRate;

        public void Initialize(int size)
        {
            Size = size;
            Value = (float)random.NextDouble();
            SetTexture();
            SetHealth();
            SetSpeed();
            SetRotation();
            Active = true;
        }

        public void Update(GameTime gametime)
        {
            Position.X -= enemyMoveSpeed;
            Rotation += RotationRate;
            Rotation = Rotation % Circle;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            if (Position.X < -Width)
            {
                Active = false;
            }
            if (Health <= 0)
            {
                Split();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }

        private void Split()
        {
            if (Size <= 1)
            {
                Active = false;
                Player.Flamoca += Value;
            }
            else
            {
                MaxHealth = MaxHealth / 2;
                Health = MaxHealth;
                Size -= 1;
                SetTexture();
                SetHealth();
                SetSpeed();
                SetRotation();
                Splitting = true;
            }
        }

        private void SetTexture()
        {
            if (Size == 4)
                Texture = Texture4;
            else if (Size == 3)
                Texture = Texture3;
            else if (Size == 2)
                Texture = Texture2;
            else
                Texture = Texture1;

            Width = Texture.Width;
            Height = Texture.Height;
        }

        private void SetHealth()
        {
            if (Size == 4)
                Health = 200;
            else if (Size == 3)
                Health = 150;
            else if (Size == 2)
                Health = 100;
            else
                Health = 50;
        }

        private void SetSpeed()
        {
            if (Size == 4)
                enemyMoveSpeed = 0.5f;
            else if (Size == 3)
                enemyMoveSpeed = 1f;
            else if (Size == 2)
                enemyMoveSpeed = 1.5f;
            else
                enemyMoveSpeed = 2f;
        }

        private void SetRotation()
        {
            do
            {
                if (Size == 4)
                    RotationRate = (float)random.Next(-2, 2) / 100;
                else if (Size == 3)
                    RotationRate = (float)random.Next(-4, 4) / 100;
                else if (Size == 2)
                    RotationRate = (float)random.Next(-6, 6) / 100;
                else
                    RotationRate = (float)random.Next(-8, 8) / 100;
            } while (RotationRate == 0);
        }
    }
}
