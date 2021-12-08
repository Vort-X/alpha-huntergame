using Godot;

namespace HunterGame.Movement
{
	public interface IBehaviour
	{
		Vector2 Target(Vector2 position, Vector2 direction);
	}
}
