using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Levi_Challenge
{
    class Ship
    {
        public string Name;
        public int Health;
        public float Shield;
        public float Speed;
        public int WeaponClass;
        public int Armour;
        public string ShipTexturePath;
        public int Cost;
        public string ShipType;
        public int Level;
        public int Points;
        public string AI;
        public int Hardpoints;
        public Weapon[] myHardpoints;
        public Texture2D ShipTexture;

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
    }
}
