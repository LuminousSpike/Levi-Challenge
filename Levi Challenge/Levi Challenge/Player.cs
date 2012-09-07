using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Levi_Challenge
{
    class Player
    {
        public Rectangle CollisionBox;
        public static int Score = 0;

        Texture2D texture;
        Vector2 Position;
        Vector2 screenSize;
        Texture2D basicLaserTexture;
        Texture2D basicMissileTexture;
        Weapon basicLaser;
        Weapon basicMissile;
        
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight)
        {
            texture = content.Load<Texture2D>(texturePath);
            Position = new Vector2(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            screenSize = new Vector2(screenWidth, screenHeight);
            basicLaserTexture = content.Load<Texture2D>("Laser-1");
            basicLaser = new Weapon(basicLaserTexture, 8, 20f, 0.3f, texture.Width, texture.Height / 2);
            basicMissileTexture = content.Load<Texture2D>("Missile-1");
            basicMissile = new Weapon(basicMissileTexture, 22, 12f, 1f, texture.Width, texture.Height / 2);
        }

        public void Update(GameTime gameTime, ProjectileManager projectileManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y += -3.5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 3.5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X += -5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 5f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                basicMissile.fire(gameTime, projectileManager, Position);
            }

            basicLaser.fire(gameTime, projectileManager, Position);

            // Make sure that the player does not go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, screenSize.X - texture.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, screenSize.Y - texture.Height);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
