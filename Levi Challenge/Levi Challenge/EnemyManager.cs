using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class EnemyManager
    {
        Texture2D enemy1;
        Enemy enemy2;

        public List<Enemy> enemies;
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;
        Random random;

        GraphicsDevice graphicsDevice { get; set; }

        public EnemyManager(GraphicsDevice graphicsdevice)
        {
            graphicsDevice = graphicsdevice;
        }

        public void Initialize(ContentManager content)
        {
            enemy1 = content.Load<Texture2D>("Enemy-1");

            enemies = new List<Enemy>();
            previousSpawnTime = TimeSpan.Zero;
            enemySpawnTime = TimeSpan.FromSeconds(2.0f);
            random = new Random();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEnemies(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
        }

        private void AddEnemy()
        {
            Vector2 position = new Vector2(graphicsDevice.Viewport.Width + enemy1.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            // Sub in with Spawn Manager
            Enemy enemy = new Enemy(enemy1, 16, 40, 4f);
            enemy.Initialize(position);
            enemies.Add(enemy);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                AddEnemy();
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
                }
            }
        }
    }
}
