using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleMy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        public static Texture2D ParticleTextTexture;
        ParticleText particleText;

        KeyboardState lastKeyboardState, keyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load our particle text font.
            font = Content.Load<SpriteFont>("NewSpriteFont");
            ParticleTextTexture = Content.Load<Texture2D>("Text-Particle");

            var view = GraphicsDevice.Viewport;

            particleText = new ParticleText(GraphicsDevice, font, "Kezumie", ParticleTextTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || WasPressed(Keys.Escape))
                this.Exit();

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            // Spacebar resets the simulation
            if (WasPressed(Keys.Space))
                particleText.Reset();

            particleText.Update();

            base.Update(gameTime);
        }

        bool WasPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw the particles using additive blending. You can experiment with using different blend modes and
            // different particle textures.
            spriteBatch.Begin(0, BlendState.Additive);
            particleText.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Press spacebar to reset. Fo Nami By Victorem", Vector2.Zero, Color.White, 0f, Vector2.Zero, 0.5f, 0, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
