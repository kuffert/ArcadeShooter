using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DGame
{
    class Player
    {
        public Texture2D image;
        public Vector2 location;
        public float vel;

        public Player()
        {
            location = new Vector2(400, 400);
            vel = 10f;
        }

        // Update the player location
        public void updatePlayer()
        {
            MouseState ms = Mouse.GetState();
            Vector2 direction = new Vector2(ms.X - image.Width/2 - location.X, ms.Y - image.Height/2 - location.Y);
            direction.Normalize();
            this.location.X += direction.X * vel;
            this.location.Y += direction.Y * vel;
                
        }
    }
}
