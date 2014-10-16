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
        // Horizon Constructor
       public Horizon(Vector2 loc, float dir)
        {
            this.image = ArcadeShooter.horizonImage;
            this.location = loc;
            this.leftBound = 0;
            this.rightBound = ArcadeShooter.width - image.Width;
            this.direction = dir;
            this.speed = 10;
        }

        // Moves the Horizon enemy Left <-> Right
        public override void moveAISprite()
        {
            if (location.X >= rightBound) { direction = -1f; }
            if (location.X <= leftBound)  { direction =  1f; }
            location.X += speed * direction;
        }
    }
}
