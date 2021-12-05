using Godot;
using HunterGame.Animals.Wolf;
using HunterGame.Behaviours;
using HunterGame.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame.DeathScenarios
{
	public class DieOnHunger : Node
	{
		private GameStateManager _gameStateManager;
		private Wolf _wolf;
		private SignalAwaiter signalAwaiter;
		[Export] public float TimeBeforeHungryDeath { get; set; }

		public override void _Ready()
		{
			_gameStateManager = GetNode<GameStateManager>("/root/GameStateManager");
			_wolf = GetParent<Wolf>();
			ResetTimer();
			_wolf.GetNode<ChasePray>("ChasePray").OnBiteRange += ResetTimer;
		}

		private void ResetTimer(object sender, EventArgs e)
		{
			ResetTimer();
		}

		private void ResetTimer()
		{
			signalAwaiter = ToSignal(GetTree().CreateTimer(TimeBeforeHungryDeath), "timeout");
		}


		public override void _Process(float delta)
		{	
			if (signalAwaiter.IsCompleted) DieOnHungerInTime();
		}

		private void DieOnHungerInTime()
		{
			_gameStateManager.Kill(_wolf);
		}
	}
}
