using System;
using Godot;
using HunterGame.Movement;

namespace HunterGame.Behaviours
{
	public class Wander : Node, IBehaviour
	{
		[Export] public float WonderDistance { get; set; } = 100f;
		[Export] public float WonderRadius { get; set; } = 50f;
		[Export] public int WonderDegreesChange { get; set; } = 90;

		private Random _rand = new Random();

		public Vector2 Target(Vector2 position, Vector2 direction)
		{
			var wanderTarget = position + WanderPoint(direction);
			return Seek(wanderTarget, position);
		}

		private Vector2 WanderPoint(Vector2 direction)
		{
			var circleCenter = direction.Normalized() * WonderDistance;
			var degree = direction.AngleTo(Vector2.Up);
			var theta = degree + _rand.Next(-WonderDegreesChange / 2, WonderDegreesChange / 2);

			var randomPoint = new Vector2((float) (WonderRadius * Math.Cos(theta)),
				(float) (WonderRadius * Math.Sin(theta)));

			return circleCenter + randomPoint;
		}


		private static Vector2 Seek(Vector2 target, Vector2 position)
		{
			var desired = (target - position).Normalized();

			return desired;
		}
	}
}
