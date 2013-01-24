using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar
{
    public class GuiLifeBar
    {
        private Vector2 Position, Scale;
        private float MaxValue, MainAlpha, BorderAlpha;
        private Texture2D Texture;
        private int Width, Height, BWidth;
        private string TexturePath;
        private Color MColor, BColor;
        private GuiBox myBox;

        public GuiLifeBar(Vector2 position, float maxValue, string texturePath = null)
        {
            Position = position;
            MaxValue = maxValue;
            TexturePath = texturePath;
        }

        public GuiLifeBar(Vector2 position, float maxValue, int width, int height, int bWidth, Color mColor, Color bColor, float mainAlpha, float borderAlpha)
        {
            Position = position;
            MaxValue = maxValue;
            Width = width;
            Height = height;
            BWidth = bWidth;
            MColor = mColor;
            BColor = bColor;
            MainAlpha = mainAlpha;
            BorderAlpha = borderAlpha;
        }

        private void Initialize()
        {

        }

        // Loads any content required
        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            
            if (TexturePath != null)
            {
                Texture = content.Load<Texture2D>(TexturePath);
                Scale = new Vector2(1, 1);
            }
            else
                myBox = new GuiBox(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                
        }

        public void UnloadContent()
        {
            if (Texture == null)
                myBox.UnloadContent();
        }

        public void Update(int value)
        {
            if (Texture == null)
                myBox.UpdateScale(value);
            else
                Scale.X = (((float)Texture.Width / 100) * (value / (float)Texture.Width));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Texture != null)
            {
                // Only scales the image, need to code a way to render a partial image
                spritebatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            else
            {
                myBox.Draw(spritebatch);
            }
        }
    }
}
