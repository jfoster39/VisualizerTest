using System;

namespace VisualizerTest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

