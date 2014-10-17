using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DGame
{
    /// <summary>
    /// This class is an abstract parent of all objects in the gameworld
    /// That are NOT the player. This includes enemies, bullets, and powerups.
    /// </summary>
    public abstract class AISprite
    {
        public Texture2D image;
        public Vector2 location;

        public abstract void moveAISprite();
        
        // Determines if the AISprite has gone out of bounds.
        // Same function applies to any AISprite, enemies and bullets alike.
        public Boolean checkOOB()
        {
            return 
                  (location.X < -image.Width / 2                        // Left Bound
                || location.X > ArcadeShooter.width - image.Width/2     // Right Bound
                || location.Y < -image.Height / 2                       // Top Bound
                || location.Y > ArcadeShooter.height - image.Height/2); // Bottom Bound
        }

        // Checks if this sprite collided with anything. If it has,
        // Delete that object from the list.
        public Boolean checkSingleCollision(List<AISprite> list)
        {
            Vector2 dist = new Vector2(100, 100);
            for (int i = 0; i < list.Count; i++)
            {
                dist = this.location - list[i].location;
                if (Math.Abs(dist.X) < 25 && Math.Abs(dist.Y) < 25)
                {
                    list.Remove(list[i]);
                    return true; 
                }
            }
            return false;
        }
    }
}