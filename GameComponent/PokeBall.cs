using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BubblePuzzle
{
    public class PokeBall
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _spriteSheet;
        private int _frameWidth = 76;
        private int _frameHeight = 109;
        public Color Color { get; set; } = Color.White; // Default color is white, but it can be changed externally

        public PokeBall(ContentManager content, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            // Replace "yourSpriteSheet" with the asset name of your sprite sheet
            _spriteSheet = content.Load<Texture2D>("Assets/PokeBall");
        }

        public void Draw(int frame, Vector2 position)
        {
            int column = frame % 6;
            int row = frame / 6;
            Rectangle sourceRectangle = new Rectangle(column * _frameWidth, row * _frameHeight, _frameWidth, _frameHeight);

            _spriteBatch.Draw(_spriteSheet, position, sourceRectangle, this.Color); // Use the Color property here
        }
    }
}