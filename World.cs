using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace SharpStory
{
    public class World
    {
        const int maxHeight = 500;
        const int maxWidth = 500;
        string worldName;
        int worldHeight;
        int worldWidth;
        public string WorldName { get { return worldName; } set { worldName = value; } }
        public int WorldHeight { get { return worldHeight; } set { worldHeight = Enumerable.Range(5, maxHeight).Contains(value) ? value : maxHeight; } }
        public int WorldWidth { get { return worldWidth; } set { worldWidth = Enumerable.Range(5, maxWidth).Contains(value) ? value : maxWidth; } }
        public Land[,] map;

        public World(string name, int height, int width)
        {
            WorldName = name;
            WorldHeight = height;
            WorldWidth = width;
            map = new Land[width, height];
        }

        public void GenerateRandomWorld()
        {
            Random randomLandType = new Random();
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    map[x, y] = new Land(new Vector2(x, y), (Land.LandType)randomLandType.Next(0, 5));
                }
            }
        }

        public void GenerateWorldCA(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                for (int x = 0; x < worldWidth-1; x++)
                {
                    for (int y = 0; y < worldHeight-1; y++)
                    {
                        Vector2[,] CAMatrix = RandomGenerationUtils.CellularAutomataMatrix(new Vector2(x, y), WorldWidth, WorldHeight);
                        List<Land.LandType> landTypeCounter = new List<Land.LandType>();
                        foreach (var item in CAMatrix)
                        {
                            try
                            {
                                landTypeCounter.Add(map[(int)item.X, (int)item.Y].Type);

                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                System.Console.WriteLine(worldWidth + " | " + worldHeight);
                                System.Console.WriteLine("---------------------");
                                System.Console.WriteLine(item.X + " | " + item.Y + "\n\n");
                            }
                        }

                        int HighestCountType = RandomGenerationUtils.CountOccurences(landTypeCounter);
                        Console.SetCursorPosition(0, 0);
                        using (StreamWriter w = File.AppendText("LandTypes.log")){
                        Utility.Log(HighestCountType.ToString() + " : " + x.ToString() + " | " + y.ToString(), w);
                        }
                        map[x, y].Type = (Land.LandType)HighestCountType;

                    }
                }
            }
        }


    }
}