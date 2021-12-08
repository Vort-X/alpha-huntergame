using Godot;
using HunterGame.Animals.Population;
using HunterGame.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame.Behaviours
{
	public abstract class SearchTarget : Node
	{
		[Export] public int SearchRadius { get; set; }

		protected GameActor NearestTargetPosition(Vector2 position, List<GameActor> targets)
		{
			GameActor closest = null;
			double closestDistance = 0;

			foreach (GameActor p in targets)
			{
				double distanceToTarget = FindDistanceToTarget(position, p.GlobalPosition);

				if (distanceToTarget != 0 && distanceToTarget < SearchRadius && (closest == null || closestDistance > distanceToTarget))
				{
					closest = p;
					closestDistance = distanceToTarget;
				}
			}

			return closest;

		}

		protected double FindDistanceToTarget(Vector2 position, Vector2 targetPosition)
		{
			return Math.Sqrt(Math.Pow(targetPosition.x - position.x, 2) + Math.Pow(targetPosition.y - position.y, 2));

		}


	}
}
