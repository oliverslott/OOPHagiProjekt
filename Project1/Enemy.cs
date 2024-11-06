using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace Project1
{
    public class Enemy : GameObject
    {
        
        
        protected static Random random = new Random();

        private Texture2D enemy;
        
        
        
        
        private static float NextFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }


        public Enemy()
        {
            scale = 1.0f;
            speed = NextFloat(100, 200);
            velocity = new Vector2(0, 1);
            position = new Vector2(NextFloat(0, - 150), 200); // ik rigtigt endnu


        }

        public override void LoadContent(ContentManager contentManager)
        {
            enemy = contentManager.Load<Texture2D>("enemytest");


            RandomSpawn();
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        protected void RandomSpawn()
        {
            //int randomX = random.Next(0, );
            //int randomY = random.Next(0, gameHeight);

            // Indstil enemy's position til den tilfældige placering
            //this.position = new Vector2(randomX, randomY);
        }

        public override void OnCollision(GameObject other)
        {
            //if (other is Player)
            //    shouldBeRemoved = true;
        }
    }
}
