using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RunnerECS.Components;
using RunnerECS.Content;

namespace RunnerECS.Systems
{
    public class CollisionDetectionSystem
    {
        public bool CollisionOccured { get; private set; } = false;

        public void Update(GameTime gameTime)
        {

            var playerComponents = ComponentManager.Get().GetComponents<PlayerComponent>();
            var spawnComponents = ComponentManager.Get().GetComponents<SpawnComponent>();

            foreach (var playerComponent in playerComponents)
            {
                var playerCollision = ComponentManager.Get().EntityComponent<CollisionComponent>(playerComponent.Key);
                var spriteComponent = ComponentManager.Get().EntityComponent<SpriteComponent>(playerComponent.Key);
                var positionComponent = ComponentManager.Get().EntityComponent<PositionComponent>(playerComponent.Key);

                playerCollision.BoundingRectangle = 
                    new Rectangle((int) positionComponent.Position.X,
                                  (int) positionComponent.Position.Y,
                                  spriteComponent.Texture.Width,
                                  spriteComponent.Texture.Height);

                foreach (var spawnComponent in spawnComponents)
                {
                    var collision = ComponentManager.Get().EntityComponent<CollisionComponent>(spawnComponent.Key);

                    collision.Collided = false;

                    collision.BoundingRectangle =
                        new Rectangle((int)positionComponent.Position.X,
                            (int)positionComponent.Position.Y,
                            spriteComponent.Texture.Width,
                            spriteComponent.Texture.Height);

                    if (playerCollision.BoundingRectangle.Intersects(collision.BoundingRectangle))
                    {
                        collision.Collided = true;
                        CollisionOccured = true;
                    }
                }
            }
        }
    }
}
