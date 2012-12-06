using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class EnemyManager
    {
        public List<Texture2D> EnemyTexture;

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
            EnemyTexture = new List<Texture2D>();

            Enemies = new List<Enemy>();
            Astroids = new List<Astroid>();
        }

        public void Update(GameTime gameTime, ProjectileManager projectilemanager)
        {
            UpdateEnemies(gameTime, projectilemanager);
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

        public void AddEnemy(Random random, Ship ship)
        {
            Enemy enemy = new Enemy(ship);
            Vector2 position = new Vector2(graphicsDevice.Viewport.Width + enemy.myShip.ShipTexture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            enemy.LoadContent(position);
            Enemies.Add(enemy);
        }

        public void AddAstroid(Random random)
        {
            Astroid astroid = new Astroid(AstroidTexture1, AstroidTexture2, AstroidTexture3, AstroidTexture4);
            astroid.Initialize(random.Next(1, 5));
            astroid.Position = new Vector2(graphicsDevice.Viewport.Width + astroid.Texture.Width / 2, random.Next(100, graphicsDevice.Viewport.Height - 100));
            Astroids.Add(astroid);
        }

        public void SplitAstroid(int size, Vector2 position)
        {
            Astroid astroid = new Astroid(AstroidTexture1, AstroidTexture2, AstroidTexture3, AstroidTexture4);
            astroid.Initialize(size);
            astroid.Position = position;
            Astroids.Add(astroid);
        }

        private void UpdateEnemies(GameTime gameTime, ProjectileManager projectilemanager)
        {
            for (int i = Astroids.Count - 1; i >= 0; i--)
            {
                if (Astroids[i].Splitting == true)
                {
                    Random random = new Random();
                    SplitAstroid(Astroids[i].Size, Astroids[i].Position + new Vector2(0, -25 * Astroids[i].Size));
                    Astroids[i].Position += new Vector2(0, 25 * Astroids[i].Size);
                    Astroids[i].Splitting = false;
                }

                Astroids[i].Update(gameTime);

                if (Astroids[i].Active == false)
                {
                    Astroids.RemoveAt(i);
                }

            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime, projectilemanager);

                if (Enemies[i].Active == false)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            AstroidTexture1 = content.Load<Texture2D>(@"Astroids\Astroid-1");
            AstroidTexture2 = content.Load<Texture2D>(@"Astroids\Astroid-2");
            AstroidTexture3 = content.Load<Texture2D>(@"Astroids\Astroid-3");
            AstroidTexture4 = content.Load<Texture2D>(@"Astroids\Astroid-4");
        }
    }
}
