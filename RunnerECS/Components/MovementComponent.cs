using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RunnerECS.Components
{
    public class MovementComponent:IEngineComponent
    {
        public Vector2 Velocity { get; set; }
    }
}
