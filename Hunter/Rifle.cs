using System;
using HunterGame.Animals;
using HunterGame.Hunter;

namespace Godot
{
	public class Rifle: KinematicBody2D
	{
		[Export] public int Ammo { get; set; } = 12;
		[Export] public float ReloadTime { get; set; } = 0.5f;
		[Export] public float ShotDistance { get; set; } = 1000f;

		public event Action<int, int> OnShot;

		private RayCast2D _rayCast; 
		
		private Vector2 _shotPosition;
		private Vector2 _shotDirection;

		private bool IsReloading => _timer.TimeLeft > 0;
		private bool CanShoot => !IsReloading && _currentAmmo > 0;
		private bool _hasHit = false;
		private int _currentAmmo;

		private Timer _timer;

		public override void _Ready()
		{
			_timer = new Timer();
			_timer.OneShot = true;
			_timer.WaitTime = ReloadTime;

			AddChild(_timer);

			_rayCast = GetNode<RayCast2D>("RayCast");
			_rayCast.Enabled = true;

			_currentAmmo = Ammo;
			DrawSetTransform(Vector2.Zero, 0, Vector2.One);
		}

		public override void _PhysicsProcess(float delta)
		{
			if (Input.IsActionPressed("ui_accept"))
			{
				AimAndShoot();
			}
			
			Update();
		}

		private void AimAndShoot()
		{
			if (!CanShoot) return;
			
			Aim();
			Shoot();
				
			_currentAmmo--;
			_timer.Start();
			
			OnShot?.Invoke(_currentAmmo, Ammo);
		}

		private void Aim()
		{
			_shotPosition = new Vector2(GlobalPosition) + GetParent<Hunter>().Axis * ShotDistance / 30;
			
			_shotDirection = _shotPosition + GetParent<Hunter>().Axis * ShotDistance;
		}

		private void Shoot()
		{
			_rayCast.CastTo = _shotDirection - _shotPosition;
			_rayCast.ForceRaycastUpdate();
			
			_hasHit = _rayCast.IsColliding();
			if (_hasHit)
			{
				GD.Print(_rayCast.GetCollider());
				((DieOnHit)_rayCast.GetCollider()).OnHit();
			}
		}

		public override void _Draw()
		{
			var parent = GetParent<Hunter>();
			if (IsReloading)
			{
				DrawLine(_shotPosition - parent.GlobalPosition, (_shotDirection-parent.GlobalPosition), 
					_hasHit ? Colors.Red : Colors.White, 3f);
			}
			
		}
	}
}
