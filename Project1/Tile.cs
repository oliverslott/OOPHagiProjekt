using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Tile : GameObject
    {
        public Tile(Texture2D sprite)
        {
            Sprite = sprite;
            scale = 2;
            size = new Vector2(sprite.Width * (int)scale, sprite.Height * (int)scale);
            CollisionEnabled = false;
        }
        public override void LoadContent(ContentManager contentManager)
        {
            //Sprite = contentManager.Load<Texture2D>("tile");
            //size = new Vector2(Sprite.Width*scale, Sprite.Height*scale);
        }

        public override void OnCollision(GameObject other)
        {
            //No collision needed
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
