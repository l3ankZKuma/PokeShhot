﻿// Optimized and refactored version
﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Graphics;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/GameObjects/Bubble.cs
namespace BubblePuzzle
{
    public class Bubble : GameObject
    {
        //Set speed and gun angle
        public float Speed;
        public float Angle;

        //Set sounds
        public SoundEffectInstance deadSFX, stickSFX, awesomeSFX, unbelieveSFX;
        private Random _rndSound = new Random();

        //Set interface
        public Bubble(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, Bubble[,] gameObjects)
        {
            //Bubble position
            if (isActive)
            {
                Velocity.X = (float)Math.Cos(Angle) * Speed;
                Velocity.Y = (float)Math.Sin(Angle) * Speed;
                Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                DetectCollision(gameObjects);
                if (Position.Y <= 40)
                {
                    isActive = false;
                    if (Position.X > 880)
                    {
                        gameObjects[0, 7] = this;
                        Position = new Vector2((7 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 800)
                    {
                        gameObjects[0, 6] = this;
                        Position = new Vector2((6 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 720)
                    {
                        gameObjects[0, 5] = this;
                        Position = new Vector2((5 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 640)
                    {
                        gameObjects[0, 4] = this;
                        Position = new Vector2((4 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 560)
                    {
                        gameObjects[0, 3] = this;
                        Position = new Vector2((3 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 480)
                    {
                        gameObjects[0, 2] = this;
                        Position = new Vector2((2 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 400)
                    {
                        gameObjects[0, 1] = this;
                        Position = new Vector2((1 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    else if (Position.X > 320)
                    {
                        gameObjects[0, 0] = this;
                        Position = new Vector2((0 * 80) + ((0 % 2) == 0 ? 320 : 360), (0 * 70) + 40);
                    }
                    Singleton.Instance.Shooting = false;

                    stickSFX.Volume = Singleton.Instance.sfxVolume;
                    stickSFX.Play();
                }

                if (Position.X <= 325)
                {
                    Angle = -Angle;
                    Angle += MathHelper.ToRadians(180);
                }

                if (Position.X + _texture.Width >= 960)
                {
                    Angle = -Angle;
                    Angle += MathHelper.ToRadians(180);
                }
            }
        }

        //Detect bubble collision
        private void DetectCollision(Bubble[,] gameObjects)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gameObjects[i, j] != null && !gameObjects[i, j].isActive)
                    {
                        if (CheckCollision(gameObjects[i, j]) <= 70)
                        {
                            if (Position.X >= gameObjects[i, j].Position.X)
                            {
                                if (i % 2 == 0)
                                {
                                    if (j == 7)
                                    {
                                        gameObjects[i + 1, j - 1] = this;
                                        gameObjects[i + 1, j - 1].Position = new Vector2(((j - 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                        CheckRemoveBubble(gameObjects, color, new Vector2(j - 1, i + 1));
                                    }
                                    else
                                    {
                                        gameObjects[i + 1, j] = this;
                                        gameObjects[i + 1, j].Position = new Vector2((j * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                        CheckRemoveBubble(gameObjects, color, new Vector2(j, i + 1));
                                    }
                                }
                                else
                                {
                                    gameObjects[i + 1, j + 1] = this;
                                    gameObjects[i + 1, j + 1].Position = new Vector2(((j + 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                    CheckRemoveBubble(gameObjects, color, new Vector2(j + 1, i + 1));
                                }
                            }
                            else
                            {
                                if (i % 2 == 0)
                                {
                                    gameObjects[i + 1, j - 1] = this;
                                    gameObjects[i + 1, j - 1].Position = new Vector2(((j - 1) * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                    CheckRemoveBubble(gameObjects, color, new Vector2(j - 1, i + 1));
                                }
                                else
                                {
                                    gameObjects[(i + 1), j] = this;
                                    gameObjects[(i + 1), j].Position = new Vector2((j * 80) + (((i + 1) % 2) == 0 ? 320 : 360), ((i + 1) * 70) + 40);
                                    CheckRemoveBubble(gameObjects, color, new Vector2(j, i + 1));
                                }
                            }
                            isActive = false;

                            //When the bubbles pop
                            //Main Mechanics = Bubble-popping when three or more bubbles with same colour connect
                            //Extra Mechanics = Gain score when bubble pop
                            if (Singleton.Instance.removeBubble.Count >= 3)
                            {
                                Singleton.Instance.Score += Singleton.Instance.removeBubble.Count * 100;
                                deadSFX.Volume = Singleton.Instance.sfxVolume;
                                awesomeSFX.Volume = Singleton.Instance.effVolume;
                                unbelieveSFX.Volume = Singleton.Instance.effVolume;
                                deadSFX.Play();

                                switch(_rndSound.Next(0,2))
                                {
                                    case 0:
                                        awesomeSFX.Play();
                                        break;                          
                                    case 1:
                                        unbelieveSFX.Play();
                                        break;
                                }
                                
                            }

                            //When shoot the bubbles and not pop 
                            //Extra Mechanics = Loss 100 score per one shoot
                            else if (Singleton.Instance.removeBubble.Count > 0)
                            {
                                Singleton.Instance.Score -= 100;
                                stickSFX.Volume = Singleton.Instance.sfxVolume;
                                stickSFX.Play();
                                foreach (Vector2 v in Singleton.Instance.removeBubble)
                                {
                                    gameObjects[(int)v.Y, (int)v.X] = new Bubble(_texture)
                                    {
                                        name = "Bubble",
                                        Position = new Vector2((v.X * 80) + ((v.Y % 2) == 0 ? 320 : 360), (v.Y * 70) + 40),
                                        color = color,
                                        isActive = false,
                                    };
                                }
                            }
                            Singleton.Instance.removeBubble.Clear();
                            Singleton.Instance.Shooting = false;
                            return;
                        }

                    }
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, color);
            base.Draw(spriteBatch);
        }

        //Check bubble collision
        public int CheckCollision(Bubble other)
        {
            return (int)Math.Sqrt(Math.Pow(Position.X - other.Position.X, 2) + Math.Pow(Position.Y - other.Position.Y, 2));
        }
        public void CheckRemoveBubble(Bubble[,] gameObjects, Color ColorTarget, Vector2 me)
        {
            if ((me.X >= 0 && me.Y >= 0) && (me.X <= 7 && me.Y <= 8) && (gameObjects[(int)me.Y, (int)me.X] != null && gameObjects[(int)me.Y, (int)me.X].color == ColorTarget))
            {
                Singleton.Instance.removeBubble.Add(me);
                gameObjects[(int)me.Y, (int)me.X] = null;
            }
            else
            {
                return;
            }
            //Right
            CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X + 1, me.Y));
            //Left
            CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X - 1, me.Y)); 
            if (me.Y % 2 == 0)
            {
                //Top Right
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X, me.Y - 1));
                //Top Left
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X - 1, me.Y - 1));
                //Bottom Right
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X, me.Y + 1));
                //Bottom Left
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X - 1, me.Y + 1)); 
            }
            else
            {
                //Top Right
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X + 1, me.Y - 1));
                //Top Left
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X, me.Y - 1));
                //Bottom Right
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X + 1, me.Y + 1));
                //Bottom Right 
                CheckRemoveBubble(gameObjects, ColorTarget, new Vector2(me.X, me.Y + 1)); 		
            }
        }
    }
}
