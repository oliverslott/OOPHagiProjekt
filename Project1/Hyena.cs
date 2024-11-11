using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Hyena : Enemy
    {
        private Texture2D[] hyena_walk;
        private Texture2D[] hyena_attack;
        private Texture2D[] hyena_hurt;
        private Texture2D[] hyena_death;
        protected float hyena_speed;
        protected int hyena_health;
        protected int hyena_dmg;

        public Hyena(Player player) : base(player, 10)
        {
            speed = NextFloat(150,160);
            damage = 4/6.0f;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Hyena bevægelse
            hyena_walk = new Texture2D[6];
            for (int i = 0; i < hyena_walk.Length; i++)
            {
                hyena_walk[i] = contentManager.Load<Texture2D>($"hyena_walk_{i + 1}");
            }

            //Hyena tager skade
            hyena_hurt = new Texture2D[2];
            for (int i = 0; i < hyena_hurt.Length; i++)
            {
                hyena_hurt[i] = contentManager.Load<Texture2D>($"hyena_hurt_{i + 1}");
            }

            //Hyena angreb
            hyena_attack = new Texture2D[6];
            for (int i = 0; i < hyena_attack.Length; i++)
            {
                hyena_attack[i] = contentManager.Load<Texture2D>($"hyena_attack_{i + 1}");
            }

            //Hyena omkom
            hyena_death = new Texture2D[6];
            for (int i = 0; i < hyena_death.Length; i++)
            {
                hyena_death[i] = contentManager.Load<Texture2D>($"hyena_death_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(hyena_walk);
        }

        public override void LoadAttackAnimation(ContentManager contentManager) //Hyena har ikke attack animation
        {
            ChangeAnimationSprites(hyena_attack);
        } 

        public override void LoadHurtAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(hyena_hurt);
        }

        public override void LoadDeathAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(hyena_death);
        }
    }
}