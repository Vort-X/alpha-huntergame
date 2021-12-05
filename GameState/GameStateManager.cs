using System.Collections.Generic;
using System.Linq;
using Godot;
using HunterGame.Animals;
using HunterGame.Animals.Population;

namespace HunterGame.GameState
{
	public class GameStateManager: Node
	{
		private Population _population;
		
		public override void _Ready()
		{
			_population = GetNode<Population>("/root/Population");
		}

		public void Kill(GameActor actor)
        {
			switch (actor)
			{
				case Animal animal:
					KillAnimal(animal);
					break;
				case Hunter hunt:
					KillHunter();
					break;

			}
		}

		private void KillAnimal(Animal animal)
		{
			 switch (animal)
			{
				case Predator pred:
					_population.RemovePredator(pred);
					break;
				case Prey prey: 
					_population.RemovePrey(prey);
					break;
			}
			 
			animal.QueueFree();
		}

		private void KillHunter()
		{
			new List<Predator>(_population.Predators).ForEach(_population.RemovePredator);
			new List<Prey>(_population.Preys).ForEach(p => _population.RemovePrey(p));

			GetTree().ReloadCurrentScene();
		}
		
	}
}
