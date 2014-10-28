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
    /// ArcadeShooter represents the main functionality of the game. It contains
    /// all  necessary information the game needs to keep track of, including the
    /// player, all sprite images, the size of the game window, all displayed images,
    /// the bullets, powerups, and enemies, score and time. It manages the initializing
    /// of data, loading/unloading of content, drawing of images, and other game specific
    /// functionality.
    /// </summary>
    public class ArcadeShooter : Microsoft.Xna.Framework.Game
    {
        public static Player player;            // The player
        public static int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Texture2D playerImage;                  // image of the player
        Texture2D reticle;                      // image of the reticle
        static Vector2 origin;                  // origin of player image
        SpriteEffects playerEffect;             // effects on the player sprite
        public static Texture2D bulletImage;    // image of the bullet
        public static Texture2D horizonImage;   // image of horizon enemy
        public static Texture2D verticianImage; // image of vertician enemy
        public static Texture2D fleelerImage;   // image of fleeler enemy
        public static Texture2D chaserImage;    // image of chaser enemy
        GraphicsDeviceManager graphics;         // graphical options 
        SpriteBatch spriteBatch;                // sprites to display
        public List<AISprite> enemies;          // list of enemies
        SoundEffect backgroundTheme;            // background music
        SpriteFont scoreFont;                   // Desired font
        Vector2 scoreLoc;                       // Location of score
        int score;                              // current player score
        SpriteFont timeFont;                    // font for timer
        Vector2 timeLoc;                        // location of time
        int time;                               // time remaining
        SpriteFont winLoseFont;                 // font for the Win/Lose Message
        Vector2 winLoc;                         // win location
        Vector2 loseLoc;                        // lose location
        Boolean hasWon;                         // whether or not the player won
        Boolean hasLost;                        // whether or not the player has lost
        Boolean decrementTime;                  // whether or not to decrement time

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
            // Other Loads
            origin = new Vector2(playerImage.Width / 2, playerImage.Height / 2);
            playerEffect = SpriteEffects.None;
            player.bulletSound = Content.Load<SoundEffect>("Laser");
            backgroundTheme = Content.Load<SoundEffect>("base2");
            scoreFont = Content.Load<SpriteFont>("scoreFont");
            scoreLoc = new Vector2(10, 20);
            score = 0;
            timeFont = Content.Load<SpriteFont>("timeFont");
            timeLoc = new Vector2(10, 40);
            time = 3000;
            decrementTime = true;
            winLoseFont = Content.Load<SpriteFont>("winLoseFont");
            winLoc = new Vector2(width/2 - 175, height/4);
            loseLoc = new Vector2(0, 0);
            hasWon = false;
            hasLost = false;
            // Plays background music
            SoundEffectInstance loopBG = backgroundTheme.CreateInstance();
            loopBG.IsLooped = true;
            loopBG.Play();
            // Loads the enemies on the level
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

            // Garbage Collection
            removeOOB(player.bullets);       // Removes any OOB bullets
            removeOOB(enemies);              // Removes any OOB enemies

            // Check Collisions
            checkMultipleCollisions(player.bullets, enemies);

            // Decrease time every second
            if (decrementTime)
            { time -= 1; }
            // Check if the player won/lost
            checkWin();
            checkLose();

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
            // Draw Win
            drawWin(spriteBatch);
            // Check Lose
            drawLose(spriteBatch);
            // Draw Player
            spriteBatch.Draw(playerImage, player.location, null, Color.White, player.rotation, origin, 1f, playerEffect, 0); 
            // Draw bullets, enemies and powerups
            displayAISprites(player.bullets); 
            displayAISprites(enemies);
            // Draw Reticle
            spriteBatch.Draw(reticle, player.reticleLoc, Color.White);
            // Draw Score
            spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), scoreLoc, Color.White);
            // Draw Timer
            spriteBatch.DrawString(timeFont, "Time Remaining: " + (time/100).ToString(), timeLoc, Color.White);

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
            AISprite horiz2 = new Horizon(new Vector2(500, width/16), -1);
            AISprite horiz3 = new Horizon(new Vector2(height-200, width/16*2), 1);
            AISprite horiz4 = new Horizon(new Vector2(height-100, width/16*3), -1);
            AISprite verti1 = new Vertician(new Vector2(width / 8, 0), -1);
            AISprite verti2 = new Vertician(new Vector2(width / 8*2, 400), 1);
            AISprite verti3 = new Vertician(new Vector2(width /8*7, 0), -1);
            AISprite verti4 = new Vertician(new Vector2(width / 8*6, 400), 1);
            AISprite chase1 = new Chaser(new Vector2(50, 100));
            AISprite chase2 = new Chaser(new Vector2(1600, 500));
            AISprite chase3 = new Chaser(new Vector2(ArcadeShooter.width / 2, 1000));
            enemies.Add(horiz1); 
            enemies.Add(horiz2); 
            enemies.Add(horiz3); 
            enemies.Add(horiz4); 
            enemies.Add(verti1);
            enemies.Add(verti2);
            enemies.Add(verti3);
            enemies.Add(verti4);
            enemies.Add(chase1); 
            enemies.Add(chase2); 
            enemies.Add(chase3); 
        }

        // Quit the game if the escape button is pressed
        protected void escPressed(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
        }

        // Checks if anything on one list collided with sprites on
        // another (For example, bullets with enemies). If it does,
        // delete it from the list.
        protected void checkMultipleCollisions(List<AISprite> list1, List<AISprite> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].checkSingleCollision(list2))
                {
                    score += 1000;
                    list1.Remove(list1[i]);
                }
            }
        }

        // Check if the player defeated all enemies in the level
        protected void checkWin()
        {
            hasWon = (enemies.Count == 0);
        }

        // Check if the player has run out of time or gotten hit.
        protected void checkLose()
        {
            hasLost = (player.collideEnemy(enemies) || time <= 0);
        }

        // Display the Win announcement if the player wins
        protected void drawWin(SpriteBatch sb)
        {
            if (hasWon)
            {
                decrementTime = false;
                sb.DrawString(winLoseFont, "You've Won!", winLoc, Color.White);
            }
        }

        // Check if the player lost, either by running out of time, or getting hit.
        protected void drawLose(SpriteBatch sb)
        {
            if (hasLost) 
            {
                sb.DrawString(winLoseFont, "Out of time! Game Over", loseLoc, Color.White);
            }
        }
    }
}
