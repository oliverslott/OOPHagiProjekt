using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1;

public class Player : GameObject
{
    public float Health { get; set; } = 1000; // 1000 HP - player health

    public float health;
    private Texture2D front;
    private Texture2D back;
    private Texture2D right;
    private Texture2D left;
    private Texture2D[] walk_down_sprites;
    private Texture2D[] walk_back_sprites;
    private Texture2D[] walk_right_sprites;
    private Texture2D[] walk_left_sprites;
    private Texture2D bulletSprite;
    private float shootInterval = 0.5f;
    private float shootCooldown = 0f;
    private int level = 0;
    private int xp = 0;

    private SoundEffect bulletSound;
    private SoundEffectInstance bulletSoundInstance;
    private float soundEffectVolume = 0.1f;

    private SoundEffect walking;
    private SoundEffectInstance walkingInstance;

    public event Action OnLevelUp;



    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private Direction currentDirection = Direction.DOWN;

    //public float Health { get => health; set => health = value; }

    public float Speed { get => speed; set => speed = value; }

    //Public setter for buffmanager
    public float ShootInterval { get => shootInterval; set => shootInterval = value; }
    public int Level
    {
        get
        {
            return xp/100;
        }
        set
        {
            if(value > level)
            {
                level = value;
                OnLevelUp?.Invoke();
            }
            else
            {
                level = value;
            }
        }
    }

    public int Xp { get => xp; set => xp = value; }

    public Player()
    {
        speed = 300;
        position.X = Game1.GetScreenSize().X / 2;
        position.Y = Game1.GetScreenSize().Y - 100;
        fps = 12;
        scale = 3;
        health = 1000;
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
        sprites = [front];

        bulletSprite = contentManager.Load<Texture2D>("Bullet_Small"); //Gets loaded before-hand for better performance

        
        bulletSound = contentManager.Load<SoundEffect>($"Sounds\\pew"); // loads bullet sound
        bulletSoundInstance = bulletSound.CreateInstance(); // creates instance for playback

        walking = contentManager.Load<SoundEffect>("walkingGame"); // loads walking sound
        walkingInstance = walking.CreateInstance(); // creates instance for playback during player movement


    }

    public override void OnCollision(GameObject other)
    {
        //
    }

    public override void Update(GameTime gameTime)
    {
        if (shootCooldown >= 0)
        {
            shootCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        HandleAnimationState();
        HandleInput(gameTime);
        Animate(gameTime);
        Move(gameTime);


    }

    /// <summary> // Malthe
    /// Handles player input and updates the player's movement direction, velocity, 
    /// and shooting actions based on keyboard input. Also manages walking sound effects.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    private void HandleInput(GameTime gameTime)
    {
        velocity = Vector2.Zero;
        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.W))
        {
            currentDirection = Direction.UP;
            velocity += new Vector2(0, -1);
        }
        if (keyState.IsKeyDown(Keys.S))
        {
            currentDirection = Direction.DOWN;
            velocity += new Vector2(0, 1);
        }

        if (keyState.IsKeyDown(Keys.A))
        {
            currentDirection = Direction.LEFT;
            velocity += new Vector2(-1, 0);
        }

        if (keyState.IsKeyDown(Keys.D))
        {
            currentDirection = Direction.RIGHT;
            velocity += new Vector2(1, 0);
        }

        if (velocity != Vector2.Zero)
        {
            velocity.Normalize();
        }

        if (keyState.IsKeyDown(Keys.Space))
        {
            Shoot();
        }
        if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.A) ||
    keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.D))
        {
            if (walkingInstance.State != SoundState.Playing)
            {
                walkingInstance.Volume = soundEffectVolume;
                walkingInstance.Play();
            }
        }
        else
        {
            if (walkingInstance.State == SoundState.Playing)
            {
                walkingInstance.Stop();
            }
        }
    }

    private void Shoot()
    {
        if (shootCooldown <= 0)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            //The reason I am not using player position here is because we are doing some weird matrix translation, which causes the mouseposition and player position to be out of sync.
            Game1.InstantiateGameobject(new Bullet(bulletSprite, position, mousePosition - Game1.GetScreenSize() / 2));
            shootCooldown = ShootInterval;

            bulletSound.Play(soundEffectVolume, 0.0f, 0.0f);
        }
    }

    private Vector2 GetVelocityByDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return new Vector2(0, -1);
            case Direction.DOWN:
                return new Vector2(0, 1);
            case Direction.LEFT:
                return new Vector2(-1, 0);
            case Direction.RIGHT:
                return new Vector2(1, 0);
            default:
                return new Vector2(0, 1);
        }
    }

    private void HandleAnimationState()
    {
        switch (currentDirection)
        {
            case Direction.UP:
                if (velocity.LengthSquared() > 0f)
                {
                    ChangeAnimationSprites(walk_back_sprites);
                }
                else
                {
                    ChangeAnimationSprites([back]);
                }
                break;
            case Direction.DOWN:
                if (velocity.LengthSquared() > 0f)
                {
                    ChangeAnimationSprites(walk_down_sprites);
                }
                else
                {
                    ChangeAnimationSprites([front]);
                }
                break;
            case Direction.LEFT:
                if (velocity.LengthSquared() > 0f)
                {
                    ChangeAnimationSprites(walk_left_sprites);
                }
                else
                {
                    ChangeAnimationSprites([left]);
                }
                break;
            case Direction.RIGHT:
                if (velocity.LengthSquared() > 0f)
                {
                    ChangeAnimationSprites(walk_right_sprites);
                }
                else
                {
                    ChangeAnimationSprites([right]);
                }
                break;
        }
    }

    public void AddXp(int amount)
    {
        xp += amount;
        Level = xp / 100;
    }
}