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
            for (int i = 0; i < Projectiles.Count; i++)
            {
                for (int ii = 0; ii < Enemies.Count; ii++)
                {
                    // Enemies
                    if (Projectiles[i].CollisionBox.Intersects(Enemies[ii].CollisionBox) && Projectiles[i].ShooterShip != Enemies[ii].myShip)
                    {
                        Projectiles[i].Active = false;
                        Enemies[ii].myShip.Health -= Projectiles[i].ProjectileDamage;
                    }

                    if (player.CollisionBox.Intersects(Enemies[ii].CollisionBox))
                    {
                        Enemies[ii].myShip.Health = 0;
                    }
                }

                // Player
                if (Projectiles[i].CollisionBox.Intersects(player.CollisionBox) && Projectiles[i].ShooterShip != player.myShip)
                {
                    Projectiles[i].Active = false;
                    float damage = Projectiles[i].ProjectileDamage - player.myShip.Shield;
                    player.myShip.Shield -= Projectiles[i].ProjectileDamage;
                    if (damage > 0)
                    {
                        player.myShip.Health -= (int)damage;
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
                    if (IntersectPixels(Projectiles[ii].CollisionBox, Projectiles[ii].TextureData, Astroids[i].CollisionBox, Astroids[i].TextureData))
                    {
                        Projectiles[ii].Active = false;
                        Astroids[i].Health -= Projectiles[ii].ProjectileDamage;
                    }
                }
            }
        }

        private bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) + (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) + (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // Then the intersection has been found
                        return true;
                    }
                }
            }

            // No intersection has been found
            return false;
        }
    }
}
