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
            int rand = 450;
            if (rand < 300)
                enemyManager.AddEnemy(random, enemyManager.enemy1, 20, 40, 5f);
            else if (rand < 400)
                enemyManager.AddEnemy(random, enemyManager.enemy2, 30, 60, 3f);
            else if (rand < 460)
                enemyManager.AddAstroid(random);
        }
    }
}
