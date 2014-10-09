using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This enemy type will consistently move left and right
    // on the horizontal axis.
    class Horizon : Enemy
    {
        Horizon(Vector2 loc)
        {
            this.image = ArcadeShooter.horizonImage;
            this.location = loc;
        }

        // Inherited from Enemy superclass:

        // Moves the Horizon enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
