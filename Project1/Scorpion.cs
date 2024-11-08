using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Scorpio : Enemy
    {
        private Texture2D[] scorpio_walk;
        private Texture2D[] scorpio_hurt;
        private Texture2D[] scorpio_attack;
        private Texture2D[] scorpio_death;
        protected float scorpio_speed;
        protected int scorpio_health;
        protected int scorpio_dmg;
        public Scorpio(Player player) : base(player)
        {
            this.scorpio_speed = 0.2f;
            this.scorpio_health = 2;
            this.scorpio_dmg = 5;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Rotte bevægelse
            scorpio_walk = new Texture2D[4];
            for (int i = 0; i < scorpio_walk.Length; i++)
            {
                scorpio_walk[i] = contentManager.Load<Texture2D>($"scorpio_walk_{i + 1}");
            }

            //Rotte angreb
            scorpio_attack = new Texture2D[4];
            for (int i = 0; i < scorpio_attack.Length; i++)
            {
                scorpio_attack[i] = contentManager.Load<Texture2D>($"scorpio_attack_{i + 1}");
            }

            //Rotte skade
            scorpio_hurt = new Texture2D[2];
            for (int i = 0; i < scorpio_hurt.Length; i++)
            {
                scorpio_hurt[i] = contentManager.Load<Texture2D>($"scorpio_hurt_{i + 1}");
            }

            //Rotte omkom
            scorpio_death = new Texture2D[4];
            for (int i = 0; i < scorpio_death.Length; i++)
            {
                scorpio_death[i] = contentManager.Load<Texture2D>($"scorpio_death_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(scorpio_walk);
        } 
        public override void LoadAttackAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(scorpio_attack);
        } 
        public override void LoadHurtAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(scorpio_hurt);
        } 
        public override void LoadDeathAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(scorpio_death);
        } 
    }
}

