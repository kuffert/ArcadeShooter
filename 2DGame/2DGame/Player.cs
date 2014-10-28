using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DGame
{
    /// <summary>
    /// This class contains all the attributes of the player's ship.
    /// This includes its image, current location, rotation angle, and H/V velocities
    /// </summary>
    public class Player
    {
        public Texture2D image;          // Image of the player
        public Texture2D retImage;       // reticle Image
        public Vector2 location;         // Players current location
        public Vector2 reticleLoc;       // location of the targeting reticle
        public float rotation;           // players angular rotation
        private float vel;               // players current velocity
        public List<AISprite> bullets;   // List of bullets the player has fired
        public SoundEffect bulletSound;  // laser sound effect
        private int counter;             // interval counter for sounds and bullets

        // Player Constructor
        public Player()
        {
            location = ArcadeShooter.origin;
            reticleLoc = new Vector2(750, 500);  
            bullets = new List<AISprite>();
            vel = 10f;
        }

        // Update the player location
        public void updatePlayer()
        {
            counter++;

            // The following allows the player to rotate to face the mouse:
            MouseState ms = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(ms.X, ms.Y);
            Vector2 rotDir = location - mouseLoc;
            rotation = (float)Math.Atan2(rotDir.Y, rotDir.X) - (float)(Math.PI * 0.5f);

            // Places the Reticle at the current mouse position
            reticleLoc = new Vector2(ms.X - retImage.Width/2, ms.Y - retImage.Height/2);

            // Moving the player based on input commands:
            KeyboardState curState = Keyboard.GetState();
            move(curState);

            // Firing projectiles
            fireBullet(ms, location, mouseLoc);
        }

        // Fires a bullet, adding it to the list of fired bullets.
        public void fireBullet(MouseState ms, Vector2 start, Vector2 end)
        {
            Vector2 offsetStart = start - ArcadeShooter.playerCenter;
            Vector2 offsetEnd = end - ArcadeShooter.playerCenter;
            if (ms.LeftButton == ButtonState.Pressed && counter%5 == 0)
            {
                Bullet b = new Bullet(offsetStart, offsetEnd);
                bulletSound.Play();
                bullets.Add(b);
            }
        }

        // Moves the player, bounded by the window's edges.
        protected void move(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.W) && location.Y > image.Height / 2) 
            { location.Y -= vel; } // W moves Up,
            if (ks.IsKeyDown(Keys.S) && location.Y < ArcadeShooter.height - image.Height / 2)
            { location.Y += vel; } // S moves Down,
            if (ks.IsKeyDown(Keys.A) && location.X > image.Width / 2) 
            { location.X -= vel; } // A moves Left,
            if (ks.IsKeyDown(Keys.D) && location.X < ArcadeShooter.width - image.Width / 2) 
            { location.X += vel; } // D moves right.
        }

        // Checks if the player collided with anything on a list of sprites.
        // This can be used for powerups or enemies.
        public Boolean collideEnemy(List<AISprite> list)
        {
            Vector2 dist = new Vector2(100, 100);
            for (int i = 0; i < list.Count; i++)
            {
                Vector2 offset = new Vector2(image.Width / 2, image.Height / 2);
                dist = this.location-offset - list[i].location;
                if (Math.Abs(dist.X) <50 && Math.Abs(dist.Y) < 50)
                {
                    return true;
                }
            }
            return false;
        }
    }
}