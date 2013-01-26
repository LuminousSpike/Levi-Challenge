using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class CollisionManager
    {
        public void Update(Player player, List<Enemy> Enemies, List<Asteroid> Astroids, List<Projectile> Projectiles)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                for (int ii = 0; ii < Enemies.Count; ii++)
                {
                    // Enemies
                    if (Projectiles[i].CollisionBox.Intersects(Enemies[ii].CollisionBox) && Projectiles[i].ShooterShip != Enemies[ii].myShip)
                    {
                        Projectiles[i].Active = false;
                        DamageShip(Projectiles[i].ProjectileDamage, Enemies[ii].myShip);
                    }
                    
                }

                // Player
                if (Projectiles[i].CollisionBox.Intersects(player.CollisionBox) && Projectiles[i].ShooterShip != player.myShip)
                {
                    Projectiles[i].Active = false;
                    DamageShip(Projectiles[i].ProjectileDamage, player.myShip);
                }

            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (player.CollisionBox.Intersects(Enemies[i].CollisionBox))
                {
                    Enemies[i].myShip.Health = 0;
                    DamageShip(50, player.myShip); // Replace hard coded value with a variable
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

            // Check for game over
            if (player.myShip.Health <= 0)
                Game1.gameState = Game1.GameState.GameOver;
        }

        private void DamageShip(int damageDealt, Ship ship)
        {
            float damage = damageDealt - ship.Shield;
            ship.Shield -= damageDealt;
            if (damage > 0)
            {
                ship.Health -= (int)damage;
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
