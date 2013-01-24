using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class CollisionManager
    {
        public void Update(Player player, List<Enemy> Enemies, List<Astroid> Astroids, List<Projectile> Projectiles)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (player.CollisionBox.Intersects(Enemies[i].CollisionBox))
                {
                    Enemies[i].myShip.Health = 0;
                }
                for (int ii = 0; ii < Projectiles.Count; ii++)
                {
                    // Player
                    if (Projectiles[ii].CollisionBox.Intersects(player.CollisionBox) && Projectiles[ii].ShooterShip != player.myShip)
                    {
                        Projectiles[ii].Active = false;
                        player.myShip.Health -= Projectiles[ii].ProjectileDamage;
                    }

                    // Enemies
                    if (Projectiles[ii].CollisionBox.Intersects(Enemies[i].CollisionBox) && Projectiles[ii].ShooterShip != Enemies[i].myShip)
                    {
                        Projectiles[ii].Active = false;
                        Enemies[i].myShip.Health -= Projectiles[ii].ProjectileDamage;
                    }
                }
            }

            for (int i = 0; i < Astroids.Count; i++)
            {
                if (player.CollisionBox.Intersects(Astroids[i].CollisionBox))
                {
                    Astroids[i].Health = 0;
                }
                for (int ii = 0; ii < Projectiles.Count; ii++)
                {
                    if (Projectiles[ii].CollisionBox.Intersects(Astroids[i].CollisionBox))
                    {
                        Projectiles[ii].Active = false;
                        Astroids[i].Health -= Projectiles[ii].ProjectileDamage;
                    }
                }
            }
        }
    }
}
