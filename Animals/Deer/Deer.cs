using Godot;
using System;
using HunterGame.Behaviours;
using HunterGame.Movement;


namespace HunterGame.Animals.Deer
{

	public class Deer : Prey
	{
		private IBehaviour _wanderBehaviour;
		private IBehaviour _avoidCliffBehaviour;

		public override void _Ready()
		{
			_wanderBehaviour = GetNode<Wander>("Wander");
			_avoidCliffBehaviour = GetNode<AvoidCliff>("AvoidCliff");

			base._Ready();
		}

		public override void _PhysicsProcess(float delta)
		{
			var desired = ChainOfTargets();

			Velocity = MoveAndSlide(Velocity + (desired * MaxSpeed - Velocity) / Mass);
		}

		private Vector2 ChainOfTargets()
		{
			return _avoidCliffBehaviour
				.Target(GlobalPosition, _wanderBehaviour
					.Target(GlobalPosition, Velocity));
		}

		protected override void GracefulDeath()
		{
			Population.RemovePrey(this);
			base.GracefulDeath();
		}
	}
}