using System;
using System.IO;

namespace SharpStory
{
    class Program
    {

        static void Main(string[] args)
        {
            World akrazia = new World("Akrazia", 15, 15);
            
            akrazia.GenerateRandomWorld();
            Console.Clear();
            // Console.WriteLine(akrazia.WorldName);
            // Console.WriteLine(Console.WindowWidth + " | " + Console.WindowHeight);
            bool quit = false;
            while (!quit)
            {
                
                akrazia.Render();
                Utility.PollEvents(ref quit, akrazia);
            }
            Utility.CleanLog();
        }
    }
}
