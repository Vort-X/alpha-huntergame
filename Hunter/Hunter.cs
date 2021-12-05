using HunterGame;
using HunterGame.Animals.Population;
using System;

namespace Godot
{
	public class Hunter : GameActor
	{
		[Export] public float MaxSpeed { get; set; } = 400f;
		[Export] public float Acceleration = 2000f;
		private Vector2 _velocity = Vector2.Zero;
		private Sprite _sprite;
		
		public Vector2 Axis = Vector2.Up;

		public float BodyRotation => Mathf.Deg2Rad(_sprite.RotationDegrees);

		public override void _Ready()
		{
			_sprite = GetNode<Sprite>("Sprite");
			GetNode<Population>("/root/Population").AddHunter(this);
		}

		public override void _PhysicsProcess(float delta)
		{
			var axis = GetInputAxis();

			if (axis == Vector2.Zero)
				ApplyFriction(Acceleration * delta);
			else
			{
				ApplyMovement(axis * Acceleration * delta);
				RotateBody(axis);
				Axis = axis;
			}

			_velocity = MoveAndSlide(_velocity);
		}

		private void RotateBody(Vector2 axis)
		{
			_sprite.RotationDegrees = Mathf.Rad2Deg(Vector2.Up.AngleTo(axis));
		}

		private void ApplyMovement(Vector2 acceleration)
		{
			_velocity += acceleration;
			_velocity = _velocity.Clamped(MaxSpeed);
		}

		private void ApplyFriction(float amount)
		{
			if (_velocity.Length() > amount)
				_velocity -= _velocity.Normalized() * amount;
			else
				_velocity = Vector2.Zero;
		}

		private Vector2 GetInputAxis()
		{
			var axis = Vector2.Zero;
			axis.x = Convert.ToInt32(Input.IsActionPressed("ui_right")) -
					 Convert.ToInt32(Input.IsActionPressed("ui_left"));
			axis.y = Convert.ToInt32(Input.IsActionPressed("ui_down")) -
					 Convert.ToInt32(Input.IsActionPressed("ui_up"));

			return axis.Normalized();
		}
	}
}
