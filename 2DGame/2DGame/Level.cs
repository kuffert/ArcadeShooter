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
    // The level class contains what should be displayed on the screen
    // during a particular level. Currently this includes all of the 
    // neccessary enemies that need to appear, and the amount of score
    // players earn for killing enemies.
    /// </summary>
    public class Level
    {
        public int scoreMod;
        public List<AISprite> enemies;

        Level(int score)
        {
            this.scoreMod = score;
            this.enemies = new List<AISprite>();
        }

        public static Level buildLevelOne()
        {
            Level levelOne = new Level(0);

            AISprite horiz1 = new Horizon(new Vector2(0, 0), 1);
            AISprite horiz2 = new Horizon(new Vector2(500, ArcadeShooter.width / 16), -1);
            AISprite horiz3 = new Horizon(new Vector2(ArcadeShooter.height - 200, ArcadeShooter.width / 16 * 2), 1);
            AISprite horiz4 = new Horizon(new Vector2(ArcadeShooter.height - 100, ArcadeShooter.width / 16 * 3), -1);
            AISprite verti1 = new Vertician(new Vector2(ArcadeShooter.width / 8, 0), -1);
            AISprite verti2 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 2, 400), 1);
            AISprite verti3 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 7, 0), -1);
            AISprite verti4 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 6, 400), 1);
            AISprite chase1 = new Chaser(new Vector2(50, 100));
            AISprite chase2 = new Chaser(new Vector2(1600, 500));
            AISprite chase3 = new Chaser(new Vector2(ArcadeShooter.width / 2, 1000));
            AISprite fleel1 = new Fleeler(new Vector2(ArcadeShooter.width / 4, 600));
            AISprite fleel2 = new Fleeler(new Vector2(ArcadeShooter.width / 4 * 3, 600));
            AISprite fleel3 = new Fleeler(new Vector2(ArcadeShooter.width / 4, 300));
            AISprite fleel4 = new Fleeler(new Vector2(ArcadeShooter.width / 4 * 3, 300));

            levelOne.enemies.Add(horiz1);
            levelOne.enemies.Add(horiz2);
            levelOne.enemies.Add(horiz3);
            levelOne.enemies.Add(horiz4);
            levelOne.enemies.Add(verti1);
            levelOne.enemies.Add(verti2);
            levelOne.enemies.Add(verti3);
            levelOne.enemies.Add(verti4);
            levelOne.enemies.Add(chase1);
            levelOne.enemies.Add(chase2);
            levelOne.enemies.Add(chase3);
            levelOne.enemies.Add(fleel1);
            levelOne.enemies.Add(fleel2);
            levelOne.enemies.Add(fleel3);
            levelOne.enemies.Add(fleel4);

            return levelOne;
        }

        // Builds the second level of the game
        public static Level buildLevelTwo()
        {
            Level levelTwo = new Level(50);

            AISprite horiz1 = new Horizon(new Vector2(0, 0), 1);
            AISprite horiz2 = new Horizon(new Vector2(0, ArcadeShooter.height / 8), 1);
            AISprite horiz3 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 2), 1);
            AISprite horiz4 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 3), 1);
            AISprite horiz5 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 4), 1);
            AISprite horiz6 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 5), 1);
            AISprite horiz7 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 6), 1);
            AISprite horiz8 = new Horizon(new Vector2(0, ArcadeShooter.height / 8 * 7), 1);
            AISprite verti1 = new Vertician(new Vector2(0, 0), -1);
            AISprite verti2 = new Vertician(new Vector2(ArcadeShooter.width / 8, 0), -1);
            AISprite verti3 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 2, 0), -1);
            AISprite verti4 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 3, 0), -1);
            AISprite verti5 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 4, 0), -1);
            AISprite verti6 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 5, 0), -1);
            AISprite verti7 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 6, 0), -1);
            AISprite verti8 = new Vertician(new Vector2(ArcadeShooter.width / 8 * 7, 0), -1);

            levelTwo.enemies.Add(horiz1);
            levelTwo.enemies.Add(horiz2);
            levelTwo.enemies.Add(horiz3);
            levelTwo.enemies.Add(horiz4);
            levelTwo.enemies.Add(horiz5);
            levelTwo.enemies.Add(horiz6);
            levelTwo.enemies.Add(horiz7);
            levelTwo.enemies.Add(horiz8);
            levelTwo.enemies.Add(verti1);
            levelTwo.enemies.Add(verti2);
            levelTwo.enemies.Add(verti3);
            levelTwo.enemies.Add(verti4);
            levelTwo.enemies.Add(verti5);
            levelTwo.enemies.Add(verti6);
            levelTwo.enemies.Add(verti7);
            levelTwo.enemies.Add(verti8);

            return levelTwo;
        }

        // Builds the third level of the game
        public static Level buildLevelThree()
        {
            Level levelThree = new Level(100);

            AISprite chase1 = new Chaser(new Vector2(0, 0));
            AISprite chase2 = new Chaser(new Vector2(ArcadeShooter.width / 8, 0));
            AISprite chase3 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 2, 0));
            AISprite chase4 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 3, 0));
            AISprite chase5 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 4, 0));
            AISprite chase6 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 5, 0));
            AISprite chase7 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 6, 0));
            AISprite chase8 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 7, 0));
            AISprite chase9 = new Chaser(new Vector2(0, ArcadeShooter.height - 50));
            AISprite chase10 = new Chaser(new Vector2(ArcadeShooter.width / 8, ArcadeShooter.height - 50));
            AISprite chase11 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 2, ArcadeShooter.height - 50));
            AISprite chase12 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 3, ArcadeShooter.height - 50));
            AISprite chase13 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 4, ArcadeShooter.height - 50));
            AISprite chase14 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 5, ArcadeShooter.height - 50));
            AISprite chase15 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 6, ArcadeShooter.height - 50));
            AISprite chase16 = new Chaser(new Vector2(ArcadeShooter.width / 8 * 7, ArcadeShooter.height - 50));


            levelThree.enemies.Add(chase1);
            levelThree.enemies.Add(chase2);
            levelThree.enemies.Add(chase3);
            levelThree.enemies.Add(chase4);
            levelThree.enemies.Add(chase5);
            levelThree.enemies.Add(chase6);
            levelThree.enemies.Add(chase7);
            levelThree.enemies.Add(chase8);
            levelThree.enemies.Add(chase9);
            levelThree.enemies.Add(chase10);
            levelThree.enemies.Add(chase11);
            levelThree.enemies.Add(chase12);
            levelThree.enemies.Add(chase13);
            levelThree.enemies.Add(chase14);
            levelThree.enemies.Add(chase15);
            levelThree.enemies.Add(chase16);

            return levelThree;
        }
    }
}
