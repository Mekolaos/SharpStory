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

        public static void Log(string logMessage, string logFile)
        {
            using (StreamWriter w = File.AppendText(logFile))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }
        }

        public static void CleanLog()
        {
            string[] files = Directory.GetFiles(".", "*.log"); ;
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }

    public class RandomGenerationUtils
    {
        public static Vector2[,] NeighboursMatrix(Vector2 coordinates, int WorldWidth, int WorldHeight)
        {
            /// <summary>Create a 3x3 matrix of coordinates to compare 
            ///          land type values for the cellular automaton.</summary>
            Vector2[,] new_coords = new Vector2[3, 3];
            coordinates -= new Vector2(1);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (coordinates == new Vector2(-1))
                    {
                        new_coords[i, j] = new Vector2(WorldWidth - 1, WorldHeight - 1);
                    }
                    else if (coordinates.X < 0)
                    {
                        new_coords[i, j] = new Vector2(WorldWidth - 1, coordinates.Y);
                    }
                    else if (coordinates.Y < 0)
                    {
                        new_coords[i, j] = new Vector2(coordinates.X, WorldHeight - 1);
                    }
                    else
                    {
                        new_coords[i, j] = coordinates + new Vector2(i, j);
                    }
                }
            }
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
                occurenceList[counter++] = ((from temp in list where temp.Equals(item) select temp).Count());
                // Once written by the great Ali Laouichi. cpt is a programmer's best tool, you can never use it too much.
                typelist[cpt++] = item;

            }
            int value = Array.IndexOf(occurenceList, occurenceList.Max());

            // Bug right here where you're giving it the maximum value instead of a enum type, and thus give the wrong color to each part of land
            return (int)typelist[value];
        }


    }
}