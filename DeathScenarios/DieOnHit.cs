using Godot;
using System;
using HunterGame.Animals;
using HunterGame.GameState;

public class DieOnHit : Area2D
{
	private GameStateManager _gameStateManager;
	public override void _Ready()
	{
		_gameStateManager = GetNode<GameStateManager>("/root/GameStateManager");
	}

	public void OnHit()
	{
		_gameStateManager.Kill(GetParent<Animal>());
	}
}
