using Godot;
using System;
using System.Reflection;
using HunterGame.Movement;

public class AvoidCliff : Node, IBehaviour
{
	private Cliff _cliff;
	
	public override void _Ready()
	{
		_cliff = GetNode<Cliff>("/root/Cliff");
	}

	public Vector2 Target(Vector2 position, Vector2 direction)
	{
		if (_cliff.IsNearLeftX(position.x))
			return new Vector2(1, direction.y).Normalized();
		
		if (_cliff.IsNearRightX(position.x))
			return new Vector2(-1, direction.y).Normalized();
		
		if (_cliff.IsNearTopY(position.y))
			return new Vector2(direction.x, 1).Normalized();
		
		if (_cliff.IsNearBottomY(position.y))
			return new Vector2(direction.x, -1).Normalized();

		return direction;
	}
}
