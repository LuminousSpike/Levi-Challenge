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

        public Enemy(Ship ship)
        {
            myShip = ship.ShallowCopy();
        }

        public Enemy ShallowCopy()
        {
            Enemy other = (Enemy)this.MemberwiseClone();
            other.myShip = this.myShip.ShallowCopy();
            return other;
        }

        public void LoadContent(Vector2 position)
        {
            Position = position;
            Active = true;
        }

        public void Update(GameTime gametime, ProjectileManager projectilemanager)
        {
            Position.X -= myShip.Speed;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, myShip.ShipTexture.Width, myShip.ShipTexture.Height);
            if (Position.X < -myShip.ShipTexture.Width)
            {
                Active = false;
            }
            if (myShip.Health <= 0)
            {
                Active = false;
                Player.Score += myShip.Points;
            }
            myShip.FireWeapon(gametime, projectilemanager, Position, false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myShip.ShipTexture, Position, Color.White);
        }
    }
}
