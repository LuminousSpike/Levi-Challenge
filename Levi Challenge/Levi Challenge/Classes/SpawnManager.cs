using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class SpawnManager
    {
        public EnemyManager enemyManager;

        int Level = 0;
        int SpawnCount = 0;
        int SpawnLimit = 10;
        bool AstroidSwarm = false;
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;
        Random random;

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content)
        {
            enemyManager = new EnemyManager(graphicsDevice);
            enemyManager.Initialize(content);

            previousSpawnTime = TimeSpan.Zero;
            enemySpawnTime = TimeSpan.FromSeconds(2.0f);
            random = new Random();
        }

        public void Update(GameTime gameTime, ProjectileManager projectilemanager)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                Spawn();
                previousSpawnTime = gameTime.TotalGameTime;

            }

            enemyManager.Update(gameTime, projectilemanager);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemyManager.Draw(spriteBatch);
        }

        private void Spawn()
        {
            if (SpawnCount < SpawnLimit)
            {
                if (AstroidSwarm == false)
                {
                    int rand = random.Next(100);
                    if (rand < 95)
                    {
                        bool spawned = false;
                        while (spawned == false)
                        {
                            int minLvl = random.Next(Level + 1);
                            foreach (Ship ship in XMLEngine.EnemyShips)
                            {
                                if ((ship.Level <= Level) && (ship.Level == minLvl))
                                {
                                    enemyManager.AddEnemy(random, ship);
                                    spawned = true;
                                }
                            }
                        }
                    }
                    else
                        enemyManager.AddAstroid(random);
                }
                else
                    enemyManager.AddAstroid(random);
                SpawnCount ++;
            }
            else
            {
                Level += 1;
                SpawnCount = 0;
                SpawnLimit += 5;

                if (Level >= 3)
                {
                    if (random.Next(100) <= 12)
                        AstroidSwarm = true;
                }
            }
        }
    }
}
