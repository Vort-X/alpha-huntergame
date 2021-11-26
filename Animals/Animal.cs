using Godot;

namespace HunterGame.Animals
{
    public class Animal: KinematicBody2D
    {
        [Export] public float MaxSpeed { get; set; } = 100f;
        [Export] public float Mass { get; set; } = 1f;
	
        protected Vector2 Velocity = Vector2.Zero;

        public override void _PhysicsProcess(float delta)
        {
            var desired = ChainOfTargets();

            Velocity = MoveAndSlide(Velocity + (desired * MaxSpeed - Velocity) / Mass);
        }

        protected virtual Vector2 ChainOfTargets()
        {
            return Vector2.Zero;
        }
    }
}