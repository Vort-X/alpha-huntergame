using Godot;
using HunterGame.Animals.Population;

namespace HunterGame.Animals.Wolf
{
	public class WolfPopulator : Populator<Wolf>
	{
		[Export] private string PathToWolfScene { get; set; }
		public override void _Ready()
		{
			PathToAnimalNode = PathToWolfScene;
			base._Ready();
		}
	}
}
