using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    // Hussein
    public static class Scoreboard
    {
        private static int enemyKillCount;
        private static SpriteFont font;
        private static Vector2 position;

        public static void Initialize(SpriteFont spriteFont, Vector2 pos)
        {
            font = spriteFont;
            position = pos;
            enemyKillCount = 0;
        }

        public static void AddKill()
        {
            enemyKillCount++;
        }

        public static void ResetScore()
        {
            enemyKillCount = 0;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + enemyKillCount, position, Color.White);
        }
    }
}
