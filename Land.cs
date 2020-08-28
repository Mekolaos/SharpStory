using System.Numerics;
using System.Collections;
using System.Linq;

namespace SharpStory
{
    public class Land
    {
        public enum LandType
        {
            dirt,
            water,
            tree,
            sand,
            road
        }
        public Vector2 Position;
        public LandType Type;
        Entity Animal;

        public Land(Vector2 position, LandType type)
        {
            Position = position;
            Type = type;
        }
        public Land(Entity animal):base()
        {
            Animal = animal;
        }

    }
}