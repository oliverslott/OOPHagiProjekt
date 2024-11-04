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

    public Player()
    {
        speed = 300;
        position.X = Game1.GetScreenSize().X / 2;
        position.Y = Game1.GetScreenSize().Y - 100;
        fps = 12;
        scale = 3;
    }

    public override void LoadContent(ContentManager contentManager)
    {
        front = contentManager.Load<Texture2D>("front");
        back = contentManager.Load<Texture2D>("back");
        right = contentManager.Load<Texture2D>("right");
        left = contentManager.Load<Texture2D>("left");

        Sprite = front;
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
            Sprite = back;
            velocity += new Vector2(0, -1);
        }
        if (keyState.IsKeyDown(Keys.S))
        {
            Sprite = front;
            velocity += new Vector2(0, 1);
        }
        if (keyState.IsKeyDown(Keys.A))
        {
            Sprite = left;
            velocity += new Vector2(-1, 0);
        }
        if (keyState.IsKeyDown(Keys.D))
        {
            Sprite = right;
            velocity += new Vector2(1, 0);
        }


        if (velocity != Vector2.Zero)
        {
            velocity.Normalize();
        }
    }
}