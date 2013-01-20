using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Levi_Challenge
{
    class Ship
    {
        public string Name, ShipTexturePath, ShipType, AI;
        public int Health, WeaponClass, Armour, Cost, Level, Points, Hardpoints;
        public float Shield, Speed;
        public Weapon[] myHardpoints;
        public Texture2D ShipTexture;
        public Vector2 position;
        
        private bool acceleratingX = false, acceleratingY = false;
        private Vector2 mySpeed = new Vector2();

        // Player Constructor
        public Ship(string name, int health, float shield, float speed, int weaponclass, int armour, string texture, int cost, string shiptype, int hardpoints)
        {
            Name = name;
            Health = health;
            Shield = shield;
            Speed = speed;
            WeaponClass = weaponclass;
            Armour = armour;
            ShipTexturePath = texture;
            Cost = cost;
            ShipType = shiptype;

            Hardpoints = hardpoints;
            myHardpoints = new Weapon[hardpoints];
        }

        // Enemy Constructor
        public Ship(string name, int health, float shield, float speed, int weaponclass, int armour, string texture, int level, int points, string ai, int hardpoints)
        {
            Name = name;
            Health = health;
            Shield = shield;
            Speed = speed;
            WeaponClass = weaponclass;
            Armour = armour;
            ShipTexturePath = texture;
            Level = level;
            Points = points;
            AI = ai;

            Hardpoints = hardpoints;
            myHardpoints = new Weapon[hardpoints];
        }

        public Ship ShallowCopy()
        {
            Ship other = (Ship)this.MemberwiseClone();
            other.myHardpoints = new Weapon[Hardpoints];
            for (int i = 0; i < other.Hardpoints; i++)
            {
                other.myHardpoints[i] = this.myHardpoints[i].ShallowCopy();
            }
            return other;
        }

        public void MountWeapon(int hardpoint, Weapon weapon)
        {
            myHardpoints[hardpoint] = weapon.ShallowCopy();
            myHardpoints[hardpoint].OffsetX = 0;
            myHardpoints[hardpoint].OffsetY = 0;
        }

        public void FireWeapon(GameTime gameTime, ProjectileManager projectileManager, Vector2 position, bool isplayer)
        {
            for (int mPoint = 0; mPoint < Hardpoints; mPoint++)
                if (this.myHardpoints[mPoint] != null)
                    this.myHardpoints[mPoint].fire(gameTime, projectileManager, position + new Vector2(ShipTexture.Width / 2, ShipTexture.Height / 2), isplayer, this);
        }

        public void Move(float XSpeed, float YSpeed)
        {
            // To stop vectors adding
            if (XSpeed != 0)
            {
                if (mySpeed.X < XSpeed)
                {
                    mySpeed.X += (Speed / 5);
                }
                if (mySpeed.X > XSpeed)
                {
                    mySpeed.X -= (Speed / 5);
                }
                acceleratingX = true;
            }
            if (YSpeed != 0)
            {
                if (mySpeed.Y < YSpeed)
                {
                    mySpeed.Y += (Speed / 5);
                }
                if (mySpeed.Y > YSpeed)
                {
                    mySpeed.Y -= (Speed / 5);
                }
                acceleratingY = true;
            }
        }

        public void Deaccelerate(bool X, bool Y)
        {
            if (X == true)
            {
                if (mySpeed.X < 0.8f && mySpeed.X > -0.8f)
                {
                    mySpeed.X = 0f;
                }
                if (mySpeed.X > 0)
                {
                    mySpeed.X -= (Speed / 5);
                }
                if (mySpeed.X < 0)
                {
                    mySpeed.X += (Speed / 5);
                }
            }

            if (Y == true)
            {
                if (mySpeed.Y < 0.8f && mySpeed.Y > -0.8f)
                {
                    mySpeed.Y = 0f;
                }
                if (mySpeed.Y > 0)
                {
                    mySpeed.Y -= (Speed / 5);
                }
                if (mySpeed.Y < 0)
                {
                    mySpeed.Y += (Speed / 5);
                }
            }
        }

        public void Update()
        {
            position += mySpeed;
            if (acceleratingX == true)
                acceleratingX = false;
            else
                Deaccelerate(true, false);

            if (acceleratingY == true)
                acceleratingY = false;
            else
                Deaccelerate(false, true);
        }
    }
}
