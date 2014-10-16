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
        // Vertician Constructor
        public Vertician(Vector2 loc, float dir)
        {
            this.image = ArcadeShooter.verticianImage;
            this.location = loc;
            this.upBound = 0;
            this.downBound = ArcadeShooter.height - image.Height;
            this.direction = dir;
            this.speed = 10;
        }

        // moves the Vertician enemy Up/Down
        public override void moveAISprite()
        {
            if (location.Y < upBound) { direction = 1f; }
            if (location.Y > downBound) { direction = -1f; }
            location.Y += speed * direction;
        }
    }
}
