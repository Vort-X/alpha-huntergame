using System.Collections.Generic;
using System.Collections.ObjectModel;
using Godot;

namespace HunterGame.Animals.Population
{
	public class Population : Node
	{
		private List<Animal> _animals;
		private List<Predator> _predators;
		private List<Prey> _preys;
		private Hunter _hunter;


		public List<Animal> Animals => _animals;
		public List<Predator> Predators => _predators;
		public List<Prey> Preys => _preys;

		public override void _Ready()
		{
			_animals = new List<Animal>();
			_predators = new List<Predator>();
			_preys = new List<Prey>();
		}

		public List<GameActor> GetAllGameActors()
		{
			List<GameActor> actors = new List<GameActor>(_animals)
			{
				_hunter
			};
			return actors;
		}

		public List<GameActor> GetAllEnemies()
		{
			List<GameActor> enemies = new List<GameActor>(_predators)
			{
				_hunter
			};
			return enemies;
		}

		public List<GameActor> GetAllPrays()
		{
			List<GameActor> preys = new List<GameActor>(_preys)
			{
				_hunter
			};
			return preys;
		}

		public void AddPrey(Prey prey)
		{
			_animals.Add(prey);
			_preys.Add(prey);
			
			AddChild(prey);
		}

		public void RemovePrey(Prey prey)
		{
			_animals.Remove(prey);
			_preys.Remove(prey);
			
			prey.QueueFree();
		}

		public void AddPredator(Predator predator)
		{
			_animals.Add(predator);
			_predators.Add(predator);
			
			AddChild(predator);
		}

		public void RemovePredator(Predator predator)
		{
			_animals.Remove(predator);
			_predators.Remove(predator);
			
			predator.QueueFree();
		}

		public void AddHunter(Hunter hunter)
		{
			_hunter = hunter;
		}

		public Vector2 GetHunterPosition()
		{
			return _hunter.GlobalPosition;
		}
		
	}
}
