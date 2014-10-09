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
        Fleeler(Vector2 loc)
        {
            this.image = ArcadeShooter.fleelerImage;
            this.location = loc;
        }

        // Inherited from Enemy superclass:

        // Moves the Fleeler enemy
        public override void moveAISprite()
        {
            throw new NotImplementedException();
        }
    }
}
