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



        /// <summary> // Malthe
        /// Initializes a new instance of the Enemy class with a specified player reference and maximum health.
        /// Sets initial values for scale, velocity, random position, and animation speed.
        /// </summary>
        /// <param name="player">The player instance that the enemy interacts with or targets.</param>
        /// <param name="maxHealth">The maximum health for the enemy.</param>
        public Enemy(Player player, int maxHealth)
        {
            // Set player reference and initialize health values
            this.player = player;
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;

            // Set scale for enemy size and initial velocity
            scale = 2f;
            velocity = new Vector2(0, 1);

            // Randomly set the initial position within the game area
            RandomSpawn();

            // Set frames per second for enemy animation
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

        /// <summary> // Malthe
        /// Sets the enemy's position to a random location within the game area dimensions.
        /// This helps in generating random spawn points for enemies.
        /// </summary>
        protected void RandomSpawn()
        {
            int gameWidth = 1280;  // Width of the game area
            int gameHeight = 720;  // Height of the game area

            // Generate random x and y coordinates within the game area
            float randomX = NextFloat(0, gameWidth);
            float randomY = NextFloat(0, gameHeight);

            // Set enemy's position to the randomly generated location
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
                    player.AddXp(10); //TODO: Should different enemies have different xp drop?
                    Game1.AddGameobjectToRemove(this);
                }
            }
            if (other is Player)
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
