using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class EnemyManager
    {
        public Texture2D enemy1;
        public Texture2D enemy2;

        public List<Enemy> enemies;

        GraphicsDevice graphicsDevice { get; set; }

        public EnemyManager(GraphicsDevice graphicsdevice)
        {
            graphicsDevice = graphicsdevice;
        }

        public void Initialize(ContentManager content)
        {
            enemy1 = content.Load<Texture2D>("Enemy-1");
            enemy2 = content.Load<Texture2D>("Enemy-2");
            enemies = new List<Enemy>();
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

        public void AddEnemy(Random random, Texture2D enemyTexture, int health, int value, float enemymovespeed)
        {
            Vector2 position = new Vector2(graphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            Enemy enemy = new Enemy(enemyTexture, health, value, enemymovespeed);
            enemy.Initialize(position);
            enemies.Add(enemy);
        }

        private void UpdateEnemies(GameTime gameTime)
        {


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
