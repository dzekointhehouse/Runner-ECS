using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RunnerECS.Components;
using RunnerECS.Content;
using RunnerECS.Managers;

namespace RunnerECS.Systems
{
    public class SpawnSystem
    {
        private Random random = new Random();
        //private Texture2D blockTexture;
        public void LoadContent(ContentManager contentManager)
        {
           // blockTexture = contentManager.Load<Texture2D>("teo");
        }

        public void Update(GameTime gameTime)
        {
            var spawnComponents = ComponentManager.Get().GetComponents<SpawnComponent>();

            foreach (var spawnComponent in spawnComponents)
            {
                var spawn = spawnComponent.Value as SpawnComponent;

                //if (random.NextDouble() < spawn.SpawnProbability)
                //{
                //    //var block = ComponentManager.Get().NewEntity();

                //    //ComponentManager.Get().AddComponentToEntity(new SpriteComponent() { Texture = blockTexture }, block);
                //    //ComponentManager.Get().AddComponentToEntity(new PositionComponent() { Position = new Vector2(AssetManager.Get().GameSceneViewport.Width + 50, AssetManager.Get().GameSceneViewport.Height - blockTexture.Height) }, block);
                //    //ComponentManager.Get().AddComponentToEntity(new MovementComponent(), block);
                //}

                var position = ComponentManager.Get().EntityComponent<PositionComponent>(spawnComponent.Key);
                var sprite = ComponentManager.Get().EntityComponent<SpriteComponent>(spawnComponent.Key);

                if (position != null && position.Position.X + sprite.Texture.Width <
                    AssetManager.Get().GameSceneViewport.X)
                {
                    position.Position = new Vector2(AssetManager.Get().GameSceneViewport.Width + 50,
                        AssetManager.Get().GameSceneViewport.Height - sprite.Texture.Height);
                }
            }
        }
    }
}
