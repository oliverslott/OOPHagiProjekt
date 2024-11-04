using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject
{
    private Texture2D sprite;
    protected Texture2D[] sprites;
    protected Vector2 position;
    protected float rotation;
    protected float fps;
    private float timeElapsed;
    private int currentIndex;
    protected Vector2 velocity;
    protected float speed;
    protected Rectangle collisionBox;

    public Texture2D Sprite { get => sprite; set => sprite = value; }
    public Vector2 Position { get => position; set => position = value; }
    public Rectangle CollisionBox { get => collisionBox; }
    protected int CurrentIndex { get => currentIndex; set => currentIndex = value; }

    public abstract void LoadContent(ContentManager contentManager);

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, Position, null, Color.White, rotation, new Vector2(Sprite.Width/2, Sprite.Height/2), 1f, SpriteEffects.None, 0);
        collisionBox = new Rectangle((int)position.X-Sprite.Width/2, (int)position.Y - Sprite.Height/2, sprite.Width, sprite.Height);
    }

    public abstract void Update(GameTime gameTime);

    protected void Animate(GameTime gameTime)
    {
        timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        currentIndex = (int)(timeElapsed * fps);

        sprite = sprites[currentIndex];

        if(currentIndex >= sprites.Length - 1)
        {
            timeElapsed = 0;
            currentIndex = 0;
        }
    }

    protected void Move(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position += velocity * speed * deltaTime;
    }

    public bool IsColiding(GameObject other)
    {
        return true;
    }

    public abstract void OnCollision(GameObject other);

    public void CheckCollision(GameObject other)
    {
        if(CollisionBox.Intersects(other.CollisionBox))
        {
            OnCollision(other);
        }
    }
}