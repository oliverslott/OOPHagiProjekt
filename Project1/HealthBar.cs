using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class HealthBar
    {
        private Texture2D texture;
        private Vector2 position;
        private int width;
        private int height;
        private int maxHealth;
        public int currentHealth;

        public HealthBar(Texture2D texture, Vector2 position, int width, int height, int maxHealth)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
        }

        public void SetHealth(int health)
        {
            currentHealth = MathHelper.Clamp(health, 0, maxHealth);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            int healthBarCurrentWidth = (int)(width * healthPercentage);

            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, healthBarCurrentWidth, height), Color.White);
        }
    }
}
