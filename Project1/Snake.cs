using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
/// <summary>
/// Snake fjende-klasse nedarver fra Enemy
/// </summary>
    public class Snake : Enemy
    {
        private Texture2D[] snake_walk;
        private Texture2D[] snake_hurt;
        private Texture2D[] snake_attack;
        private Texture2D[] snake_death;
        /// <summary>
        /// Snake konstruktør.
        /// Konstruktøren tager mod player, og player + maxHealth (nedarvet fra Enemy)
        /// maxHealth = 2 for denne fjende.
        /// Andre stats modificeres manuelt i konstruktøren
        /// </summary>
        /// <param name="player"></param>
        public Snake(Player player) : base(player, 2)
        {
            maxHealth = 2;
            speed = NextFloat(40,50);
            damage = 5/6.0f;

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

        public override void LoadWalkAnimation(ContentManager contentManager) //Indlæser ganganimationen for snake
        {
            ChangeAnimationSprites(snake_walk);
        }
        public override void LoadAttackAnimation(ContentManager contentManager) //Indlæser angrebsanimationen for snake
        {
            ChangeAnimationSprites(snake_attack);
        }
        public override void LoadHurtAnimation(ContentManager contentManager) //Indlæser skadeanimationen for snake
        {
            ChangeAnimationSprites(snake_hurt);
        }
        public override void LoadDeathAnimation(ContentManager contentManager) //Indlæser dødsanimationen for snake
        {
            ChangeAnimationSprites(snake_death);
        }
    }
}