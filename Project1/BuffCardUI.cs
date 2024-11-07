﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Project1
{
    /// <summary>
    /// I don't think UI stuff should be gameobjects, because gameobject superclass simply has too many things that UI stuff don't need
    /// This sadly causes some duplicated code like LoadContent and Draw
    /// </summary>
    public class BuffCardUI
    {
        private Texture2D backgroundSprite;
        private string description;
        private Vector2 position;
        private bool hovering = false;
        private Buff buff;
        private SpriteFont spriteFont;
        private BuffManager buffManager;

        public BuffCardUI(Buff buff, BuffManager buffManager)
        {
            this.buff = buff;
            this.buffManager = buffManager;

            description = buff.Description;
        }

        public void LoadContent(ContentManager contentManager)
        {
            backgroundSprite = contentManager.Load<Texture2D>("BuffCardBackground");
            position = new Vector2(Game1.GetScreenSize().X / 2 - backgroundSprite.Width / 2, Game1.GetScreenSize().Y / 2 - backgroundSprite.Height / 2);
            spriteFont = contentManager.Load<SpriteFont>("font1");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundSprite, position, Color.White);
            Vector2 stringSize = spriteFont.MeasureString(description);
            spriteBatch.DrawString(spriteFont, description, new Vector2(position.X + backgroundSprite.Width/2, position.Y + backgroundSprite.Height/2), Color.White, 0, stringSize/2, 1f, SpriteEffects.None, 0);
        }
        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            Rectangle backgroundRect = new Rectangle((int)position.X, (int)position.Y, backgroundSprite.Width, backgroundSprite.Height);

            hovering = mouseRect.Intersects(backgroundRect);

            if (hovering)
            {
                Mouse.SetCursor(MouseCursor.Hand);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Click();
                }
            }
            else
            {
                Mouse.SetCursor(MouseCursor.Arrow);
            }
        }

        private void Click()
        {
            Debug.WriteLine("Added buff");
            buffManager.AddBuff(buff);
        }
    }
}