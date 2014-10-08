using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGame
{
    class GameWorld
    {
        public Player player;      // the Player in the world
        int score;                 // current player score
        int bombCount;             // current # of bombs
        int time;                  // time left
        List<Enemy> enemies;       // list of enemies
        List<Powerup> powerups;    // list of powerups

        // Constructor for the game world
        public GameWorld(Player player)
        {
            this.player = player;
            score = 0;
            bombCount = 2;
            time = 120;
            enemies = new List<Enemy>();
            powerups = new List<Powerup>();
        }
    }
}
