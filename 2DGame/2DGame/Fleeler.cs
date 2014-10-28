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
        int maxDist;  // Furthest distance a Fleeler can be from the player
        int xMove;    // either 1 or -1, moving away or towards
        int yMove;    // also either 1 or -1, moving away or towards
        public Fleeler(Vector2 loc)
        {
            this.image = ArcadeShooter.fleelerImage;
            this.location = loc;
            this.leftBound = 0;
            this.rightBound = ArcadeShooter.width - image.Width;
            this.upBound = 0;
            this.downBound = ArcadeShooter.height - image.Height;
            this.speed = 7;  // Faster than player
            this.xMove = 1;
            this.yMove = 1;
            this.maxDist = 300;
        }

        // Inherited from Enemy superclass:

        // Moves the Fleeler enemy
        public override void moveAISprite()
        {
            // If this enemy is to the left of the player, it needs to move left,
            // and vice versa. If it is within the distance range, it will move away
            // from the player. Otherwise it will move closer.
            Vector2 travelVector = this.location - (ArcadeShooter.player.location - ArcadeShooter.playerCenter);
            if (Math.Abs(travelVector.X) > maxDist) { xMove = -1; }
            if (Math.Abs(travelVector.Y) > maxDist) { yMove = -1; }
            travelVector.Normalize();
            location.X += travelVector.X * speed * xMove;
            location.Y += travelVector.Y * speed * yMove;
            xMove = 1;
            yMove = 1;
        }
    }
}
