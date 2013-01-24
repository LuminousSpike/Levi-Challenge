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
        public Ship myShip;
        
        private Texture2D texture;
        private Vector2 screenSize;
        private XMLEngine xmlEngine;

        public Player(XMLEngine xmlengine)
        {
            xmlEngine = xmlengine;
        }
        
        public void Initialize()
        {
            
        }

        public void LoadContent(ContentManager content, int screenWidth, int screenHeight)
        {
            myShip = xmlEngine.PlayerShips[0].ShallowCopy();
            texture = myShip.ShipTexture;
            myShip.position = new Vector2(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            screenSize = new Vector2(screenWidth, screenHeight);
        }

        public void Update(GameTime gameTime, ProjectileManager projectileManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                myShip.Move(0f, -myShip.Speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                myShip.Move(0f, myShip.Speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                myShip.Move(-myShip.Speed, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                myShip.Move(myShip.Speed, 0f);
            }

            myShip.Update();
            myShip.FireWeapon(gameTime, projectileManager, myShip.position, true);

            // Make sure that the player does not go out of bounds
            myShip.position.X = MathHelper.Clamp(myShip.position.X, 0, screenSize.X - texture.Width);
            myShip.position.Y = MathHelper.Clamp(myShip.position.Y, 0, screenSize.Y - texture.Height);
            CollisionBox = new Rectangle((int)myShip.position.X, (int)myShip.position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, myShip.position, Color.White);
        }
    }
}
