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
        public static Vector2 playerCenter;     // origin of player image
        public static Vector2 origin;           // center of the screen
        SpriteEffects playerEffect;             // effects on the player sprite
        public static Texture2D bulletImage;    // image of the bullet
        public static Texture2D horizonImage;   // image of horizon enemy
        public static Texture2D verticianImage; // image of vertician enemy
        public static Texture2D fleelerImage;   // image of fleeler enemy
        public static Texture2D chaserImage;    // image of chaser enemy
        protected Texture2D introImage;         // first intro screen
        protected Texture2D intro2Image;        // second intro screen
        GraphicsDeviceManager graphics;         // graphical options 
        SpriteBatch spriteBatch;                // sprites to display
        public List<AISprite> enemies;          // list of enemies
        private List<Level> levels;             // list of levels
        SoundEffect backgroundTheme;            // background music
        SoundEffect successSound;               // Sound that plays when you win
        SpriteFont scoreFont;                   // Desired font
        Vector2 scoreLoc;                       // Location of score
        int score;                              // current player score
        int points;                             // number of points obtained per kill
        SpriteFont timeFont;                    // font for timer
        Vector2 timeLoc;                        // location of time
        int time;                               // time remaining
        SpriteFont winLoseFont;                 // font for the Win/Lose Message
        Vector2 winLoc;                         // win location
        Vector2 loseLoc;                        // lose location
        Boolean hasWon;                         // whether or not the player won
        Boolean hasLost;                        // whether or not the player has lost
        Boolean decrementTime;                  // whether or not to decrement time
        public string loseMessage;              // the lose message to be displayed to the player
        bool canMove;                           // boolean determines if anything on the screen can move
        bool intro1;                            // bool to start/end the intro1
        bool intro2;                            // bool to start/end the intro2
        int curLevel;                           // current level #

        public ArcadeShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = height;  // Sets the window height
            graphics.PreferredBackBufferWidth = width;    // Sets the window width
            graphics.IsFullScreen = true;
            origin = new Vector2(ArcadeShooter.width / 2, ArcadeShooter.height / 2);
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
            levels = new List<Level>();
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
            introImage = Content.Load<Texture2D>("introPlayer");
            intro2Image = Content.Load<Texture2D>("intro");
            playerImage = Content.Load<Texture2D>("playerShip");
            player.image = playerImage;
            reticle = Content.Load<Texture2D>("Reticle");
            player.retImage = reticle;
            bulletImage = Content.Load<Texture2D>("Bullet");
            horizonImage = Content.Load<Texture2D>("Horizon");
            verticianImage = Content.Load<Texture2D>("Vertician");
            fleelerImage = Content.Load<Texture2D>("Fleeler");
            chaserImage = Content.Load<Texture2D>("Chaser");
            playerEffect = SpriteEffects.None;

            // Load locations
            playerCenter = new Vector2(playerImage.Width / 2, playerImage.Height / 2);
            scoreLoc = new Vector2(10, 20);
            timeLoc = new Vector2(10, 40);
            winLoc = new Vector2(width / 2 - 175, height / 4);
            loseLoc = new Vector2(width / 2 - 425, height / 4);

            // Fonts
            scoreFont = Content.Load<SpriteFont>("scoreFont");
            winLoseFont = Content.Load<SpriteFont>("winLoseFont");
            timeFont = Content.Load<SpriteFont>("timeFont");

            // Boolean and counter loads
            score = 0;
            points = 100;
            time = 3000;
            decrementTime = false;
            hasWon = false;
            hasLost = false;
            canMove = false;
            intro1 = true;
            intro2 = false;
            curLevel = -1;

            // Sound loads
            player.bulletSound = Content.Load<SoundEffect>("Laser");
            backgroundTheme = Content.Load<SoundEffect>("base2");
            successSound = Content.Load<SoundEffect>("success");
            SoundEffectInstance loopBG = backgroundTheme.CreateInstance();
            loopBG.IsLooped = true;
            loopBG.Play();

            // Load Levels
            levels.Add(Level.buildLevelOne());
            levels.Add(Level.buildLevelTwo());
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
            // Checks key input methods
            KeyboardState ks = Keyboard.GetState();
            updateIntro(ks);
            escPressed(ks);
            nextLevel(ks);
            restartLevel(ks);
            

            // Updates Objects in the gameworld (if they are currently allowed to)
            if (canMove)
            {
                player.updatePlayer();           // Moves, Rotates, and Fires
                moveAISprites(enemies);          // Moves existing enemies
                moveAISprites(player.bullets);   // Moves existing bullets
            }

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

            //Draw Intro
            drawIntro(spriteBatch);
            // Draw Win
            drawWin(spriteBatch);
            // Check Lose
            drawLose(spriteBatch);
            // Draw Player
            spriteBatch.Draw(playerImage, player.location, null, Color.White, player.rotation, playerCenter, 1f, playerEffect, 0);
            // Draw bullets, enemies and powerups
            displayAISprites(player.bullets);
            displayAISprites(enemies);
            // Draw Reticle
            spriteBatch.Draw(reticle, player.reticleLoc, Color.White);
            // Draw Score
            spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), scoreLoc, Color.White);
            // Draw Timer
            spriteBatch.DrawString(timeFont, "Time Remaining: " + (time / 100).ToString(), timeLoc, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // When a passed in a list of AISprites (bullets, enemies)
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
                    score += points;
                    list1.Remove(list1[i]);
                }
            }
        }

        // Check if the player defeated all enemies in the level
        protected void checkWin()
        {
            hasWon = (enemies.Count == 0 && !intro1 && !intro2);
        }

        // Check if the player has run out of time or gotten hit.
        protected void checkLose()
        {
            if (player.collideEnemy(enemies))
            {
                canMove = false;
                hasLost = true;
                loseMessage = "You have been hit! Game over.";
            }
            if (time <= 0)
            {
                canMove = false;
                hasLost = true;
                loseMessage = "Ran out of time! Game over.";
            }
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
                decrementTime = false;
                sb.DrawString(winLoseFont, loseMessage, loseLoc, Color.White);
            }
        }

        // restart the game if the player decides to
        protected void restartLevel(KeyboardState ks)
        {
            if ((hasWon || hasLost) && ks.IsKeyDown(Keys.R))
            {
                player.location = origin;
                canMove = true;
                enemies.Clear();
                decrementTime = true;
                hasLost = false;
                hasWon = false;
                // Need to reset the enemies now
                time = 3000;
            }
        }

        // draw the game intro
        protected void drawIntro(SpriteBatch sb)
        {
            if (intro1) { sb.Draw(introImage, origin - new Vector2(introImage.Width / 2, introImage.Height / 2), Color.White); }
            if (intro2) { sb.Draw(intro2Image, origin - new Vector2(intro2Image.Width / 2, intro2Image.Height / 2), Color.White); }
        }

        // update intro
        protected void updateIntro(KeyboardState ks)
        {
            if (intro1 && ks.IsKeyDown(Keys.Space))
            {
                intro1 = false;
                intro2 = true;
            }
            if (intro2 && ks.IsKeyDown(Keys.Enter))
            {
                intro2 = false;
                canMove = true;
                decrementTime = true;
                updateLevel();
            }
        }

        // If the player has won a level, allow them to proceed to the next by pressing 
        // space
        protected void nextLevel(KeyboardState ks)
        {
            if (hasWon && ks.IsKeyDown(Keys.Space))
            {
                hasWon = false;
                updateLevel();
            }
        }

        // Updates the current level when called, loading a new list of enemies
        protected void updateLevel()
        {
            curLevel++;
            if (curLevel < 0) { return; }
            if (levels.Count <= curLevel) { return; }
            enemies = levels[curLevel].enemies;
            points += levels[curLevel].scoreMod;
        }
    }
}
