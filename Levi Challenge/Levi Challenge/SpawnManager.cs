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
                previousSpawnTime = gameTime.TotalGameTime;
                if (gameTime.TotalGameTime < TimeSpan.FromSeconds(30))
                    enemyManager.AddEnemy(random, enemyManager.enemy1, 20, 40, 5f);
                else
                    enemyManager.AddEnemy(random, enemyManager.enemy2, 30, 60, 3f);
            }

            enemyManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemyManager.Draw(spriteBatch);
        }
    }
}
