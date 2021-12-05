using Godot;
using System;
using HunterGame.Animals;
using HunterGame.GameState;
using HunterGame;

public class DieOnOutbounds : Node2D
{
	private Cliff _cliff;
	private GameStateManager _gameStateManager;
	
	public override void _Ready()
	{
		_cliff = GetNode<Cliff>("/root/Cliff");
		_gameStateManager = GetNode<GameStateManager>("/root/GameStateManager");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (_cliff.IsOnCliff(GlobalPosition)) 
			return;

		_gameStateManager.Kill(GetParent<GameActor>());
	}
}
