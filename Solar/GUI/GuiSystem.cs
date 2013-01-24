using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Solar
{
    // System for making the GUI easier to use. Handles Loading, Unloading, and Drawing each Object.
    public class GuiSystem
    {
        // Lists for each GUI Object
        private List<GuiBox> GuiBoxList;
        private List<GuiButton> GuiButtonList;
        private List<GuiLifeBar> GuiLifeBarList;
        private List<GuiTextBox> GuiTextBoxList;

        private KeyboardState CurrentKeyboardState, PreviousKeyboardState;
        private int ButtonIndex = 0;

        public int GuiButtonCount
        {
            get { return GuiButtonList.Count; }
        }

        public int GuiButtonIndex
        {
            get { return ButtonIndex; }
        }

        // Initializes the list, here incase an Object requires something at runtime in the future.
        public void Initialize()
        {
            GuiBoxList = new List<GuiBox>();
            GuiButtonList = new List<GuiButton>();
            GuiLifeBarList = new List<GuiLifeBar>();
            GuiTextBoxList = new List<GuiTextBox>();
        }

        // Anything which needs to load content or do something at content load time.
        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            foreach (GuiButton item in GuiButtonList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            foreach (GuiLifeBar item in GuiLifeBarList)
            {
                item.LoadContent(content, graphicsDevice);
            }
            foreach (GuiTextBox item in GuiTextBoxList)
            {
                item.LoadContent(content, graphicsDevice);
            }
        }

        // Anything which uses textures generated at runtime or in the future.
        public void UnloadContent()
        {
            foreach (GuiBox item in GuiBoxList)
            {
                item.UnloadContent();
            }

            foreach (GuiButton item in GuiButtonList)
            {
                item.UnloadContent();
            }

            foreach (GuiLifeBar item in GuiLifeBarList)
            {
                item.UnloadContent();
            }

            foreach (GuiTextBox item in GuiTextBoxList)
            {
                item.UnloadContent();
            }
        }

        // The Add function is used for adding any GUI Object to the lists above.
        public void Add(GuiBox guiBox)
        {
            GuiBoxList.Add(guiBox);
        }

        public void Add(GuiButton guiButton)
        {
            GuiButtonList.Add(guiButton);
        }

        public void Add(GuiLifeBar guiLifeBar)
        {
            GuiLifeBarList.Add(guiLifeBar);
        }

        public void Add(GuiTextBox guiTextBox)
        {
            GuiTextBoxList.Add(guiTextBox);
        }

        // Any update code which is generic for all Objects of a type.
        public void Update()
        {
            // Button Management
            CurrentKeyboardState = Keyboard.GetState();
            if ((CurrentKeyboardState.IsKeyUp(Keys.Down) && PreviousKeyboardState.IsKeyDown(Keys.Down)) || (CurrentKeyboardState.IsKeyUp(Keys.S) && PreviousKeyboardState.IsKeyDown(Keys.S)))
            {
                if (ButtonIndex < GuiButtonCount - 1)
                    ButtonIndex++;

                ButtonIndexUpdate(ButtonIndex);
            }

            if ((CurrentKeyboardState.IsKeyUp(Keys.Up) && PreviousKeyboardState.IsKeyDown(Keys.Up)) || (CurrentKeyboardState.IsKeyUp(Keys.W) && PreviousKeyboardState.IsKeyDown(Keys.W)))
            {
                if (ButtonIndex > 0)
                    ButtonIndex--;

                ButtonIndexUpdate(ButtonIndex);
            }
            PreviousKeyboardState = CurrentKeyboardState;
        }

        public void ButtonIndexUpdate(int index)
        {
            for (int i = 0; i < GuiButtonList.Count; i++)
            {
                if (i == index)
                    GuiButtonList[i].IsSelected = true;
                else
                    GuiButtonList[i].IsSelected = false;
            }
        }

        // Loops through each Object to execute the draw code.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GuiBox item in GuiBoxList)
            {
                item.Draw(spriteBatch);
            }

            foreach (GuiButton item in GuiButtonList)
            {
                item.Draw(spriteBatch);
            }

            foreach (GuiLifeBar item in GuiLifeBarList)
            {
                item.Draw(spriteBatch);
            }

            foreach (GuiTextBox item in GuiTextBoxList)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
