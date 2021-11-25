using HunterGame.Animals;

namespace Godot
{
	public class Rifle: KinematicBody2D
	{
		[Export] public int Ammo { get; set; } = 12;
		[Export] public float ReloadTime { get; set; } = 0.5f;
		[Export] public float ShotDistance { get; set; } = 1000f;

		private RayCast2D _rayCast; 
		
		private Vector2 _shotPosition;
		private Vector2 _shotDirection;

		private bool IsReloading => _timer.TimeLeft > 0;
		private bool CanShoot => !IsReloading && Ammo > 0;
		private bool _hasHit = false;

		private Timer _timer;

		public override void _Ready()
		{
			_timer = new Timer();
			_timer.OneShot = true;
			_timer.WaitTime = ReloadTime;

			AddChild(_timer);

			_rayCast = GetNode<RayCast2D>("RayCast");
			_rayCast.Enabled = true;
			
			
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
				
			Ammo--;
			_timer.Start();
		}

		private void Aim()
		{
			_shotPosition = new Vector2(GlobalPosition) + GetParent<Hunter>().Axis * ShotDistance / 30;
			
			_shotDirection = _shotPosition + GetParent<Hunter>().Axis * ShotDistance;
			//GD.Print($"{_shotPosition} -> {_shotDirection}");
		}

		private void Shoot()
		{
			_rayCast.CastTo = _shotDirection - _shotPosition;
			_rayCast.ForceRaycastUpdate();

			// var spaceState = GetWorld2d().DirectSpaceState;
			// var result = spaceState.IntersectRay(_shotPosition, _shotDirection,
			// 	new Collections.Array {this});
			//
			// GD.Print($"{result}");
			GD.Print($"Collided with: {_rayCast.GetCollider()}");

			_hasHit = _rayCast.IsColliding();
			if (_hasHit)
			{
				((DieOnHit)_rayCast.GetCollider()).OnHit();
			}
		}

		public override void _Draw()
		{
			var parent = GetParent<Hunter>();
			if (IsReloading)
			{
				//GD.Print($"Drawing: {_shotPosition} -> {_shotDirection}");
				DrawLine(_shotPosition - parent.GlobalPosition, (_shotDirection-parent.GlobalPosition), 
					_hasHit ? Colors.Red : Colors.White, 3f);
			}
			
		}
	}
}
