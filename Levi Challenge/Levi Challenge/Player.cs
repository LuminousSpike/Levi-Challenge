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
        public static float Flamoca;

        Ship myShip = new Ship(100, 40f, 3.5f);
        Texture2D texture;
        Vector2 position;
        Vector2 screenSize;
        Texture2D basicLaserTexture;
        Texture2D basicMissileTexture;
        Weapon basicLaser;
        Weapon basicMissile;

        
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight)
        {
            texture = content.Load<Texture2D>(texturePath);
            position = new Vector2(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
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
                position.Y += -myShip.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += myShip.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X += -myShip.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += myShip.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                basicMissile.fire(gameTime, projectileManager, position);
            }

            basicLaser.fire(gameTime, projectileManager, position);

            // Make sure that the player does not go out of bounds
            position.X = MathHelper.Clamp(position.X, 0, screenSize.X - texture.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, screenSize.Y - texture.Height);
            CollisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
