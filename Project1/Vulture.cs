using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
/// <summary>
/// Vulture fjende-klasse nedarver fra Enemy
/// </summary>
    public class Vulture : Enemy
    {
        private Texture2D[] vulture_walk;
        private Texture2D[] vulture_attack;
        private Texture2D[] vulture_hurt;
        private Texture2D[] vulture_death;
        /// <summary>
        /// Vulture konstruktør.
        /// Konstruktøren tager mod player, og player + maxHealth (nedarvet fra Enemy)
        /// maxHealth = 4 for denne fjende.
        /// Andre stats modificeres manuelt i konstruktøren
        /// </summary>
        /// <param name="player"></param>

        public Vulture(Player player) : base(player, 4)
        {
            speed = NextFloat(250,260);
            damage = 2/6.0f;

            RandomSpawn();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            //Vulture bevægelse
            vulture_walk = new Texture2D[4];
            for (int i = 0; i < vulture_walk.Length; i++)
            {
                vulture_walk[i] = contentManager.Load<Texture2D>($"vulture_walk_{i + 1}");
            }

            //Vulture tager skade
            vulture_hurt = new Texture2D[2];
            for (int i = 0; i < vulture_hurt.Length; i++)
            {
                vulture_hurt[i] = contentManager.Load<Texture2D>($"vulture_hurt_{i + 1}");
            }

            //Vulture angreb
            vulture_attack = new Texture2D[4];
            for (int i = 0; i < vulture_attack.Length; i++)
            {
                vulture_attack[i] = contentManager.Load<Texture2D>($"vulture_attack_{i + 1}");
            }

            //Vulture omkom
            vulture_death = new Texture2D[4];
            for (int i = 0; i < vulture_death.Length; i++)
            {
                vulture_death[i] = contentManager.Load<Texture2D>($"vulture_death_{i + 1}");
            }
            base.LoadContent(contentManager);
        }

        public override void LoadWalkAnimation(ContentManager contentManager) //Indlæser ganganimationen for vulture
        {
            ChangeAnimationSprites(vulture_walk);
        }

        public override void LoadAttackAnimation(ContentManager contentManager) //Indlæser angrebsanimationen for vulture
        {
            ChangeAnimationSprites(vulture_attack);
        } 

        public override void LoadHurtAnimation(ContentManager contentManager) //Indlæser skadeanimationen for vulture
        {
            ChangeAnimationSprites(vulture_hurt);
        }

        public override void LoadDeathAnimation(ContentManager contentManager) //Indlæser dødsanimationen for vulture
        {
            ChangeAnimationSprites(vulture_death);
        }
    }
}