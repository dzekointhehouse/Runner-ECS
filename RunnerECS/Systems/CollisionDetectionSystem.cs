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
                var playerSprite = ComponentManager.Get().EntityComponent<SpriteComponent>(playerComponent.Key);
                var playerPosition = ComponentManager.Get().EntityComponent<PositionComponent>(playerComponent.Key);

                playerCollision.BoundingRectangle = 
                    new Rectangle((int) playerPosition.Position.X,
                                  (int) playerPosition.Position.Y,
                                  playerSprite.Texture.Width,
                                  playerSprite.Texture.Height);

                foreach (var spawnComponent in spawnComponents)
                {
                    var collision = ComponentManager.Get().EntityComponent<CollisionComponent>(spawnComponent.Key);
                    var boxSprite = ComponentManager.Get().EntityComponent<SpriteComponent>(spawnComponent.Key);
                    var boxPosition = ComponentManager.Get().EntityComponent<PositionComponent>(spawnComponent.Key);

                    playerCollision.Collided = false;
                    CollisionOccured = false;

                    collision.BoundingRectangle =
                        new Rectangle((int)boxPosition.Position.X,
                            (int)boxPosition.Position.Y,
                            boxSprite.Texture.Width,
                            boxSprite.Texture.Height);

                    if (playerCollision.BoundingRectangle.Intersects(collision.BoundingRectangle))
                    {
                        playerCollision.Collided = true;
                        CollisionOccured = true;
                    }
                }
            }
        }
    }
}
