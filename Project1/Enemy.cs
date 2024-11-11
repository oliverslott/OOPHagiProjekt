using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Project1
{
    public abstract class Enemy : GameObject
    {
        
        
        protected static Random random = new Random();

        private Texture2D enemy;
        private Texture2D[] enemy_walk_sprites;
        private const int healthbar_width = 100;


        protected int maxHealth; // enemy health
        protected float damage; //enemy damage
        protected int currentHealth;



        protected static float NextFloat(float min, float max) //Enemy speed
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }

        private Player player;



        public Enemy(Player player)
        {
            this.player = player;
            currentHealth = maxHealth;

            scale = 2f;
            velocity = new Vector2(0, 1);
            RandomSpawn(); // Kald RandomSpawn her for at sætte en tilfældig startposition

            fps = 12; 
        }

        public override void LoadContent(ContentManager contentManager)
        {
            LoadWalkAnimation(contentManager);
            RandomSpawn();
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            FollowPlayer(gameTime);
            Animate(gameTime);
            Flip();
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            DrawHealthBar(spriteBatch);
        }

        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            int healthWidth = (int)(healthPercentage * healthbar_width);
            spriteBatch.Draw(Game1.healthTexture, new Rectangle((int)position.X - healthbar_width / 2, (int)position.Y+55, healthWidth, 5), Color.White);
        }

        private void Flip()
        {
            if (velocity.X > 0)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                spriteEffects = SpriteEffects.None;
            }
        } 

        private void FollowPlayer(GameTime gameTime)
        {
            

            Vector2 direction = player.Position - position;
            direction.Normalize();
            velocity = direction;

           
          
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
            //if (other is Enemy)
            //{
            //    shouldBeRemoved = true;
            //}
            if (other is Bullet)
            {
                currentHealth--;

                if (currentHealth <= 0)
                {
                    Game1.AddGameobjectToRemove(this);
                }
            }
            if (other is Player player)
            {
                player.Health -= damage; // Reducer HP med 
                Debug.WriteLine($"Player hit! Health is now: {player.Health}");


            }
        }

        public abstract void LoadWalkAnimation(ContentManager contentManager);
        public abstract void LoadAttackAnimation(ContentManager contentManager);
        public abstract void LoadHurtAnimation(ContentManager contentManager);
        public abstract void LoadDeathAnimation(ContentManager contentManager);

    }
    
}
