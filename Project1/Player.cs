using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1;

public class Player : GameObject
{
    private float health;

    public Player()
    {
        speed = 300;
        position.X = Game1.GetScreenSize().X / 2;
        position.Y = Game1.GetScreenSize().Y - 100;
        fps = 12;
    }

    public override void LoadContent(ContentManager contentManager)
    {
        Sprite = contentManager.Load<Texture2D>("Idle");
    }

    public override void OnCollision(GameObject other)
    {
        //
    }

    public override void Update(GameTime gameTime)
    {
        HandleInput();
        //Animate(gameTime);
        Move(gameTime);
    }

    private void HandleInput()
    {
        velocity = Vector2.Zero;

        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.W))
        {
            velocity += new Vector2(0, -1);
        }
        if (keyState.IsKeyDown(Keys.S))
        {
            velocity += new Vector2(0, 1);
        }
        if (keyState.IsKeyDown(Keys.A))
        {
            velocity += new Vector2(-1, 0);
        }
        if (keyState.IsKeyDown(Keys.D))
        {
            velocity += new Vector2(1, 0);
        }


        if (velocity != Vector2.Zero)
        {
            velocity.Normalize();
        }
    }
}