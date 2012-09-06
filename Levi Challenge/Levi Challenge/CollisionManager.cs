using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class CollisionManager
    {
        public void Update(Player player, List<Enemy> Enemies, List<Projectile> Projectiles)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (player.CollisionBox.Intersects(Enemies[i].CollisionBox))
                {
                    Enemies[i].Health = 0;
                }
                for (int ii = 0; ii < Projectiles.Count; ii++)
                {
                    if (Projectiles[ii].CollisionBox.Intersects(Enemies[i].CollisionBox))
                    {
                        Projectiles[ii].Active = false;
                        Enemies[i].Health -= Projectiles[ii].Damage;
                    }
                }
            }
        }
    }
}
