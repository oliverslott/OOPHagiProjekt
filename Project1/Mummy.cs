using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Project1
{
    public class Mummy : Enemy
    {
        private Texture2D[] mummy_walk;
        private Texture2D[] mummy_attack;
        private Texture2D[] mummy_hurt;
        private Texture2D[] mummy_death;

        public Mummy(Player player) : base(player, 35)
        {
            speed = NextFloat(60,70);
            damage = 15/6.0f;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Mummy bevægelse
            mummy_walk = new Texture2D[6];
            for (int i = 0; i < mummy_walk.Length; i++)
            {
                mummy_walk[i] = contentManager.Load<Texture2D>($"mummy_walk_{i + 1}");
            }

            //Mummy tager skade
            mummy_hurt = new Texture2D[2];
            for (int i = 0; i < mummy_hurt.Length; i++)
            {
                mummy_hurt[i] = contentManager.Load<Texture2D>($"mummy_hurt_{i + 1}");
            }

            //Mummy angreb
            mummy_attack = new Texture2D[6];
            for (int i = 0; i < mummy_attack.Length; i++)
            {
                mummy_attack[i] = contentManager.Load<Texture2D>($"mummy_attack_{i + 1}");
            }

            //Mummy omkom
            mummy_death = new Texture2D[6];
            for (int i = 0; i < mummy_death.Length; i++)
            {
                mummy_death[i] = contentManager.Load<Texture2D>($"mummy_death_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager) //Indlæser ganganimationen for mummy
        {
            ChangeAnimationSprites(mummy_walk);
        }

        public override void LoadAttackAnimation(ContentManager contentManager) //Indlæser angrebsanimationen for mummy
        {
            ChangeAnimationSprites(mummy_attack);
        } 

        public override void LoadHurtAnimation(ContentManager contentManager) //Indlæser skadeanimationen for mummy
        {
            ChangeAnimationSprites(mummy_hurt);
        }

        public override void LoadDeathAnimation(ContentManager contentManager) //Indlæser dødsanimationen for mummy
        {
            ChangeAnimationSprites(mummy_death);
        }
    }
}