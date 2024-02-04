// Optimized and refactored version
﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/GameObjects/Gun.cs
namespace BubblePuzzle
{
    class Gun : GameObject
    {
        //Set gun
        private Random random = new Random();
        private Texture2D bubbleTexture,nextbubbleTexture;
        private Bubble BubbleOnGun,nextBubble;
        private Color _color;
        private float angle;
        
        private  KeyboardState _previousKeyState;
        
        
        //Load sprite sheet
        private PokeBall _pokeBall;

        //Set sounds
        public SoundEffectInstance _deadSFX, _stickSFX, _awesomeSFX, _unbelieveSFX;
        public Gun(Texture2D texture, Texture2D bubble, ContentManager content) : base(texture)
        {
            bubbleTexture = bubble;
            _color = GetRandomColor();
            _previousKeyState = Keyboard.GetState();
            // Use the passed-in spriteBatch instead of creating a new one
          //  _pokeBall = new PokeBall(content, spriteBatch);
        }

        

        public override void Update(GameTime gameTime, Bubble[,] gameObjects)
        {
            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();
            
            KeyboardState currentKeyState = Keyboard.GetState();
            
            if (currentKeyState.IsKeyDown(Keys.S) && _previousKeyState.IsKeyUp(Keys.S)) { // Assuming 'S' is the swap key
                SwapBubbles();
            }
            
            _previousKeyState = currentKeyState;
            
            if (Singleton.Instance.MouseCurrent.Y < 625)
            {
                angle = (float)Math.Atan2((Position.Y + _texture.Height / 2) - Singleton.Instance.MouseCurrent.Y, (Position.X + _texture.Width / 2) - Singleton.Instance.MouseCurrent.X);
                //Click shooting
                //Main Mechanics = "Shooting a bubble"
                if (!Singleton.Instance.Shooting && Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                {
                    BubbleOnGun = new Bubble(bubbleTexture)
                    {
                        name = "Bubble",
                        Position = new Vector2(Singleton.Instance.UISize.X / 2 - bubbleTexture.Width / 2, 700 - bubbleTexture.Height),
                        deadSFX = _deadSFX,
                        stickSFX = _stickSFX,
                        awesomeSFX = _awesomeSFX,
                        unbelieveSFX = _unbelieveSFX,
                        color = _color,
                        isActive = true,
                        Angle = angle + MathHelper.Pi,
                        Speed = 1000,
                    };
                    _color = GetRandomColor();
                    Singleton.Instance.Shooting = true;
                }
            }
            if (Singleton.Instance.Shooting)
                BubbleOnGun.Update(gameTime, gameObjects);
        }

        //Draw bubbles on gun
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (nextBubble != null && !Singleton.Instance.Shooting) {
                var nextBubblePosition = new Vector2(Position.X + 300, Position.Y); // Example position
                spriteBatch.Draw(bubbleTexture, nextBubblePosition, nextBubble.color);
            }

            
            spriteBatch.Draw(_texture, Position + new Vector2(50, 50), null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(50, 50), 1.5f, SpriteEffects.None, 0f);
            if (!Singleton.Instance.Shooting)
                spriteBatch.Draw(bubbleTexture, new Vector2(Singleton.Instance.UISize.X / 2 - bubbleTexture.Width / 2, 700 - bubbleTexture.Height), _color);
            else
                BubbleOnGun.Draw(spriteBatch);
        }

        //Random bubbles color
        public Color GetRandomColor()
        {
            Color _color = Color.Black;
            switch (random.Next(0, 6))
            {
                case 0:
                    _color = Color.White;
                    break;
                case 1:
                    _color = Color.Blue;
                    break;
                case 2:
                    _color = Color.Magenta;
                    break;
                case 3:
                    _color = Color.Red;
                    break;
                case 4:
                    _color = Color.Green;
                    break;
                case 5:
                    _color = Color.Orange;
                    break;
            }  
            return _color;
        }
        
        public void SwapBubbles() {
            try {
                var temp = BubbleOnGun;
                BubbleOnGun = nextBubble;
                nextBubble = temp;

                nextBubble.color = GetRandomColor();
                
            } catch (Exception ex) {
                // Log or print out the exception details
                Console.WriteLine(ex.Message);
            }
        }


        
        
        
    }
}
