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

        private static int Level = 0, SpawnCount = 0, SpawnLimit = 10;
        private bool AstroidSwarm = false;
        private TimeSpan enemySpawnTime, previousSpawnTime;
        private Random random;
        private XMLEngine xmlEngine;

        public static int WaveCount
        {
            get
            {
                return Level;
            }
        }

        public SpawnManager(XMLEngine xmlengine)
        {
            xmlEngine = xmlengine;
        }

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Level = 0;
            SpawnCount = 0;
            SpawnLimit = 10;

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
                            foreach (Ship ship in xmlEngine.EnemyShips)
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
