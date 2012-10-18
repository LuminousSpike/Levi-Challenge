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

        public Ship(int health, float shield, float speed)
        {
            Health = health;
            Shield = shield;
            Speed = speed;
        }
    }
}
