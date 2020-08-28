using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace SharpStory
{

    public static class Utility
    {
        public static void PollEvents(ref bool quitEvent, World to_change)
        {
            // Poll...POT...Poll...POT
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                quitEvent = true;
            }
            if (keyInfo.Key == ConsoleKey.O)
            {
                System.Console.WriteLine("Generating CA");
                // Something's wrong with the types
                to_change.GenerateWorldCA(1);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }
    }

    public class RandomGenerationUtils
    {
        public static Vector2[,] CellularAutomataMatrix(Vector2 coordinates, int worldWidth, int WorldHeight)
        {
            Vector2[,] new_coords = new Vector2[3, 3];
            // TODO : Code like this is how you get fired.
            // Also probably easily fixable with lambdas ?
            // Seriously tho, fix this shit
            new_coords[0, 0] = coordinates.X == 0 | coordinates.Y == 0 ? coordinates : coordinates + new Vector2(-1);
            new_coords[1, 0] = coordinates.Y == 0 ? coordinates : coordinates + new Vector2(0, -1);
            new_coords[2, 0] = coordinates.X == worldWidth | coordinates.Y == 0 ? coordinates : coordinates + new Vector2(1, -1);
            new_coords[1, 1] = coordinates;
            new_coords[0, 1] = coordinates.X == 0 ? coordinates : coordinates + new Vector2(-1, 0);
            new_coords[0, 2] = coordinates.X == 0 | coordinates.Y == WorldHeight ? coordinates : coordinates + new Vector2(-1, 1);
            new_coords[2, 2] = coordinates.X == worldWidth | coordinates.Y == WorldHeight ? coordinates : coordinates + new Vector2(1);
            return new_coords;
        }

        public static int CountOccurences(List<Land.LandType> list)
        {
            int[] occurenceList = new int[list.Count];
            Land.LandType[] typelist = new Land.LandType[list.Count];
            int counter = 0;
            int cpt = 0;
            foreach (var item in list)
            {
                // This particular line was legit just copied from stack overflow without any kind of thought put into it.
                // TODO: Actually read how this works ? Linq is weird.
                occurenceList[counter++] = ((from temp in list where temp.Equals(item) select temp).Count());

                typelist[cpt++] = item;

            }
            int value = Array.IndexOf(occurenceList, occurenceList.Max());

            // Bug right here where you're giving it the maximum value instead of a enum type, and thus give the wrong color to each part of land
            return (int)typelist[value];
        }


    }
}