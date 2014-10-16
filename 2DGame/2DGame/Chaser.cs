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
        Chaser(Vector2 loc, float dir)
        {
            this.image = ArcadeShooter.chaserImage;
            this.location = loc;
            this.leftBound = 0;
            this.rightBound = ArcadeShooter.width - image.Width;
            this.upBound = 0;
            this.downBound = ArcadeShooter.height - image.Height;
            this.direction = dir;
            this.speed = 8; // Slower than player
        }

        // Inherited from AISprite/Enemy superclass:

        // Moves the Chaser enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
