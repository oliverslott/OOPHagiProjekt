using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Project1
{
    internal class Bullet : GameObject
    {
        private const float lifetimeInSeconds = 2f;
        private float currentLifetime = 0;
        public Bullet(Texture2D texture2D, Vector2 position, Vector2 direction)
        {
            Sprite = texture2D;
            Position = position;
            speed = 1000;
            direction.Normalize();
            velocity = direction;
        }
        public override void LoadContent(ContentManager contentManager)
        {
            //Gets loaded through the constructor
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Debug.WriteLine("Bullet hit enemy!");
                //TODO

                Game1.AddGameobjectToRemove(this);
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            currentLifetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(currentLifetime >= lifetimeInSeconds)
            {
                Game1.AddGameobjectToRemove(this);
            }
            Move(gameTime);
        }
    }
}
