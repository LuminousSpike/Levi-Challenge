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

        Texture2D AstroidTexture1;
        Texture2D AstroidTexture2;
        Texture2D AstroidTexture3;
        Texture2D AstroidTexture4;

        public List<Enemy> Enemies;
        public List<Astroid> Astroids;

        GraphicsDevice graphicsDevice { get; set; }

        public EnemyManager(GraphicsDevice graphicsdevice)
        {
            graphicsDevice = graphicsdevice;
        }

        public void Initialize(ContentManager content)
        {
            LoadContent(content);
            enemy1 = content.Load<Texture2D>("Enemy-1");
            enemy2 = content.Load<Texture2D>("Enemy-2");
            Enemies = new List<Enemy>();
            Astroids = new List<Astroid>();
        }

        public void Update(GameTime gameTime)
        {
            UpdateEnemies(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Astroids.Count; i++)
            {
                Astroids[i].Draw(spriteBatch);
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(spriteBatch);
            }
        }

        public void AddEnemy(Random random, Texture2D enemyTexture, int health, int value, float enemymovespeed)
        {
            Vector2 position = new Vector2(graphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            Enemy enemy = new Enemy(enemyTexture, health, value, enemymovespeed);
            enemy.Initialize(position);
            Enemies.Add(enemy);
        }

        public void AddAstroid(Random random)
        {
            Astroid astroid = new Astroid(AstroidTexture1, AstroidTexture2, AstroidTexture3, AstroidTexture4);
            astroid.Initialize();
            astroid.Position = new Vector2(graphicsDevice.Viewport.Width + astroid.Texture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            Astroids.Add(astroid);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            for (int i = Astroids.Count - 1; i >= 0; i--)
            {
                Astroids[i].Update(gameTime);

                if (Astroids[i].Active == false)
                {
                    Astroids.RemoveAt(i);
                }
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);

                if (Enemies[i].Active == false)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            AstroidTexture1 = content.Load<Texture2D>("Astroid-4");
            AstroidTexture2 = content.Load<Texture2D>("Astroid-4");
            AstroidTexture3 = content.Load<Texture2D>("Astroid-4");
            AstroidTexture4 = content.Load<Texture2D>("Astroid-4");
        }
    }
}
