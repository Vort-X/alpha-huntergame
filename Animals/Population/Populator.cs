using System;
using Godot;

namespace HunterGame.Animals.Population
{
	public class Populator<T> : Node2D  where T: Animal, new()
	{
		[Export] public int Amount { get; private set; }
		
		private PackedScene Scene;
		protected string PathToAnimalNode { get; set; }

		private CircleShape2D _area;
		private Population _population;
		private readonly Random _rand = new Random();

		public override void _Ready()
		{
			_area = (CircleShape2D) GetNode<CollisionShape2D>("Area/Circle").Shape;
			_population = GetNode<Population>("/root/Population");
			
			Scene = ResourceLoader.Load<PackedScene>(PathToAnimalNode);
		
			Populate();
		}

		private void Populate()
		{
			for (var i = 0; i < Amount; i++)
			{
				var position = GenerateRandomPoint();
				var animal = Scene.Instance<T>();
				animal.GlobalPosition = position;

				switch (animal)
				{
					case Prey prey:
						_population.AddPrey(prey);
						break;
					case Predator predator:
						_population.AddPredator(predator);
						break;
				}
			}
		}

		private Vector2 GenerateRandomPoint()
		{
			var theta = _rand.NextDouble() * 360;
			var radius = _rand.Next((int) _area.Radius / 2, (int) _area.Radius);

			var randomPoint = new Vector2((float) (radius * Math.Cos(theta)),
				(float) (radius * Math.Sin(theta)));

			return GlobalPosition + randomPoint;
		}
	}
}
