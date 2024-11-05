using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1;

public class Player : GameObject
{
    private float health;
    private Texture2D front;
    private Texture2D back;
    private Texture2D right;
    private Texture2D left;
    private Texture2D[] walk_down_sprites;
    private Texture2D[] walk_back_sprites;
    private Texture2D[] walk_right_sprites;
    private Texture2D[] walk_left_sprites;
    private double animationTimer = 0;
    private double timePerFrame;

    public Player()
    {
        speed = 300;
        position.X = Game1.GetScreenSize().X / 2;
        position.Y = Game1.GetScreenSize().Y - 100;
        fps = 20;
        scale = 3;

        //Calculate time per frame
        timePerFrame = 1/fps;

    }

    public override void LoadContent(ContentManager contentManager)
    {
        //Player faces front (sprite)
        front = contentManager.Load<Texture2D>("front");
        //Walking down animations (facing screen)
        walk_down_sprites = new Texture2D[4];
        for (int i = 0; i < walk_down_sprites.Length; i++) 
        {
            walk_down_sprites[i] = contentManager.Load<Texture2D>($"walk_down_{i + 1}");
        }

        //Player faces back (sprite)
        back = contentManager.Load<Texture2D>("back");
        //Walking up animations (facing away from screen)
        walk_back_sprites = new Texture2D[4];
        for (int i = 0; i < walk_back_sprites.Length; i++) 
        {
            walk_back_sprites[i] = contentManager.Load<Texture2D>($"walk_up_{i + 1}");
        }

        //Player faces right (sprite)
        right = contentManager.Load<Texture2D>("right");
        //Walking up animations (facing away from screen)
        walk_right_sprites = new Texture2D[4];
        for (int i = 0; i < walk_right_sprites.Length; i++) 
        {
            walk_right_sprites[i] = contentManager.Load<Texture2D>($"walk_right_{i + 1}");
        }

        //Player faces left (sprite)
        left = contentManager.Load<Texture2D>("left");
        //Walking up animations (facing away from screen)
        walk_left_sprites = new Texture2D[4];
        for (int i = 0; i < walk_left_sprites.Length; i++) 
        {
            walk_left_sprites[i] = contentManager.Load<Texture2D>($"walk_left_{i + 1}");
        }

        //Sprite displayed when player spawns
        Sprite = front;
    }

    public override void OnCollision(GameObject other)
    {
        //
    }

    public override void Update(GameTime gameTime)
    {
        HandleInput(gameTime);
        //Animate(gameTime);
        Move(gameTime);
    }

    private void HandleInput(GameTime gameTime)
    {
        velocity = Vector2.Zero;
        KeyboardState keyState = Keyboard.GetState();

        //Updates animation timer to elapsed game time
        animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

        //Next animation frame will be updated, when enough time has passed.
        if (animationTimer >= timePerFrame)
        {
            //Resets timer
            animationTimer -= timePerFrame; 

            //W-tast input
            if (keyState.IsKeyDown(Keys.W))
            {
                //Sprite bliver opdateret til at vise spritet svarende 
                //til CurrentIndex i walk_back_sprites-arrayet
                Sprite = walk_back_sprites[CurrentIndex];
                
                // Nulstiller CurrentIndex ved slut af animations-array
                CurrentIndex++;
                if (CurrentIndex >= walk_back_sprites.Length)
                {
                    CurrentIndex = 0;
                }

                //Player bevæger sig op ved tryk på "W"
                velocity += new Vector2(0, -1);

            }

            //S-tast input
            if (keyState.IsKeyDown(Keys.S))
            {
                //Sprite bliver opdateret til at vise spritet svarende 
                //til CurrentIndex i walk_back_sprites-arrayet
                Sprite = walk_down_sprites[CurrentIndex];
                
                // Nulstiller CurrentIndex ved slut af animations-array
                CurrentIndex++;
                if (CurrentIndex >= walk_down_sprites.Length)
                {
                    CurrentIndex = 0;
                }

                //Player bevæger sig op ved tryk på "W"
                velocity += new Vector2(0, 1);
            }

            //A-tast input
            if (keyState.IsKeyDown(Keys.A))
            {
                //Sprite bliver opdateret til at vise spritet svarende 
                //til CurrentIndex i walk_back_sprites-arrayet
                Sprite = walk_left_sprites[CurrentIndex];
                
                // Nulstiller CurrentIndex ved slut af animations-array
                CurrentIndex++;
                if (CurrentIndex >= walk_left_sprites.Length)
                {
                    CurrentIndex = 0;
                }

                //Player bevæger sig op ved tryk på "W"
                velocity += new Vector2(-1, 0);
            }

            //D-tast input
            if (keyState.IsKeyDown(Keys.D))
            {
                //Sprite bliver opdateret til at vise spritet svarende 
                //til CurrentIndex i walk_back_sprites-arrayet
                Sprite = walk_right_sprites[CurrentIndex];
                
                // Nulstiller CurrentIndex ved slut af animations-array
                CurrentIndex++;
                if (CurrentIndex >= walk_right_sprites.Length)
                {
                    CurrentIndex = 0;
                }

                //Player bevæger sig op ved tryk på "W"
                velocity += new Vector2(1, 0);
            } 

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }
        
    }
}