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
            using (Visualizer window = new Visualizer())
            {
                window.Run();
            }
        }
    }
#endif
}

