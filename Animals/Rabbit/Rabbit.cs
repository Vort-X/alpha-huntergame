using Godot;
using System;
using HunterGame.Animals;
using HunterGame.Behaviours;
using HunterGame.Movement;

namespace HunterGame.Animals.Rabbit
{

	public class Rabbit : Prey
	{
		private IBehaviour _wanderBehaviour;
		private IBehaviour _avoidCliffBehaviour;

		public override void _Ready()
		{
			_wanderBehaviour = GetNode<Wander>("Wander");
			_avoidCliffBehaviour = GetNode<AvoidCliff>("AvoidCliff");
			
			base._Ready();
		}

		protected override Vector2 ChainOfTargets()
		{
			return _avoidCliffBehaviour
				.Target(GlobalPosition, _wanderBehaviour
					.Target(GlobalPosition, Velocity));
		}
	}
}
