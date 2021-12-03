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
    public class ChasePray : Node, IBehaviour
    {
        private List<Prey> preys;

        public override void _Ready()
        {
            preys = GetNode<Population>("Population").Preys;
        }

        public Vector2 Target(Vector2 position, Vector2 direction)
        {
            Prey closestPrey = ClosestVisiblePray(position);
            return closestPrey != null ? closestPrey.GlobalPosition - position : direction;
        }

        private Prey ClosestVisiblePray(Vector2 position)
        {
            Prey closest = null;
            double closestDistance = 0;
            foreach (Prey p in preys)
            {
                double distanceToPrey = Math.Sqrt(Math.Pow(p.GlobalPosition.x - position.x, 2) + Math.Pow(p.GlobalPosition.y - position.y, 2));
                if (closest == null
                    || (distanceToPrey < 110
                    && closestDistance > distanceToPrey))
                {
                    closest = p;
                    closestDistance = distanceToPrey;
                }
            }

            return closest;
        }
    }
}
