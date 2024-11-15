using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project1
{
    public class Game1 : Game
    {
        private Random rnd;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont textFont;

        private List<GameObject> gameObjects;

        private static List<GameObject> gameObjectsToRemove;
        private static List<GameObject> gameObjectsToAdd;

        private float spawnInterval;
        private float timeBetweenInterval = 1f;

        private static Vector2 screenSize;

        private Texture2D collisionTexture;

        private bool gameOver = false;


        private HealthBar playerHealthBar;

        private Texture2D tileSprite;

        private Texture2D tileSprite2;

        private Player player;

        private BuffManager buffManager;
        private PlayerLevelUI playerLevelUI;
        public static bool isPaused = false;


        private Song song;
        private float songVolume = 0.02f;


        

        public static Texture2D healthTexture;




        public Game1()
        {
            rnd = new Random();

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            

        }

        public static Vector2 GetScreenSize()
        {
            return screenSize;
        }

        protected override void Initialize()
        {
            spawnInterval = 5;


            gameObjects = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();

            tileSprite = Content.Load<Texture2D>("tile");
            tileSprite2 = Content.Load<Texture2D>("tile2");



            Random rand = new Random();

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Texture2D chosenSprite;
                    switch (rand.Next(0, 2))
                    {
                        case 0:
                            chosenSprite = tileSprite;
                            break;
                        case 1:
                            chosenSprite = tileSprite2;
                            break;
                        default:
                            chosenSprite = tileSprite;
                            break;
                    }
                    Tile newTile = new Tile(chosenSprite);
                    newTile.Position = new Vector2(x * newTile.Size.X, y * newTile.Size.Y);
                    gameObjects.Add(newTile);
                }
            }
            player = new Player();
            buffManager = new BuffManager(player);
            playerLevelUI = new PlayerLevelUI(player);
            gameObjects.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            //spriteFont = Content.Load<SpriteFont>("font2");
            //TODO

            textFont = Content.Load<SpriteFont>("text"); 


            collisionTexture = Content.Load<Texture2D>("pixel");

            buffManager.LoadContent(Content);
            playerLevelUI.LoadContent(Content);

            foreach (GameObject gameobject in gameObjects)
            {
                gameobject.LoadContent(Content);
            }

            healthTexture = new Texture2D(GraphicsDevice, 1, 1);
            healthTexture.SetData(new[] { Color.Red });

            playerHealthBar = new HealthBar(healthTexture, new Vector2(20, 20), 200, 20, 1000);

            song = Content.Load<Song>("music1"); // music for the game

            MediaPlayer.IsRepeating = true; // music keeps playing as long as the game is running
            MediaPlayer.Volume = songVolume; 
            MediaPlayer.Play(song);
        }

        protected override void Update(GameTime gameTime)
        {
            

            if (gameOver) // Malthe
            {
                MediaPlayer.Stop();

                if (Keyboard.GetState().IsKeyDown(Keys.R)) // if R is pressed the game will restart
                {
                    ResetGame();
                }
                return;
                
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(!isPaused)
            {
                spawnInterval += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnInterval > timeBetweenInterval)
                {
                    SpawnEnemy();
                    spawnInterval = 0;
                }


                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(gameTime);

                    if (gameObject.CollisionEnabled)
                    {
                        foreach (GameObject other in gameObjects)
                        {
                            if (other == gameObject) continue;

                            gameObject.CheckCollision(other);
                        }
                    }
                }

                if (player != null && playerHealthBar != null)
                {
                    playerHealthBar.SetHealth((int)player.Health);
                }
            }

            if (player != null && player.Health <= 0)
            {
                gameOver = true; // if player health i <0 the game ends 
                //Exit();
            }

            AddGameobjects();
            RemoveGameobjects();

           

            buffManager.UpdateUI(gameTime);

            base.Update(gameTime);
        }

        public static void AddGameobjectToRemove(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gameOver) // Malthe
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin();
                Vector2 textPosition1 = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, _graphics.PreferredBackBufferHeight / 2 - 50);
                Vector2 textPosition2 = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 250, _graphics.PreferredBackBufferHeight / 2 + 200);
                _spriteBatch.DrawString(textFont, $"GAME OVER", textPosition1, Color.Green);
                _spriteBatch.DrawString(textFont, $"Press R to Restart", textPosition2, Color.Green);
                _spriteBatch.End();
                return;
                // Creates a gameOver screen when player dies, with text implemented on the screen
                

            }




            //Move the camera based on the player position
            Matrix cameraTransform = Matrix.CreateTranslation(-player.Position.X+screenSize.X/2, -player.Position.Y+screenSize.Y/2, 0);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cameraTransform);

            foreach (GameObject gameobject in gameObjects)
            {
                gameobject.Draw(_spriteBatch);

#if DEBUG
                DrawCollisionBox(gameobject);
#endif
            }

             

            _spriteBatch.End();


            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //Place all ui elements here (things that should stay on screen no matter the camera position)

            buffManager.DrawUI(_spriteBatch);
            playerHealthBar.Draw(_spriteBatch);
            playerLevelUI.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddGameobjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.LoadContent(Content);
                gameObjects.Add(gameObject);
                Console.WriteLine($"Spawned object: {gameObject}");
            }

            gameObjectsToAdd.Clear();
        }

        /// <summary> // Malthe og Oliver
        /// Resets the game state to its initial conditions, preparing for a new game session.
        /// Resetting the player's health and position, clearing all enemies and
        /// other non-player game objects, resetting relevant game variables, and restarting the background music.
        /// </summary>
        private void ResetGame()
        {
            // Reset game-over status
            gameOver = false;

            // Reset player's health and position to initial values
            player.Health = 1000;
            player.Position = new Vector2(screenSize.X / 2, screenSize.Y - 100);

            // Remove all enemies and other non-player objects from the game
            gameObjects.RemoveAll(o => o is Enemy);

            // Clear the buffers for game objects to add and remove
            gameObjectsToAdd.Clear();
            gameObjectsToRemove.Clear();

            // Reset game-specific variables
            spawnInterval = 5;

            // Restart background music
            MediaPlayer.Play(song);
        }

        public static void InstantiateGameobject(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }


        /// <summary> // Mark og Malthe
        /// Spawns a random enemy based on predefined probabilities. Each enemy type 
        /// has a specific chance of spawning, and the spawned enemy is added to the game world.

        /// </summary>
        private void SpawnEnemy()
        {
            Enemy spawnedEnemy;

            // Generate a random number to determine which enemy to spawn
            int spawnEnemy = rnd.Next(100);

            // Spawn different enemies based on probability ranges
            if (spawnEnemy <= 20)
                spawnedEnemy = new Rat(player); // 21% chance
            else if (spawnEnemy > 20 && spawnEnemy <= 38)
                spawnedEnemy = new Snake(player); // 18% chance
            else if (spawnEnemy > 38 && spawnEnemy <= 55)
                spawnedEnemy = new Scorpio(player); // 17% chance
            else if (spawnEnemy > 55 && spawnEnemy <= 70)
                spawnedEnemy = new Vulture(player); // 15% chance
            else if (spawnEnemy > 70 && spawnEnemy <= 85)
                spawnedEnemy = new Hyena(player); // 15% chance
            else if (spawnEnemy > 85 && spawnEnemy <= 95)
                spawnedEnemy = new Deceased(player); // 10% chance
            else
                spawnedEnemy = new Mummy(player); // 5% chance

           
            spawnedEnemy.LoadContent(Content);
            gameObjects.Add(spawnedEnemy);
        }



        private void RemoveGameobjects()
        {
            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                gameObjects.Remove(gameObject);
            }
            gameObjectsToRemove.Clear();
        }

        private void DrawCollisionBox(GameObject go)
        {
            if (!go.CollisionEnabled) return;

            Rectangle collisionBox = go.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);

            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);

            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);

            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

        }
    }
}
