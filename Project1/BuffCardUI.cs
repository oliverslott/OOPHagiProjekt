using Microsoft.Xna.Framework;
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
        private BuffManager buffManager;
        private SpriteFont spriteFont;
        public Vector2 Position { get => position; set => position = value; }

        public BuffCardUI(Buff buff, BuffManager buffManager, Texture2D backgroundSprite, SpriteFont spriteFont)
        {
            this.buff = buff;
            this.buffManager = buffManager;
            this.backgroundSprite = backgroundSprite;
            this.spriteFont = spriteFont;

            description = buff.Description;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundSprite, position, Color.White);
            Vector2 stringSize = spriteFont.MeasureString(description);
            spriteBatch.DrawString(spriteFont, description, new Vector2(position.X + backgroundSprite.Width/2, position.Y + backgroundSprite.Height/2), Color.White, 0, stringSize/2, 0.38f, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            Rectangle backgroundRect = new Rectangle((int)position.X, (int)position.Y, backgroundSprite.Width, backgroundSprite.Height);

            hovering = mouseRect.Intersects(backgroundRect);

            if (hovering)
            {
                //Mouse.SetCursor(MouseCursor.Hand); Dropping the cursor change for now, they currently fight each other
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Click();
                }
            }
            else
            {
                //Mouse.SetCursor(MouseCursor.Arrow);
            }
        }

        private void Click()
        {
            Debug.WriteLine("Added buff");
            buffManager.AddBuff(buff);
        }
    }
}
