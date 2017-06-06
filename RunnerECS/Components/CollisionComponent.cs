using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RunnerECS.Components
{
    public class CollisionComponent: IEngineComponent
    {
        public Rectangle BoundingRectangle { get; set; }

        public bool Collided { get; set; } = false;
    }
}
