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

		public ReadOnlyCollection<Animal> Animals => _animals.AsReadOnly();
		public ReadOnlyCollection<Predator> Predators => _predators.AsReadOnly();
		public ReadOnlyCollection<Prey> Preys => _preys.AsReadOnly();

		public override void _Ready()
		{
			_animals = new List<Animal>();
			_predators = new List<Predator>();
			_preys = new List<Prey>();
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
		
	}
}
