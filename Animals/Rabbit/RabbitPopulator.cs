using Godot;
using HunterGame.Animals.Population;

namespace HunterGame.Animals.Rabbit
{
	public class RabbitPopulator: Populator<Rabbit>
	{
		[Export] private string PathToRabbitScene { get; set; }
		public override void _Ready()
		{
			PathToAnimalNode = PathToRabbitScene;
			base._Ready();
		}
	}
}
