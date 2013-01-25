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
