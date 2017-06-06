using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunnerECS.Components;
using RunnerECS.Content;
using RunnerECS.Managers;

namespace RunnerECS.Systems
{
    public class InputSystem
    {

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            var inputComponents = ComponentManager.Get().GetComponents<InputComponent>();

            foreach (var inputComponent in inputComponents)
            {
                var input = inputComponent.Value as InputComponent;

                input.Keypressed = false;

                if (newState.IsKeyDown(input.JumpKey))
                {
                    input.Keypressed = true;
                }
            }
        }
    }
}
