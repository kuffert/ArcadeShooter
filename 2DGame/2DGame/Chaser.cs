using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This enemy type will follow the player ceaselessly, 
    // trying to bump into  and kill them.
    class Chaser : Enemy
    {
        public Chaser(Vector2 loc)
        {
            this.image = ArcadeShooter.chaserImage;
            this.location = loc;
            this.leftBound = 0;
            this.rightBound = ArcadeShooter.width - image.Width;
            this.upBound = 0;
            this.downBound = ArcadeShooter.height - image.Height;
            this.direction = 0;
            this.speed = 5; // Slower than player
        }

        // Inherited from AISprite/Enemy superclass:

        // Moves the Chaser enemy
        public override void moveAISprite()
        {
            Vector2 offset = new Vector2(ArcadeShooter.player.image.Width/2, ArcadeShooter.player.image.Height/2);
            Vector2 travelVector = ArcadeShooter.player.location - offset - this.location;
            travelVector.Normalize();
            location.X += travelVector.X * speed;
            location.Y += travelVector.Y * speed;
        }
    }
}
