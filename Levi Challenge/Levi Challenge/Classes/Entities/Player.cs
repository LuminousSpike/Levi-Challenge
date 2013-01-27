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
        public static float Flamoca = 0f;
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
            Score = 0;
            Flamoca = 0f;
        }

        public void LoadContent(ContentManager content)
        {
            myShip = xmlEngine.PlayerShips[0].ShallowCopy();
            texture = myShip.ShipTexture;
            myShip.Position = new Vector2(Game1.ViewPortWidth / 2 - texture.Width / 2, Game1.ViewPortHeight / 2 - texture.Height / 2);
            screenSize = new Vector2(Game1.ViewPortWidth, Game1.ViewPortHeight);
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

            myShip.Update(gameTime);
            myShip.FireWeapon(gameTime, projectileManager, myShip.Position, true);

            // Make sure that the player does not go out of bounds
            myShip.Position.X = MathHelper.Clamp(myShip.Position.X, 0, screenSize.X - texture.Width);
            myShip.Position.Y = MathHelper.Clamp(myShip.Position.Y, 0, screenSize.Y - texture.Height);
            CollisionBox = new Rectangle((int)myShip.Position.X, (int)myShip.Position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, myShip.Position, Color.White);
        }
    }
}
