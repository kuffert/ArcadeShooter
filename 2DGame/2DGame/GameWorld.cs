using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DGame
{
    class GameWorld
    {
        public Player player;
        int score;
        int bombCount;
        int time;
        ArrayList enemies;
        ArrayList bullets;
        ArrayList powerups;

        public GameWorld(Player player)
        {
            this.player = player;
            score = 0;
            bombCount = 2;
            time = 120;
            enemies = new ArrayList();
            bullets = new ArrayList();
            powerups = new ArrayList();
        }
    }
}
