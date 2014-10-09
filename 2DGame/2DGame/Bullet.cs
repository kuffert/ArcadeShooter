﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    // This class represents and carries information corresponding
    // to the bullets that the player fires.
    public class Bullet : AISprite
    {
        Vector2 startLoc;            // Starting Location of the bullet
        Vector2 destination;         // Target Destination of the bullet
        float bulletSpeed;           // Travel speed of the bullet

        // Bullet Constructor
        public Bullet(Vector2 startLoc, Vector2 dest)
        {
            this.image = ArcadeShooter.bulletImage;
            this.startLoc = startLoc;
            this.location = startLoc;
            this.destination = dest;
            this.bulletSpeed = 20;
        }

        // Send the bullet from start loc to destination
        public override void moveAISprite()
        {
            Vector2 distVec = destination - startLoc;
            distVec.Normalize();
            location.X += distVec.X * bulletSpeed;
            location.Y += distVec.Y * bulletSpeed;
        }
    }
}
