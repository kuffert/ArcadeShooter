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
    // This class contains all the attributes of the player's ship.
    // This includes its image, current location, rotation angle, and H/V velocities
    public class Player
    {
        public Texture2D image;          // Image of the player
        public Texture2D retImage;       // reticle Image
        public Vector2 location;         // Players current location
        public Vector2 reticleLoc;       // location of the targeting reticle
        public float rotation;           // players angular rotation
        public float vel;                // players current velocity
        public List<AISprite> bullets;   // List of bullets the player has fired
        public SoundEffect bulletSound;  // laser sound effect
        int counter;                     // sound interval counter

        // Player Constructor
        public Player()
        {
            location = new Vector2(ArcadeShooter.width/2, ArcadeShooter.height/2);
            reticleLoc = new Vector2(750, 500);  
            bullets = new List<AISprite>();
            vel = 10f;
        }

        // Update the player location
        public void updatePlayer()
        {
            // The following allows the player to rotate to face the mouse:
            MouseState ms = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(ms.X, ms.Y);
            Vector2 rotDir = location - mouseLoc;
            rotation = (float)Math.Atan2(rotDir.Y, rotDir.X) - (float)(Math.PI * 0.5f);

            // Places the Reticle at the current mouse position
            reticleLoc = new Vector2(ms.X - retImage.Width/2, ms.Y - retImage.Height/2);

            // Moving the player based on input commands:
            // TODO: Modify movement system, possibly acceleration/speed reduction when key is released.
            // TODO: Block player from exiting boundaries
            KeyboardState curState = Keyboard.GetState();
            if (curState.IsKeyDown(Keys.W)) { location.Y -= vel; }  // W moves Up,
            if (curState.IsKeyDown(Keys.S)) { location.Y += vel; }  // S moves Down,
            if (curState.IsKeyDown(Keys.A)) { location.X -= vel; }  // A moves Left,
            if (curState.IsKeyDown(Keys.D)) { location.X += vel; }  // D moves right.

            // Firing projectiles
            // TODO: Garbage collection of projectiles beyond boundaries
            fireBullet(curState, location, mouseLoc);
            //moveAllBullets();
        }

        // Fires a bullet, adding it to the list of fired bullets.
        public void fireBullet(KeyboardState ks, Vector2 start, Vector2 end)
        {
            Vector2 offsetStart = new Vector2(start.X - image.Width / 2, start.Y - image.Height / 2);
            Vector2 offsetEnd = new Vector2(end.X - retImage.Width / 2, end.Y - retImage.Height / 2);
            if (ks.IsKeyDown(Keys.Space))
            {
                Bullet b = new Bullet(offsetStart, offsetEnd);
                playLaserSound();
                bullets.Add(b);
            }
        }

        // Move all bullets on the list
        public void moveAllBullets()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                ((Bullet)bullets[i]).moveAISprite();
            }
        }
        
        // Play sound at the appropriate interval
        public void playLaserSound()
        {
            if (counter == 5) { counter = 0; bulletSound.Play(); }
            counter++;
        }
    }
}



