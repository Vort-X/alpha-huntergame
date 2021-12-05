using Godot;
using HunterGame.Animals;
using HunterGame.Animals.Population;
using HunterGame.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame.Behaviours
{
	public class ChasePray : SearchTarget, IBehaviour
	{
		private Population populataion;
		public event EventHandler OnBiteRange;
		[Export] public int BiteRange { get; set; }

		public override void _Ready()
		{
			populataion = GetNode<Population>("/root/Population");
		}

		public Vector2 Target(Vector2 position, Vector2 direction)
		{
			GameActor target = NearestTargetPosition(position, populataion.GetAllPrays());
			if(target != null) CheckIfTargetOnBiteRange(position, target);
			return target == null ? direction : (target.GlobalPosition - position).Normalized();
		}

		private void CheckIfTargetOnBiteRange(Vector2 position, GameActor target)
		{
			if (FindDistanceToTarget(position, target.GlobalPosition) <= BiteRange) 
				OnBiteRange?.Invoke(this, new GameActorEventArg(target));
		}
	}
}
