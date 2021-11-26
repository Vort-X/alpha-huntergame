using Godot;
using System;

public class Cliff : Sprite
{
	[Export] public float DangerDistance { get; private set; } = 50;

	private float _topY;
	private float _bottomY;
	
	private float _leftX;
	private float _rightX;

	public override void _Ready()
	{
		var viewport = GetViewportRect();
		var region = RegionRect;

		_topY = (viewport.End.y - region.End.y) / 2;
		_bottomY = viewport.End.y - _topY;
		
		_leftX = (viewport.End.x - region.End.x) / 2;
		_rightX = viewport.End.x - _leftX;
		
		Position = new Vector2(viewport.End.x / 2, viewport.End.y / 2);
		
		GD.Print($"topY:{_topY}, bottomY:{_bottomY}, leftX:{_leftX}, rightX:{_rightX}");
	}

	public bool IsNearLeftX(float x)
	{
		return x - DangerDistance < _leftX;
	}	
	public bool IsNearRightX(float x)
	{
		return x + DangerDistance > _rightX;
	}
	
	public bool IsNearTopY(float y)
	{
		return y - DangerDistance < _topY;
	}	
	
	public bool IsNearBottomY(float y)
	{
		return y + DangerDistance > _bottomY;
	}

	public bool IsOnCliff(Vector2 position)
	{
		return (position.y < _bottomY && position.y > _topY) && (position.x > _leftX && position.x < _rightX);
	}
}
