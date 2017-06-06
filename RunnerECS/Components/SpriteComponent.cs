using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace RunnerECS.Components
{
    public class SpriteComponent : IEngineComponent
    {
        public Texture2D Texture { get; set; }
    }
}
