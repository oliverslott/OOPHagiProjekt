using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
/// <summary>
/// Deceased fjende-klasse nedarver fra Enemy
/// </summary>
    public class Deceased : Enemy
    {
        private Texture2D[] deceased_walk;
        private Texture2D[] deceased_attack;
        private Texture2D[] deceased_hurt;
        private Texture2D[] deceased_death;
        /// <summary>
        /// Deceased konstruktør.
        /// Konstruktøren tager mod player, og player + maxHealth (nedarvet fra Enemy)
        /// maxHealth = 20 for denne fjende.
        /// Andre stats modificeres manuelt i konstruktøren
        /// </summary>
        /// <param name="player"></param>
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

        public override void LoadWalkAnimation(ContentManager contentManager) //Indlæser ganganimationen for deceased
        {
            ChangeAnimationSprites(deceased_walk);
        }
        public override void LoadAttackAnimation(ContentManager contentManager) //Indlæser angrebsanimationen for deceased
        {
            ChangeAnimationSprites(deceased_attack);
        } 
        public override void LoadHurtAnimation(ContentManager contentManager) //Indlæser skadeanimationen for deceased
        {
            ChangeAnimationSprites(deceased_hurt);
        }
        public override void LoadDeathAnimation(ContentManager contentManager) //Indlæser dødsanimationen for deceased
        {
            ChangeAnimationSprites(deceased_death);
        }
    }
}