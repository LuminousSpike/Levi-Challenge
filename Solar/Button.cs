using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar
{
    public class Button
    {
        private Vector2 Position, Scale, TextPosition;
        private Texture2D MainTexture, BorderTexture, HoverTexture, SelectedTexture;
        private GuiBox DefaultBox, HoverBox, SelectedBox;
        private int Width, Height, BWidth;
        private Color MColor, BColor;
        private bool Textured = false;
        private float MainAlpha, BorderAlpha;
        private string Text;
        private SpriteFont Font;

        public Texture2D CurrentTexture;

        public Button(Vector2 position, string text)
        {
            Position = position;
            Textured = true;
            Text = text;
        }

        public Button(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor, float mainAlpha, float borderAlpha, string text)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            BColor = bColor;
            MainAlpha = mainAlpha;
            BorderAlpha = borderAlpha;
            Text = text;
        }

        public void LoadContent(GraphicsDevice graphicsdevice, Texture2D texture, SpriteFont font)
        {
            MainTexture = texture;
            CurrentTexture = texture;
            Font = font;
            if (MainTexture == null)
            {
                DefaultBox = new GuiBox(Position, Width, Height, BWidth, MColor, BColor, MainAlpha, BorderAlpha, graphicsdevice);
                TextPosition = new Vector2((int)(Position.X + (Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (Height / 2) - (Font.MeasureString(Text).Y / 2)));
            }
            else
                TextPosition = new Vector2((int)(Position.X + (CurrentTexture.Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (CurrentTexture.Height / 2) - (Font.MeasureString(Text).Y / 2)));
        }

        public void UnloadContent()
        {
            DefaultBox.UnloadContent();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (Textured == true)
            {
                spritebatch.Draw(CurrentTexture, Position, Color.White);
                spritebatch.DrawString(Font, Text, TextPosition, Color.White);
            }
            else
            {
                DefaultBox.Draw(spritebatch);
                spritebatch.DrawString(Font, Text, TextPosition, Color.White);
            }
        }
    }
}
