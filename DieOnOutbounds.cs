using Godot;
using System;
using HunterGame.Animals;
using HunterGame.GameState;

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
		
		switch (GetParent())
		{
			case Animal animal: 
				_gameStateManager.KillAnimal(animal);
				break;
			case Hunter _:
				_gameStateManager.KillHunter();
				break;
		}
	}
}
