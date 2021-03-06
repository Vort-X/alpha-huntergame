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

	public class RunFromEnemy : SearchTarget, IBehaviour
	{
		private Population populitaion;

		public override void _Ready()
		{
			populitaion = GetNode<Population>("/root/Population");
		}

		public Vector2 Target(Vector2 position, Vector2 direction)
		{
			GameActor target = NearestTargetPosition(position, populitaion.GetAllEnemies());
			return target == null ? direction :  (position - target.GlobalPosition).Normalized();
		}
	}
}
