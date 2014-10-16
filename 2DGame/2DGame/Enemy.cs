using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    abstract class Enemy : AISprite
    {
        // Enemy specific data
        protected float direction;
        protected int leftBound;
        protected int rightBound;
        protected int upBound;
        protected int downBound;
        protected int speed;
    }
}
