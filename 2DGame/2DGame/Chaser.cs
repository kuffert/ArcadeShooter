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
        Chaser(Vector2 loc)
        {
            this.image = ArcadeShooter.chaserImage;
            this.location = loc;
        }

        // Inherited from AISprite/Enemy superclass:

        // Moves the Chaser enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
