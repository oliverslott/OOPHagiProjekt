using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class Rat : Enemy
    {
        private Texture2D[] rat_walk;
        private Texture2D[] rat_hurt;
        private Texture2D[] rat_death;

        public Rat(Player player) : base(player)
        {
            maxHealth = 10;
            //speed = 
            //damage =

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Rotte bevægelse
            rat_walk = new Texture2D[4];
            for (int i = 0; i < rat_walk.Length; i++)
            {
                rat_walk[i] = contentManager.Load<Texture2D>($"walk_rat_{i + 1}");
            }

            //Rotte tager skade
            rat_hurt = new Texture2D[2];
            for (int i = 0; i < rat_hurt.Length; i++)
            {
                rat_hurt[i] = contentManager.Load<Texture2D>($"hurt_rat_{i + 1}");
            }

            //Rotte omkom
            rat_death = new Texture2D[4];
            for (int i = 0; i < rat_death.Length; i++)
            {
                rat_death[i] = contentManager.Load<Texture2D>($"death_rat_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(rat_walk);
        }
        public override void LoadAttackAnimation(ContentManager contentManager) //Rotte har ikke attack animation
        {
            ChangeAnimationSprites(rat_walk);
        } 
        public override void LoadHurtAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(rat_hurt);
        }
        public override void LoadDeathAnimation(ContentManager contentManager)
        {
            ChangeAnimationSprites(rat_death);
        }
    }
}
