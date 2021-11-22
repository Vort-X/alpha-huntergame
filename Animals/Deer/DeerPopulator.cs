using Godot;
using HunterGame.Animals.Population;

namespace HunterGame.Animals.Deer
{
	public class DeerPopulator : Populator<Deer>
	{
		[Export] private string PathToDeerScene { get; set; }
		public override void _Ready()
		{
			PathToAnimalNode = PathToDeerScene;
			base._Ready();
		}
	}
}
