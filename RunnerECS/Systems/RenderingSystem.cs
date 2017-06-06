using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerECS.Components;
using RunnerECS.Content;

namespace RunnerECS.Systems
{
    public class RenderingSystem
    {

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var spriteComponents = ComponentManager.Get().GetComponents<SpriteComponent>();


            spriteBatch.Begin();

            foreach (var spriteComponent in spriteComponents)
            {
                var position = ComponentManager.Get().EntityComponent<PositionComponent>(spriteComponent.Key);
                var sprite = spriteComponent.Value as SpriteComponent;
                if (position == null || sprite == null) return;
                spriteBatch.Draw(sprite.Texture, position.Position, Color.White);

            }
            spriteBatch.End();
        }
    }
}
