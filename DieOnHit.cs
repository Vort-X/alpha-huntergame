using Godot;
using System;
using HunterGame.Animals;

public class DieOnHit : Area2D
{
	public override void _Ready()
	{
		
	}

	public void OnHit()
	{
		GetParent<Animal>().GracefulDeath();
	}
}
