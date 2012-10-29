using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class Enemy
    {
        public Ship myShip;
        public Vector2 Position;
        public Rectangle CollisionBox;
        public bool Active;

        public int Health;
        public float Shield;

        float enemyMoveSpeed;

        public Enemy(Ship ship)
        {
            myShip = ship;
            enemyMoveSpeed = myShip.Speed;
            Health = ship.Health;
            Shield = ship.Shield;
        }

        public void Initialize(Vector2 position)
        {
            Position = position;
            Active = true;
        }

        public void Update(GameTime gametime)
        {
            Position.X -= enemyMoveSpeed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, myShip.ShipTexture.Width, myShip.ShipTexture.Height);
            if (Position.X < -myShip.ShipTexture.Width)
            {
                Active = false;
            }
            if (Health <= 0)
            {
                Active = false;
                Player.Score += myShip.Points;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myShip.ShipTexture, Position, Color.White);
        }
    }
}
