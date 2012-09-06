using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Levi_Challenge
{
    class SpawnManager
    {
        EnemyManager enemyManager;

        public void Initialize(GraphicsDevice graphicsDevice, ContentManager content)
        {
            enemyManager = new EnemyManager(graphicsDevice);
            enemyManager.Initialize(content);
        }

        public void Update(GameTime gameTime)
        {
            enemyManager.Update(gameTime);
        }
    }
}
