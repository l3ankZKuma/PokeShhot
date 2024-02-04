using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Reference to a similar code structure from an external source
// Code references : https://github.com/PePoDev/egypt-bubble/blob/master/Main.cs
namespace BubblePuzzle
{
    // Main class for the game, inheriting from MonoGame's Game class.
    public class Main : Game
    {
        // Graphics device manager for handling graphics configurations.
        private GraphicsDeviceManager _graphics;
        // SpriteBatch for rendering sprites.
        private SpriteBatch _spriteBatch;
        // Background music for the game.
        private Song bgm;

        // Constructor of the Main class.
        public Main()
        {
            // Initialize graphics device manager with this game instance.
            _graphics = new GraphicsDeviceManager(this);
            // Set preferred buffer width (window width).
            _graphics.PreferredBackBufferWidth= (int)Singleton.Instance.UISize.X;
            // Set preferred buffer height (window height).
            _graphics.PreferredBackBufferHeight = (int)Singleton.Instance.UISize.Y;
            // Disable vertical retrace synchronization.
            _graphics.SynchronizeWithVerticalRetrace = false;
            // Disable fixed time step.
            IsFixedTimeStep = false;
            // Make the mouse cursor visible in the game window.
            IsMouseVisible = true;
            // Set the root directory for the content.
            Content.RootDirectory = "Content";
            // Center the game window on the screen.
            Window.Position = new Point(
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (_graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (_graphics.PreferredBackBufferHeight / 2)
            );
            // Apply changes to the graphics device manager.
            _graphics.ApplyChanges();
        }

        // Method called during the game's initialization phase.
        protected override void Initialize()
        {
            // Load background music and set properties.
            bgm = Content.Load<Song>("Musics/pokemonMUSIC");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Singleton.Instance.bgmVolume;
            // Play the background music.
            MediaPlayer.Play(bgm);
            // Call base class Initialize to complete the process.
            base.Initialize();
        }

        // Method called when the game should load its content.
        protected override void LoadContent()
        {
            // Initialize SpriteBatch for rendering.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load content for the ScreenManager.
            ScreenManager.Instance.LoadContent(Content);
        }

        // Method called every frame to update the game state.
        protected override void Update(GameTime gameTime)
        {
            // Update the ScreenManager.
            ScreenManager.Instance.Update(gameTime);
            // Adjust the volume of the media player.
            MediaPlayer.Volume = Singleton.Instance.bgmVolume;

            // Get the current mouse state and log its position.
            MouseState state = Mouse.GetState();
            Console.WriteLine(state.Position);
            // Check if the exit command has been triggered.
            if (Singleton.Instance.cmdExit)
            {
                // Exit the game.
                Exit();
            }

            // Call base class Update to complete the process.
            base.Update(gameTime);
        }

        // Method called every frame to draw the game.
        protected override void Draw(GameTime gameTime)
        {
            // Clear the graphics device with white color.
            GraphicsDevice.Clear(Color.White);
            // Begin sprite batch rendering.
            _spriteBatch.Begin();

            // Draw the current screen via ScreenManager.
            ScreenManager.Instance.Draw(_spriteBatch);

            // End sprite batch rendering.
            _spriteBatch.End();

            // Call base class Draw to complete the process.
            base.Draw(gameTime);
        }
    }
}
