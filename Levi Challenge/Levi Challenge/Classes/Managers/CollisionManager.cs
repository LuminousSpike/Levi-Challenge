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
                    if (IntersectPixels(Projectiles[ii].myTransform, Projectiles[ii].Width, Projectiles[ii].Height, Projectiles[ii].TextureData, Astroids[i].myTransform, Astroids[i].Width, Astroids[i].Height, Astroids[i].TextureData))
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

        private bool IntersectPixels(Matrix transformA, int widthA, int heightA, Color[] dataA, Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which reansforms from A's local space into world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Calculate this pixel's location in B
                    Vector2 positionInB = Vector2.Transform(new Vector2(xA, yA), transformAToB);

                    // Round to the nearest pixel
                    int xB = (int)Math.Round(positionInB.X);
                    int yB = (int)Math.Round(positionInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent
                        if (colorA.A != 0 && colorB.A != 0)
                            return true;
                    }
                }
            }

            // No intersection has been found
            return false;
        }
    }
}
