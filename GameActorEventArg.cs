using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterGame
{
    public class GameActorEventArg : EventArgs
    {
        public GameActor ActorEventArgs { get; set; }

        public GameActorEventArg(GameActor actorEventArgs)
        {
            ActorEventArgs = actorEventArgs;
        }
    }
}
