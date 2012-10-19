using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Levi_Challenge
{
    class Ship
    {
        public int Health;
        public float Shield;
        public float Speed;
        public Weapon[] MountPoints;

        private int HardPoints;

        public Ship(int health, float shield, float speed, int hardpoints)
        {
            Health = health;
            Shield = shield;
            Speed = speed;
            HardPoints = hardpoints;

            MountPoints = new Weapon[hardpoints];
        }

        public void MountWeapon(int mountpoint, Weapon weapon)
        {
            MountPoints[mountpoint] = weapon;
        }

        public void FireWeapon(GameTime gameTime, ProjectileManager projectileManager, Vector2 position)
        {
            for (int mPoint = 0; mPoint < MountPoints.Length; mPoint++)
                MountPoints[mPoint].fire(gameTime, projectileManager, position);
        }
    }
}
