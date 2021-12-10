using Godot;
using System;
using HunterGame.Animals.Population;
using HunterGame.GameState;

public class UiListener : Control
{
	private Label _bulletLabel;
	private Label _deathLabel;
	
	public override void _Ready()
	{
		_bulletLabel = GetNode<Label>("Bullets");
		_deathLabel = GetNode<Label>("Death");
		GetNode<Population>("/root/Population").Hunter.Rifle.OnShot += UpdateBulletCount;
		GetNode<GameStateManager>("/root/GameStateManager").OnHunterDeath += ShowDeathLabel;
	}

	private void UpdateBulletCount(int ammo, int maxAmmo)
	{
		_bulletLabel.Text = $"BULLETS {ammo}/{maxAmmo}";
	}

	private void ShowDeathLabel()
	{
		_deathLabel.Visible = true;
	}
}
