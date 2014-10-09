using System;

namespace _2DGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ArcadeShooter game = new ArcadeShooter())
            {
                game.Run();
            }
        }
    }
#endif
}

