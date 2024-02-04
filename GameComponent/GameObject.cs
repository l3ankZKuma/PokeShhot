// Optimized and refactored version
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/GameObjects/_GameObject.cs
namespace BubblePuzzle
{
    public class GameObject
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public Color color;
        public Vector2 Velocity;
        public string name;
        public bool isActive;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public GameObject(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            isActive = true;
        }
        public virtual void Update(GameTime gameTime, Bubble[,] gameObjects)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}