using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar
{
    public class GuiTextBox
    {
        private Vector2 Position, Scale, TextPosition;
        private Texture2D MainTexture, BorderTexture, HoverTexture, SelectedTexture;
        private GuiBox DefaultBox, HoverBox, SelectedBox;
        private int Width, Height, BWidth;
        private Color MColor, BColor;
        private bool Textured = false;
        private string Text, FontPath, TexturePath;
        private SpriteFont Font;

        public Texture2D CurrentTexture;

        public GuiTextBox(Vector2 position, string text, string texturePath = null)
        {
            Position = position;
            Textured = true;
            Text = text;
            TexturePath = texturePath;
        }

        public GuiTextBox(Vector2 position, int width, int height, int bWidth, Color mColor, Color bColor, string text, string fontPath)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            BColor = bColor;
            Text = text;
            FontPath = fontPath;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            Font = content.Load<SpriteFont>(FontPath);
            if (TexturePath != null)
            {
                MainTexture = content.Load<Texture2D>(TexturePath); ;
                CurrentTexture = MainTexture;
                TextPosition = new Vector2((int)(Position.X + (CurrentTexture.Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (CurrentTexture.Height / 2) - (Font.MeasureString(Text).Y / 2)));
            }
            else
            {
                DefaultBox = new GuiBox(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                TextPosition = new Vector2((int)(Position.X + (Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (Height / 2) - (Font.MeasureString(Text).Y / 2)));
            }
        }

        public void UnloadContent()
        {
            DefaultBox.UnloadContent();
        }

        public void UpdateText(string text, GraphicsDevice graphicsdevice)
        {
            Text = text;
            if (MainTexture == null)
            {
                DefaultBox = new GuiBox(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                TextPosition = new Vector2((int)(Position.X + (Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (Height / 2) - (Font.MeasureString(Text).Y / 2)));
            }
            else
                TextPosition = new Vector2((int)(Position.X + (CurrentTexture.Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (CurrentTexture.Height / 2) - (Font.MeasureString(Text).Y / 2)));
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
