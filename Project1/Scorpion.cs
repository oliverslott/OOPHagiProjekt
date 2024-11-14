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
        public Scorpio(Player player) : base(player, 1)
        {
            speed = NextFloat(100,110);
            damage = 5/6.0f;

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

        public override void LoadWalkAnimation(ContentManager contentManager) //Indlæser ganganimationen for scorpion
        {
            ChangeAnimationSprites(scorpio_walk);
        } 
        public override void LoadAttackAnimation(ContentManager contentManager) //Indlæser angrebsanimationen for scorpion
        {
            ChangeAnimationSprites(scorpio_attack);
        } 
        public override void LoadHurtAnimation(ContentManager contentManager) //Indlæser skadeanimationen for scorpion
        {
            ChangeAnimationSprites(scorpio_hurt);
        } 
        public override void LoadDeathAnimation(ContentManager contentManager) //Indlæser dødsanimationen for scorpion
        {
            ChangeAnimationSprites(scorpio_death);
        } 
    }
}

