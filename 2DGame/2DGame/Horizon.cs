﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    class Horizon : Enemy
    {

        Horizon(Vector2 loc)
        {
            this.location = loc;
        }

        // Inherited from Enemy superclass:
        
        // Displays the enemy
        protected override void displayEnemy()
        {
            throw new NotImplementedException();
        }

        // Moves the enemy
        protected override void moveEnemy()
        {
            throw new NotImplementedException();
        }
    }
}
