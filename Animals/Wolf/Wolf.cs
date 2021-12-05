using Godot;
using System;
using HunterGame.Animals;
using HunterGame.Behaviours;
using HunterGame.Movement;

namespace HunterGame.Animals.Wolf
{
	public class Wolf : Predator
	{
		private IBehaviour _wanderBehaviour;
		private IBehaviour _avoidCliffBehaviour;
		public IBehaviour _chasePreyBehaviour;

		public override void _Ready()
		{
			_wanderBehaviour = GetNode<Wander>("Wander");
			_avoidCliffBehaviour = GetNode<AvoidCliff>("AvoidCliff");
			_chasePreyBehaviour = GetNode<ChasePray>("ChasePray");

			base._Ready();
		}


		
		protected override Vector2 ChainOfTargets()
		{
			return _avoidCliffBehaviour
					.Target(GlobalPosition, _chasePreyBehaviour
						.Target(GlobalPosition, _wanderBehaviour
							.Target(GlobalPosition, Velocity)));
		}
	}
}
