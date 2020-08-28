using System;
using System.IO;

namespace SharpStory
{
    class Program
    {

        static void Main(string[] args)
        {
            World akrazia = new World("Akrazia", 20, 40);
            if(File.Exists("LandTypes.log")) File.Delete("LandTypes.log");
            akrazia.GenerateRandomWorld();
            Console.Clear();
            // Console.WriteLine(akrazia.WorldName);
            // Console.WriteLine(Console.WindowWidth + " | " + Console.WindowHeight);
            bool quit = false;
            while (!quit)
            {
                
                for (int i = 0; i < akrazia.WorldWidth; i++)
                {
                    for (int j = 0; j < akrazia.WorldHeight; j++)
                    {
                        Console.SetCursorPosition(i + 1, j + 1);
                        Console.BackgroundColor = (ConsoleColor)(int)akrazia.map[i, j].Type;
                        Console.WriteLine(" ");
                    }
                }
                // NICE TRY BITCH
                Utility.PollEvents(ref quit, akrazia);
            }
        }
    }
}
