using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    abstract class Enemy
    {
        protected Texture2D image;
        protected Vector2 location;


        // Methods for the subclasses:
        // Displays the enemy
        protected abstract void displayEnemy();
        // Moves the enemy based on its mechanic
        protected abstract void moveEnemy();
    }
}
