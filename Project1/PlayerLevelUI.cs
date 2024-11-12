using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Has the responsibility of reading the player level from the player class and drawing it on screen
    /// Made by Oliver
    /// </summary>
    public class PlayerLevelUI
    {
        private Player player;
        private SpriteFont spriteFont;

        public PlayerLevelUI(Player player)
        {
            this.player = player;
        }

        public void LoadContent(ContentManager contentManager)
        {
            spriteFont = contentManager.Load<SpriteFont>("font1");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string levelText = $"Level: {player.Level}";
            Vector2 stringSize = spriteFont.MeasureString(levelText);
            spriteBatch.DrawString(spriteFont, levelText, new Vector2(Game1.GetScreenSize().X - stringSize.X, 0), Color.White);

            string xpText = $"XP: {player.Xp-player.Level*100}/100";
            Vector2 stringSize2 = spriteFont.MeasureString(xpText);
            spriteBatch.DrawString(spriteFont, xpText, new Vector2(Game1.GetScreenSize().X - stringSize2.X, stringSize.Y), Color.White);
        }
    }
}
