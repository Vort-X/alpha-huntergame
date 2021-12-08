using Godot;
using HunterGame.Animals.Deer;
using HunterGame.Animals.Population;
using HunterGame.Movement;
using System;
using System.Collections.Generic;
using System.Linq;

public class Flock : Node, IBehaviour
{
	private Population population;
	[Export] public int SeparateMultiplier { get; set; } = 10;
	[Export] public int AlignMultiplier { get; set; } = 5;
	[Export] public int CohesionMultiplier { get; set; } = 1;
	[Export] public int SeparationRadius { get; set; } = 50;
	[Export] public int VisionRadius { get; set; } = 120;

	public override void _Ready()
	{
		population = GetNode<Population>("/root/Population");
	}

	public Vector2 Target(Vector2 position, Vector2 direction)
	{
		var deers = GetDeers();
		if (deers.Count() <= 1) return direction;
		var sep = Separate(position, deers) * SeparateMultiplier;
		var ali = Align(position, deers) * AlignMultiplier;
		var coh = Cohesion(position, deers) * CohesionMultiplier;
		return (sep + ali + coh).Normalized();
	}
	
	private IEnumerable<Deer> GetDeers() => population.Animals.Where(a => a is Deer).Cast<Deer>();
	
	private Vector2 Separate(Vector2 position, IEnumerable<Deer> deers)
	{
		var sum = new Vector2(0, 0);
		int count = 0;
		foreach (var d in deers) 
		{
			var dist = position.DistanceTo(d.GlobalPosition); //(position - d.GlobalPosition).Length();
			if (dist > 0 && dist < SeparationRadius)
			{
				sum += (position - d.GlobalPosition).Normalized() / dist;
				count++;
			}
		}
		if (count > 0)
		{
			sum = (sum / count).Normalized();
		}
		return sum;
	}
	
	private Vector2 Align(Vector2 position, IEnumerable<Deer> deers)
	{
		var sum = new Vector2(0, 0);
		int count = 0;
		foreach (var d in deers) 
		{
			var dist = position.DistanceTo(d.GlobalPosition);
			if (dist > 0 && dist < VisionRadius)
			{
				sum += d.GetVelocity();
				count++;
			}
		}
		if (count > 0)
		{
			sum = (sum / count).Normalized();
		}
		return sum;
	}
	
	private Vector2 Cohesion(Vector2 position, IEnumerable<Deer> deers)
	{
		var sum = new Vector2(0, 0);
		int count = 0;
		foreach (var d in deers) 
		{
			var dist = position.DistanceTo(d.GlobalPosition);
			if (dist > 0 && dist < VisionRadius)
			{
				sum += d.GlobalPosition;
				count++;
			}
		}
		if (count > 0)
		{
			sum /= count;
			return (sum - position).Normalized();
		}
		return sum;
	}
}
