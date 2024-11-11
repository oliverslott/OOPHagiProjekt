using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1
{
    /// <summary>
    /// BuffManager has the responsibility of applying buffs to the player and drawing the buff cards the player can click on.
    /// Made by Oliver
    /// </summary>
    public class BuffManager
    {
        private Player player;
        private List<Buff> activeBuffs = new List<Buff>();
        private List<BuffCardUI> buffCards = new List<BuffCardUI>();
        private bool isOpen = false;
        private KeyboardState prevKeyboardState;
        private Texture2D cardBackgroundSprite;
        private SpriteFont spriteFont;
        private const int spaceBetweenCards = 15;

        public bool IsOpen { get => isOpen; }

        public BuffManager(Player player)
        {
            this.player = player;
        }

        public void AddBuff(Buff buff)
        {
            buff.Apply(player);
            activeBuffs.Add(buff);
            Close();
        }

        public void LoadContent(ContentManager contentManager)
        {
            cardBackgroundSprite = contentManager.Load<Texture2D>("BuffCardBackground");
            spriteFont = contentManager.Load<SpriteFont>("font1");

            GenerateCards();
        }

        public void AddCardBuff(Buff buff)
        {
            buffCards.Add(new BuffCardUI(buff, this, cardBackgroundSprite, spriteFont));
        }

        public void GenerateCards()
        {
            buffCards.Clear();

            AddCardBuff(new ShootSpeedBuff());
            AddCardBuff(new MovementSpeedBuff());

            PositionCards();
        }

        //Dynamically positions cards based on how many options there are
        private void PositionCards()
        {
            //ui is hard
            int containerWidth = (cardBackgroundSprite.Width + spaceBetweenCards) * buffCards.Count;
            for (int i = 0; i < buffCards.Count; i++)
            {
                //dont look
                Vector2 cardPos = new Vector2( (cardBackgroundSprite.Width + spaceBetweenCards) * i + Game1.GetScreenSize().X/2 - containerWidth/2, Game1.GetScreenSize().Y / 2 - cardBackgroundSprite.Height / 2);
                buffCards[i].Position = cardPos;
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
