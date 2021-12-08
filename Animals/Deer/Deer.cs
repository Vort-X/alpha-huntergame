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
		private IBehaviour _runFromEnemyBehaviour;
		private IBehaviour _flockBehaviour;

		public override void _Ready()
		{
			_wanderBehaviour = GetNode<Wander>("Wander");
			_avoidCliffBehaviour = GetNode<AvoidCliff>("AvoidCliff");
			_runFromEnemyBehaviour = GetNode<RunFromEnemy>("RunFromEnemy");
			_flockBehaviour = GetNode<Flock>("Flock");

			base._Ready();
		}

		protected override Vector2 ChainOfTargets()
		{
			return _avoidCliffBehaviour
					.Target(GlobalPosition, _runFromEnemyBehaviour
						.Target(GlobalPosition, 
							(_wanderBehaviour.Target(GlobalPosition, Velocity) + 
							_flockBehaviour.Target(GlobalPosition, Velocity)).Normalized()
						));

		}
	}
}

