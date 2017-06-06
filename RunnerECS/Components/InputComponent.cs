using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace RunnerECS.Components
{
    public class InputComponent : IEngineComponent
    {
        public Keys JumpKey { get; set; }
        public bool Keypressed { get; set; } = false;
    }
}
