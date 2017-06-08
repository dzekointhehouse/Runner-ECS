using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RunnerECS.Components;
using RunnerECS.Content;
using RunnerECS.Managers;
using RunnerECS.Systems;

namespace RunnerGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Song song;
        private double count = 5;
        private SpriteFont font;

        // TODO: EntityComponentSystems
        private RenderingSystem renderingSystem;
        private PhysicsSystem physicsSystem;
        private CollisionDetectionSystem collisionDetectionSystem;
        InputSystem inputSystem;
        SpawnSystem spawnSystem;
        TextSystem _textSystem;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            renderingSystem = new RenderingSystem();
            physicsSystem = new PhysicsSystem();
            collisionDetectionSystem = new CollisionDetectionSystem();
            inputSystem = new InputSystem();
            spawnSystem = new SpawnSystem();
            _textSystem = new TextSystem();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManager.Get().GameSceneViewport = graphics.GraphicsDevice.Viewport;

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            song = Content.Load<Song>("jaws");
            font = Content.Load<SpriteFont>("font");

            CreateEntities();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(song);
            }

            if (collisionDetectionSystem.CollisionOccured)
            {
                if (count <= 0)
                {
                    ResetGame();
                    count = 5;
                }
                count -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // TODO: Add your update logic here
                inputSystem.Update(gameTime);
                physicsSystem.Update(gameTime);
                spawnSystem.Update(gameTime);
                collisionDetectionSystem.Update(gameTime);
            }




            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, AssetManager.Get().GameSceneViewport.Bounds, Color.White);

            if (collisionDetectionSystem.CollisionOccured)
            {
                // GraphicsDevice.Clear(Color.Red);
                if (count >= 0)
                {
                    var message = "GAME OVER";
                    var height = AssetManager.Get().GameSceneViewport.Height;
                    var width = AssetManager.Get().GameSceneViewport.Width;

                    spriteBatch.DrawString(font, message, new Vector2((width - font.MeasureString(message).X) * 0.5f, (height - font.MeasureString(message).Y) * 0.5f), Color.White);
                }
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

            }

            spriteBatch.End();

            // TODO: Entity Component Systems
            renderingSystem.Draw(gameTime, spriteBatch);
            _textSystem.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }


        private void ResetGame()
        {
            ComponentManager.Get().ClearComponents();

            renderingSystem = new RenderingSystem();
            physicsSystem = new PhysicsSystem();
            collisionDetectionSystem = new CollisionDetectionSystem();
            inputSystem = new InputSystem();
            spawnSystem = new SpawnSystem();
            _textSystem = new TextSystem();
            CreateEntities();
        }

        private void CreateEntities()
        {
            var playerTexture = Content.Load<Texture2D>("runner");
            var blockTexture = Content.Load<Texture2D>("teo");
            spawnSystem.LoadContent(Content);

            var playerId = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new SpriteComponent() { Texture = playerTexture }, playerId);
            ComponentManager.Get().AddComponentToEntity(new PositionComponent() { Position = new Vector2(20, graphics.GraphicsDevice.Viewport.Height - playerTexture.Height) }, playerId);
            ComponentManager.Get().AddComponentToEntity(new InputComponent() { JumpKey = Keys.Up }, playerId);
            ComponentManager.Get().AddComponentToEntity(new MovementComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new CollisionComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new PlayerComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new ScoreComponent()
            {
                Font = font
            }, playerId);




            var block = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new SpriteComponent() { Texture = blockTexture }, block);
            ComponentManager.Get().AddComponentToEntity(new PositionComponent() { Position = new Vector2(AssetManager.Get().GameSceneViewport.Width + 50, AssetManager.Get().GameSceneViewport.Height - blockTexture.Height) }, block);
            ComponentManager.Get().AddComponentToEntity(new MovementComponent(), block);
            ComponentManager.Get().AddComponentToEntity(new CollisionComponent(), block);
            ComponentManager.Get().AddComponentToEntity(new SpawnComponent(), block);

        }
    }
}
