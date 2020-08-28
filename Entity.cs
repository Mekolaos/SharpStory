using System.Numerics;

namespace SharpStory
{
    public class Entity
    {
        string name;
        Vector2 position;
        float health;
        public string Name {get {return name;} set {name = value;}}
        public Vector2 Position {get {return position;} set {position = value;}}
        public float Health {get {return health;} set { health = value;}}
        public bool Alive = true;
        public EntityType Type;
        public enum EntityType
        {
            Rat,
            Cat,
            Dog
        }
        public int Threat;
        public Entity(string name, EntityType type)
        {
            Name = name;
            Type = type;
            Threat = (int)type+1;
        }

        public virtual void Bark(Entity to_fear)
        {
            if (this.Threat < to_fear.Threat && this.Alive) this.Flee();
            else to_fear.Flee();
        }

        public void Flee()
        {
            if (Alive) this.Position -= new Vector2(1);
        }

        public void MoveTo(Vector2 position)
        {
            if(Alive) this.Position = position;
        }

        public void Attack(float damage, Entity target)
        {
            if(target.Health > 0) target.Health -= damage;
            else target.Die();
        }

        public void Die()
        {
            if(Alive) this.Alive = false;
        }
    }
}