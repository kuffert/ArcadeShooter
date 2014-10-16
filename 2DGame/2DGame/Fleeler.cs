using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This enemy type will constantly flee away from the direction
    // the player is facing.
    class Fleeler : Enemy
    {
        Fleeler(Vector2 loc, float dir)
        {
            this.image = ArcadeShooter.fleelerImage;
            this.location = loc;
            this.leftBound = 0;
            this.rightBound = ArcadeShooter.width - image.Width;
            this.upBound = 0;
            this.downBound = ArcadeShooter.height - image.Height;
            this.direction = dir;
            this.speed = 12;  // Faster than player
        }

        // Inherited from Enemy superclass:

        // Moves the Fleeler enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
