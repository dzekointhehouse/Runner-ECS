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
    public class PhysicsSystem
    {
        private int secondsElapsed = 0;

        public void Update(GameTime gameTime)
        {
            var movementComponents = ComponentManager.Get().GetComponents<MovementComponent>();

            foreach (var movementComponent in movementComponents)
            {
                var movement = movementComponent.Value as MovementComponent;

                var input = ComponentManager.Get().EntityComponent<InputComponent>(movementComponent.Key);
                var position = ComponentManager.Get().EntityComponent<PositionComponent>(movementComponent.Key);
                var sprite = ComponentManager.Get().EntityComponent<SpriteComponent>(movementComponent.Key);

                secondsElapsed += (int)gameTime.ElapsedGameTime.TotalSeconds;
                var speed = 5f + (secondsElapsed * 1f);

                if (input != null)
                {


                    if (position.Position.Y + sprite.Texture.Height >= AssetManager.Get().GameSceneViewport.Height &&
                        input.Keypressed)
                    {
                        movement.Velocity = new Vector2(0, -5);
                    }
                    else if (position.Position.Y <= AssetManager.Get().GameSceneViewport.Y + 20)
                    {
                        movement.Velocity = new Vector2(0, 3 + secondsElapsed);
                    }
                    else if (position.Position.Y + sprite.Texture.Height >= AssetManager.Get().GameSceneViewport.Height)
                    {
                        movement.Velocity = new Vector2(0, 0);
                    }
                }
                else
                {

                    movement.Velocity = new Vector2(-speed, 0);
                }

                position.Position += movement.Velocity;
            }
        }
    }
}

