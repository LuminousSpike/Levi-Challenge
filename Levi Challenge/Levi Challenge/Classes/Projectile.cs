using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Projectile
    {
        public Rectangle CollisionBox;

        Texture2D Texture;
        public Vector2 Position;
        public int Damage;
        float MoveSpeed;
        public bool Active = true;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public Projectile(Texture2D texture, Vector2 position, int damage, float moveSpeed)
        {
            Texture = texture;
            Position = position;
            Damage = damage;
            MoveSpeed = moveSpeed;
        }

        public void Update(Viewport viewport)
        {
            Position.X += MoveSpeed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (Position.X + Width / 2 > viewport.Width)
                Active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
