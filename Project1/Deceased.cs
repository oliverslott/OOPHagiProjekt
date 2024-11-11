using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Deceased : Enemy
    {
        private Texture2D[] deceased_walk;
        private Texture2D[] deceased_attack;
        private Texture2D[] deceased_hurt;
        private Texture2D[] deceased_death;
        protected float deceased_speed;
        protected int deceased_health;
        protected int deceased_dmg;
        public Deceased(Player player) : base(player, 20)
        {
            speed = NextFloat(40,50);
            damage = 12/6.0f;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Deceased bevægelse
            deceased_walk = new Texture2D[6];
            for (int i = 0; i < deceased_walk.Length; i++)
            {
                deceased_walk[i] = contentManager.Load<Texture2D>($"deceased_walk_{i + 1}");
            }

            //Deceased tager skade
            deceased_hurt = new Texture2D[2];
            for (int i = 0; i < deceased_hurt.Length; i++)
            {
                deceased_hurt[i] = contentManager.Load<Texture2D>($"deceased_hurt_{i + 1}");
            }

            //Deceased angreb
            deceased_attack = new Texture2D[4];
            for (int i = 0; i < deceased_attack.Length; i++)
            {
                deceased_attack[i] = contentManager.Load<Texture2D>($"deceased_attack_{i + 1}");
            }

            //Deceased omkom
            deceased_death = new Texture2D[6];
            for (int i = 0; i < deceased_death.Length; i++)
            {
                deceased_death[i] = contentManager.Load<Texture2D>($"deceased_death_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(deceased_walk);
        }
        public override void LoadAttackAnimation(ContentManager contentManager) //Rotte har ikke attack animation
        {
            ChangeAnimationSprites(deceased_attack);
        } 
        public override void LoadHurtAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(deceased_hurt);
        }
        public override void LoadDeathAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(deceased_death);
        }
    }
}