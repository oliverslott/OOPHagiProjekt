using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Snake : Enemy
    {
        private Texture2D[] snake_walk;
        private Texture2D[] snake_hurt;
        private Texture2D[] snake_attack;
        private Texture2D[] snake_death;
        protected float snake_speed;
        protected int snake_health;
        protected int snake_dmg;
        public Snake(Player player) : base(player)
        {
            this.snake_speed = 1.2f;
            this.snake_health = 3;
            this.snake_dmg = 3;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {

            //Rotte bevægelse
            snake_walk = new Texture2D[4];
            for (int i = 0; i < snake_walk.Length; i++)
            {
                snake_walk[i] = contentManager.Load<Texture2D>($"snake_walk_{i + 1}");
            }

            //Rotte angreb
            snake_attack = new Texture2D[6];
            for (int i = 0; i < snake_attack.Length; i++)
            {
                snake_attack[i] = contentManager.Load<Texture2D>($"snake_attack_{i + 1}");
            }

            //Rotte skade
            snake_hurt = new Texture2D[2];
            for (int i = 0; i < snake_hurt.Length; i++)
            {
                snake_hurt[i] = contentManager.Load<Texture2D>($"snake_hurt_{i + 1}");
            }

            //Rotte omkom
            snake_death = new Texture2D[4];
            for (int i = 0; i < snake_death.Length; i++)
            {
                snake_death[i] = contentManager.Load<Texture2D>($"snake_death_{i + 1}");
            }

            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(snake_walk);
        }
    }
}