using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This class is an abstract parent of all objects in the gameworld
    // That are NOT the player. This includes enemies, bullets, and powerups.
    public abstract class AISprite
    {
        public Texture2D image;
        public Vector2 location;

        public abstract void moveAISprite();
        public Boolean checkOOB()
        {
            return (location.X < 0 || location.X > ArcadeShooter.width || location.Y < 0 || location.Y > ArcadeShooter.height);
        }
    }
}
