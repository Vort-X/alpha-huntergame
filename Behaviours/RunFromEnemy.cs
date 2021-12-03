using Godot;
using HunterGame.Animals;
using HunterGame.Animals.Population;
using HunterGame.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame.Behaviours
{

    public class RunFromEnemy : Node, IBehaviour
    {
        private List<Predator> predators;
        private Hunter hunter;

        public override void _Ready()
        {
            predators = GetNode<Population>("Population").Predators;
            hunter = GetNode<Hunter>("Hunter");
        }

        public Vector2 Target(Vector2 position, Vector2 direction)
        {
            Vector2 runVector = VectorToClosestEnemy(position);
            return runVector == Vector2.Zero ? direction :  position - runVector;
        }

        private Vector2 VectorToClosestEnemy(Vector2 position)
        {
            (Predator, double) closestPredator = ClosestVisiblePredatorWithDistance(position);
            double distanceToHunter = Math.Sqrt(Math.Pow(hunter.GlobalPosition.x - position.x, 2) + Math.Pow(hunter.GlobalPosition.y - position.y, 2));
            if (closestPredator.Item1 == null
                    || (distanceToHunter < 100
                    && closestPredator.Item2 > distanceToHunter))
            {
                return hunter.GlobalPosition;
            }

            return closestPredator.Item1 != null ? closestPredator.Item1.GlobalPosition : Vector2.Zero;
        }

        private (Predator, double) ClosestVisiblePredatorWithDistance(Vector2 position)
        {
            Predator closest = null;
            double closestDistance = 0;
            foreach (Predator p in predators)
            {
                double distanceToPredator = Math.Sqrt(Math.Pow(p.GlobalPosition.x - position.x, 2) + Math.Pow(p.GlobalPosition.y - position.y, 2));
                if (closest == null
                    || (distanceToPredator < 100
                    && closestDistance > distanceToPredator))
                {
                    closest = p;
                    closestDistance = distanceToPredator;
                }
            }

            return (closest, closestDistance);
        }

        
}
}
