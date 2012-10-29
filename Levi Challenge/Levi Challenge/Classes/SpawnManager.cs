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

        int Level = 1;
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

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                Spawn();
                previousSpawnTime = gameTime.TotalGameTime;

            }

            enemyManager.Update(gameTime);
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
                    int rand = random.Next(500);
                    if (rand < 300)
                        enemyManager.AddEnemy(random, XMLEngine.EnemyShips[0]);
                    else if (rand < 400)
                        enemyManager.AddEnemy(random, XMLEngine.EnemyShips[0]);
                    else if (rand < 460)
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
