using Godot;

namespace HunterGame.Animals
{
    public class Animal: KinematicBody2D
    {
        [Export] public float MaxSpeed { get; set; } = 100f;
        [Export] public float Mass { get; set; } = 1f;
	
        protected Vector2 Velocity = Vector2.Up;
        
        protected Population.Population Population { get; private set; }

        public override void _Ready()
        {
            Population = GetNode<Population.Population>("root/Population");
        }

        protected virtual void GracefulDeath()
        {
            QueueFree();
        }
    }
}