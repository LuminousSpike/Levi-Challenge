using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Solar
{
    public class GuiButton
    {
        private Vector2 Position, Scale, TextPosition;
        private Texture2D MainTexture, BorderTexture, HoverTexture, SelectedTexture;
        private GuiBox DefaultBox, HoverBox, SelectedBox;
        private int Width, Height, BWidth;
        private Color MColor, MColorSelected, BColor, FontColor = Color.White;
        private bool Textured = false, Selected = false;
        private string Text, TexturePath, FontPath;
        private SpriteFont Font;

        public Texture2D CurrentTexture;
        public bool IsSelected
        {
            get { return Selected; }
            set { Selected = value; }
        }

        public GuiButton(Vector2 position, string text, string texturePath, string fontPath)
        {
            Position = position;
            Textured = true;
            Text = text;
            TexturePath = texturePath;
            FontPath = fontPath;
        }

        public GuiButton(Vector2 position, int width, int height, int bWidth, Color mColor, Color mColorSelected, Color bColor, Color fontColor, string text, string fontPath)
        {
            Position = position;
            Width = width;
            Height = height;
            BWidth = bWidth;
            Scale = new Vector2(width - bWidth, height - bWidth);
            MColor = mColor;
            MColorSelected = mColorSelected;
            BColor = bColor;
            FontColor = fontColor;
            Text = text;
            FontPath = fontPath;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsdevice)
        {
            
            Font = content.Load<SpriteFont>(FontPath);
            if (MainTexture == null)
            {
                DefaultBox = new GuiBox(Position, Width, Height, BWidth, MColor, BColor, graphicsdevice);
                SelectedBox = new GuiBox(Position, Width, Height, BWidth, MColorSelected, BColor, graphicsdevice);
                TextPosition = new Vector2((int)(Position.X + (Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (Height / 2) - (Font.MeasureString(Text).Y / 2)));
            }
            else
            {
                TextPosition = new Vector2((int)(Position.X + (CurrentTexture.Width / 2) - (Font.MeasureString(Text).X / 2)), (int)(Position.Y + (CurrentTexture.Height / 2) - (Font.MeasureString(Text).Y / 2)));
                MainTexture = content.Load<Texture2D>(TexturePath);
                CurrentTexture = MainTexture;
            }
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
                spritebatch.DrawString(Font, Text, TextPosition, FontColor);
            }
            else
            {
                if (Selected == true)
                    SelectedBox.Draw(spritebatch);
                else
                    DefaultBox.Draw(spritebatch);
                spritebatch.DrawString(Font, Text, TextPosition, FontColor);
            }
        }
    }
}
