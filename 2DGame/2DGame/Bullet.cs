using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    class Bullet
    {
        Texture2D image;
        Vector2 location;
        // Need something to keep track of direction

        Bullet(Vector2 loc)
        {
            this.location = loc;

        }
    }
}
