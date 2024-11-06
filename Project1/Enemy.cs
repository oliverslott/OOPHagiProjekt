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

        private Player player;


        public Enemy(Player player)
        {
            this.player = player;
            
            scale = .2f;
            speed = NextFloat(25, 50);
            velocity = new Vector2(0, 1);
            RandomSpawn(); // Kald RandomSpawn her for at sætte en tilfældig startposition




        }

        public override void LoadContent(ContentManager contentManager)
        {
            enemy = contentManager.Load<Texture2D>("enemytest");

            Sprite = enemy;

            RandomSpawn();
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            FollowPlayer(gameTime);
        }

        private void FollowPlayer(GameTime gameTime)
        {
            ;

            Vector2 direction = player.Position - position;
            direction.Normalize();
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

           
          
        }

        protected void RandomSpawn()
        {
            // Antag spillets bredde og højde; disse værdier skal ændres til spillets faktiske dimensioner
            int gameWidth = 1280;  // Skift til bredden af din spilskærm
            int gameHeight = 720; // Skift til højden af din spilskærm

            // Generer tilfældige x- og y-koordinater inden for spilområdet
            float randomX = NextFloat(0, gameWidth);
            float randomY = NextFloat(0, gameHeight);

            // Sæt enemy's position til den tilfældige placering
            position = new Vector2(randomX, randomY);
        }

        public override void OnCollision(GameObject other)
        {
            
        }
    }
}
