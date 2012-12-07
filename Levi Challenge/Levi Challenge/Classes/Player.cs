﻿using System;
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
        public Ship myShip;
        
        Texture2D texture;
        Vector2 position;
        Vector2 screenSize;
        
        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager content, int screenWidth, int screenHeight)
        {
            myShip = XMLEngine.PlayerShips[0].ShallowCopy();
            texture = myShip.ShipTexture;
            position = new Vector2(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            screenSize = new Vector2(screenWidth, screenHeight);
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

            myShip.FireWeapon(gameTime, projectileManager, position, true);

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
