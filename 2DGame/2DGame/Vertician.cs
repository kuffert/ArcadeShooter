using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This enemy type will consistently travel up and down on the
    // vertical axis.
    class Vertician : Enemy
    {
        Vertician(Vector2 loc)
        {
            this.image = ArcadeShooter.verticianImage;
            this.location = loc;
        }

        // Inherited from Enemy superclass:

        // moves the Vertician enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
