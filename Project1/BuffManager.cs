using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Project1
{
    public class BuffManager
    {
        private Player player;
        private List<Buff> activeBuffs = new List<Buff>();
        private List<BuffCardUI> buffCards = new List<BuffCardUI>();
        private bool isOpen = false;
        private KeyboardState prevKeyboardState;

        public bool IsOpen { get => isOpen; }

        public BuffManager(Player player)
        {
            this.player = player;
            buffCards.Add(new BuffCardUI(new ShootSpeedBuff(), this));
        }

        public void AddBuff(Buff buff)
        {
            buff.Apply(player);
            activeBuffs.Add(buff);
            Close();
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach(BuffCardUI buffCardUI in buffCards)
            {
                buffCardUI.LoadContent(contentManager); //TODO: Might be more performant to load all content at once instead of every single one?
            }
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            if (!isOpen) return;

            foreach (BuffCardUI buffCardUI in buffCards)
            {
                buffCardUI.Draw(spriteBatch);
            }
        }

        public void UpdateUI(GameTime gameTime)
        {
            OpenWithKeyPress();

            if (!isOpen) return;

            foreach (BuffCardUI buffCardUI in buffCards)
            {
                buffCardUI.Update(gameTime);
            }
        }

        private void OpenWithKeyPress()
        {
            //For debug purposes. Should be shown after clearing a wave in the future.
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.H) && prevKeyboardState.IsKeyUp(Keys.H))
            {
                if (isOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }

            prevKeyboardState = currentState;
        }

        public void Open()
        {
            isOpen = true;
        }

        public void Close()
        {
            isOpen = false;
            Mouse.SetCursor(MouseCursor.Arrow);
        }
    }
}
