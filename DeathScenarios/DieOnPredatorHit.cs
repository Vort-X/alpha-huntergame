using Godot;
using HunterGame.Behaviours;
using HunterGame.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame
{
	public class DieOnPredatorHit : Node
	{
		private GameStateManager _gameStateManager;

		public override void _Ready()
		{
			_gameStateManager = GetNode<GameStateManager>("/root/GameStateManager");
			GetParent<ChasePray>().OnBiteRange += OnBiteRangeKiller;
		}

		public void OnBiteRangeKiller(object sender, EventArgs e)
		{
			_gameStateManager.Kill(((GameActorEventArg)e).ActorEventArgs);
		}
	}
}
