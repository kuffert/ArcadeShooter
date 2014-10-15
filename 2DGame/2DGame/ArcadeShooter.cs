using System;
using System.Collections.Generic;
using System.Linq;
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
    /// This is the main type for your game
    /// </summary>
    public class ArcadeShooter : Microsoft.Xna.Framework.Game
    {
        Player player;                          // The player
        public static int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Texture2D playerImage;                  // image of the player
        Texture2D reticle;                      // image of the reticle
        static Vector2 origin;                  // origin of player image
        SpriteEffects playerEffect;             // effects on the player sprite
        Texture2D bombImage;                    // image of the bomb
        public static Texture2D bulletImage;    // image of the bullet
        public static Texture2D horizonImage;   // image of horizon enemy
        public static Texture2D verticianImage; // image of vertician enemy
        public static Texture2D fleelerImage;   // image of fleeler enemy
        public static Texture2D chaserImage;    // image of chaser enemy
        GraphicsDeviceManager graphics;         // graphical options 
        SpriteBatch spriteBatch;                // sprites to display
        int score;                              // current player score
        int bombCount;                          // current # of bombs
        int time;                               // time left
        public List<AISprite> enemies;          // list of enemies
        public List<AISprite> powerups;         // list of powerups

        public ArcadeShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = height;  // Sets the window height
            graphics.PreferredBackBufferWidth = width;    // Sets the window width
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player();
            enemies = new List<AISprite>();
            powerups = new List<AISprite>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load Sprite Batch
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load Images
            playerImage = Content.Load<Texture2D>("playerShip");
            player.image = playerImage;
            reticle = Content.Load<Texture2D>("Reticle");
            player.retImage = reticle;
            bulletImage = Content.Load<Texture2D>("Bullet");
            horizonImage = Content.Load<Texture2D>("Horizon");
            verticianImage = Content.Load<Texture2D>("Vertician");
            fleelerImage = Content.Load<Texture2D>("Fleeler");
            chaserImage = Content.Load<Texture2D>("Chaser");
            bombImage = Content.Load<Texture2D>("Bomb");
            // Other Loads
            origin = new Vector2(playerImage.Width / 2, playerImage.Height / 2);
            playerEffect = SpriteEffects.None;
            player.bulletSound = Content.Load<SoundEffect>("Laser");
            levelOne();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState ks = Keyboard.GetState();
            escPressed(ks);

            // Updates Objects in the gameworld
            player.updatePlayer();           // Moves, Rotates, and Fires
            moveAISprites(enemies);          // Moves existing enemies
            moveAISprites(player.bullets);   // Moves existing bullets
            moveAISprites(powerups);

            // Garbage Collection
            removeOOB(player.bullets);       // Removes OOB bullets
            removeOOB(enemies);              // Removes any enemies that somehow go OOB
            removeOOB(powerups);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // Draw Player
            spriteBatch.Draw(playerImage, player.location, null, Color.White, player.rotation, origin, 1f, playerEffect, 0); 
            // Draw bullets, enemies and powerups
            displayAISprites(player.bullets); 
            displayAISprites(enemies);
            displayAISprites(powerups); 
            // Draw Reticle
            spriteBatch.Draw(reticle, player.reticleLoc, Color.White);
            
            spriteBatch.End(); 

            base.Draw(gameTime);
        }

        // When a passed in a list of AISprites (bullets, enemies, powerups)
        // This function will display each one's corresponding image.
        protected void displayAISprites(List<AISprite> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            { 
                spriteBatch.Draw(sprites[i].image, sprites[i].location, Color.White); 
            }
        }

        // Takes a list of AISprites and moves them according to their inherent 
        // movement scripts. (i.e, vertician moves up/down, but horizon move left/right
        protected void moveAISprites(List<AISprite> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            { 
                sprites[i].moveAISprite(); 
            }
        }

        // Takes a list of AISprites and removes any that have gone out of bounds.
        protected void removeOOB(List<AISprite> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].checkOOB())
                {
                    sprites.Remove(sprites[i]);
                }
            }
        }

        // Initializes level one
        protected void levelOne()
        {
            AISprite horiz1 = new Horizon(new Vector2(0, 0), 1);
            AISprite verti1 = new Vertician(new Vector2(0, width / 4), -1);
            enemies.Add(horiz1);
            enemies.Add(verti1);
        }

        // ESC Button Pressed
        protected void escPressed(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
        }
    }
}
