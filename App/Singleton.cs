using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;


namespace BubblePuzzle
{
    class Singleton
    {
        // Defines the UI size which can be accessed globally.
        public Vector2 UISize = new Vector2(1280, 720);

        // Set volume levels for different audio components.
        public float bgmVolume = .25f; // Background music volume.
        public float sfxVolume = .5f;  // Sound effects volume.
        public float effVolume = .75f; // Other effects volume.

        // Game mechanics related variables.
        public bool Shooting = false; // Indicates if shooting is happening.
        public List<Vector2> removeBubble = new List<Vector2>(); // List to track bubbles to be removed.
        public bool cmdExit = false; // Command to exit the game.
        public string BestTime, BestScore; // Strings to store the best time and score.

        // Initial score for the game.
        public int Score = 0;

        // State of the mouse in the previous and current frame.
        public MouseState MousePrevious, MouseCurrent;
        
        
        //Sprite Font
        
        

        // Singleton pattern implementation.
        private static Singleton instance; // Private static instance.
        public static Singleton Instance // Public accessor for the instance.
        
        
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton(); // Instantiate if not already done.
                }
                return instance;
            }
        }
    }
}