using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar
{
    public class GuiBox
    {
        private Vector2 Position, Scale, bVertical, bHorizontal;
        private Texture2D MainTexture, BorderTexture;
        private int Width, Height, BWidth;
        private Color MColor, BColor;
        private float Alpha;

        public GuiBox(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor, float mainAlpha, float borderAlpha, GraphicsDevice graphicsdevice)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            BColor = bColor;
            Alpha = mainAlpha;
            MColor.A = (byte)(255 / 100 * mainAlpha);
            BColor.A = (byte)(255 / 100 * borderAlpha);

            // Set border positions
            bVertical = new Vector2(bWidth, height - BWidth);
            bHorizontal = new Vector2(width + BWidth, bWidth);

            // Create Texture
            Color myMainColor = new Color(100f, 100f, 100f, Alpha);
            MainTexture = new Texture2D(graphicsdevice, 1, 1);
            MainTexture.SetData(new[] { myMainColor });
            Color myBorderColor = new Color(100f, 100f, 100f, Alpha);
            BorderTexture = new Texture2D(graphicsdevice, 1, 1);
            BorderTexture.SetData(new[] { myBorderColor });
        }

        public void UnloadContent()
        {
            MainTexture.Dispose();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // Draw borders
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y), null, BColor, 0f, Vector2.Zero, bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X + Width - BWidth, Position.Y), null, BColor, 0f, Vector2.Zero, bVertical, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y - BWidth), null, BColor, 0f, Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);
            spritebatch.Draw(MainTexture, new Vector2(Position.X - BWidth, Position.Y + Height - BWidth), null, BColor, 0f, Vector2.Zero, bHorizontal, SpriteEffects.None, 0f);

            // Draw fill
            spritebatch.Draw(MainTexture, Position, null, MColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
