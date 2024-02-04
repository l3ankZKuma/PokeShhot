using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/Screen/PlayScreen.cs
namespace BubblePuzzle
{
    class PlayScreen : GameScreen
    {
        //Set Screen
        private Texture2D BG, Black, BubbleTexture, GunTexture;
        private SpriteFont Arial;
        private Bubble[,] bubble = new Bubble[9, 8];
        private Color _color;
        private Random random = new Random();
        private Gun gun;

        //Set font
        private Vector2 fontSize;

        //Set timer
        private float _timer = 0f;
        private float _timer2 = 0f;
        private float Timers = 0f;
        private float timerPerUpdate = 0.05f;
        private float tickPerUpdate = 30f;
        private int alpha = 255;

        //Set fade UI
        private bool fadeFinish = false;
        private bool gameOver = false;
        private bool gameWin = false;

        //Set sounds
        private SoundEffectInstance UnbelievableSFX, AwesomeSFX, BubbleSFX_stick, BubbleSFX_dead,GameOved;
        private SoundEffectInstance Click;
        
        

        //InitialState
        //Main Mechanics : Starting pattern of bubbles
        public void Initial()
        {
            _color = new Color(255, 255, 255, alpha);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    bubble[i, j] = new Bubble(BubbleTexture)
                    {
                        name = "Bubble",
                        Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40),
                        color = GetRandomColor(),
                        isActive = false,
                    };
                }
            }
            Click.Volume = Singleton.Instance.sfxVolume;
            BubbleSFX_stick.Volume = Singleton.Instance.sfxVolume;
            BubbleSFX_dead.Volume = Singleton.Instance.sfxVolume;
            UnbelievableSFX.Volume = Singleton.Instance.effVolume;
            AwesomeSFX.Volume = Singleton.Instance.effVolume;
            gun = new Gun(GunTexture, BubbleTexture, content)
            {
                name = "Gun",
                Position = new Vector2(Singleton.Instance.UISize.X / 2 - GunTexture.Width / 2, 700 - GunTexture.Height),
                color = Color.White,
                _deadSFX = BubbleSFX_dead,
                _stickSFX = BubbleSFX_stick,
                _awesomeSFX = AwesomeSFX,
                _unbelieveSFX = UnbelievableSFX,
                isActive = true,
            };
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

        //Winning Conditions
        public bool CheckWin(Bubble[,] bubble)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (bubble[i, j] != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            BG = content.Load<Texture2D>("PlayScreen/BG2");
            Black = content.Load<Texture2D>("Black");
            BubbleTexture = content.Load<Texture2D>("PlayScreen/bubble");
            GunTexture = content.Load<Texture2D>("PlayScreen/hand1");
            Arial = content.Load<SpriteFont>("Arial");
            UnbelievableSFX = content.Load<SoundEffect>("Musics/PlayScreen/dattebayo").CreateInstance();
            AwesomeSFX = content.Load<SoundEffect>("Musics/PlayScreen/awesome").CreateInstance();
            BubbleSFX_dead = content.Load<SoundEffect>("UI_SoundPack8_Error_v1").CreateInstance();
            BubbleSFX_stick = content.Load<SoundEffect>("UI_SoundPack11_Select_v14").CreateInstance();
            
            Click = content.Load<SoundEffect>("transition t07 two-step 007").CreateInstance();
            Initial();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        //Check GameOver and Winning Condition
        //Main Mechanics : Player wins when they clear all bubbles, player loses when a bubble reaches the boundary
        //Main Mechanics : “Ceiling dropping” when some time passes
        //Extra Mechanics : BestScore Record
        //Extra Mechanics : Play time Record
        public override void Update(GameTime gameTime)
        {
            if (!gameOver && !gameWin)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (bubble[i, j] != null)
                            bubble[i, j].Update(gameTime, bubble);
                    }
                }
                gun.Update(gameTime, bubble);
                Timers += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

                for (int i = 0; i < 8; i++)
                {
                    if (bubble[8, i] != null)
                    {
                        gameOver = true;
                        Singleton.Instance.BestScore = Singleton.Instance.Score.ToString();
                        Singleton.Instance.BestTime = Timers.ToString("F");
                    }
                }
                //Check ball flying
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 7 - (i % 2); j++)
                    {
                        if (i % 2 != 0)
                        {
                            if (bubble[i - 1, j] == null && bubble[i - 1, j + 1] == null)
                            {
                                bubble[i, j] = null;
                            }
                            if (bubble[i, 1] == null && bubble[i - 1, 0] == null && bubble[i - 1, 1] == null)
                            {
                                bubble[i, 0] = null;
                            }
                            if (bubble[i, 5] == null && bubble[i - 1, 7] == null && bubble[i - 1, 6] == null)
                            {
                                bubble[i, 6] = null;
                            }
                        }
                        else
                        {
                            if (bubble[i - 1, j - 1] == null && bubble[i - 1, j] == null)
                            {
                                bubble[i, j] = null;
                            }
                            if (bubble[i - 1, 0] == null && bubble[i, 1] == null)
                            {
                                bubble[i, 0] = null;
                            }
                            if (bubble[i - 1, 6] == null && bubble[i, 6] == null)
                            {
                                bubble[i, 7] = null;
                            }
                        }
                    }
                }

                _timer2 += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if (_timer2 >= tickPerUpdate)
                {
                    //Check game over before scroll
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            //Record BestScore and BestTime
                            if (bubble[i, j] != null)
                            {
                                gameOver = true;
                                Singleton.Instance.BestScore = Singleton.Instance.Score.ToString();
                                Singleton.Instance.BestTime = Timers.ToString("F");
                            }
                        }
                    }
                    //Scroll position 
                    for (int i = 5; i >= 0; i--)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            bubble[i + 2, j] = bubble[i, j];
                        }
                    }
                    //Draw new scroll position
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            if (bubble[i, j] != null)
                            {
                                bubble[i, j].Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40);
                            }
                        }
                    }
                    //Random ball after scroll
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            bubble[i, j] = new Bubble(BubbleTexture)
                            {
                                name = "Bubble",
                                Position = new Vector2((j * 80) + ((i % 2) == 0 ? 320 : 360), (i * 70) + 40),
                                color = GetRandomColor(),
                                isActive = false,
                            };
                        }
                    }

                    _timer2 -= tickPerUpdate;
                }

                gameWin = CheckWin(bubble);

            }
            else
            {
                Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
                Singleton.Instance.MouseCurrent = Mouse.GetState();
                if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                {
                    Singleton.Instance.Score = 0;
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                }
            }
            //fade out screen
            if (!fadeFinish)
            {
                _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if (_timer >= timerPerUpdate)
                {
                    alpha -= 5;
                    _timer -= timerPerUpdate;
                    if (alpha <= 5)
                    {
                        fadeFinish = true;
                    }
                    _color.A = (byte)alpha;
                }
            }



            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bubble[i, j] != null)
                        bubble[i, j].Draw(spriteBatch);
                }
            }
            gun.Draw(spriteBatch);

            spriteBatch.DrawString(Arial, "Score : " + Singleton.Instance.Score, new Vector2(1060, 260), Color.Black);
            spriteBatch.DrawString(Arial, "Time : " + Timers.ToString("F"), new Vector2(20, 260), Color.Black);
            spriteBatch.DrawString(Arial, "Next Wave in " + (tickPerUpdate - _timer2).ToString("F"), new Vector2(20, 210), Color.Black);

            if (gameOver)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("GameOver !");
                spriteBatch.DrawString(Arial, "GameOver !", Singleton.Instance.UISize / 2 - fontSize / 2, _color);
                
            }

            if (gameWin)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("Winner !");
                spriteBatch.DrawString(Arial, "Winner !", Singleton.Instance.UISize / 2 - fontSize / 2, _color);
            }

            //Draw fade out screen
            if (!fadeFinish)
            {
                spriteBatch.Draw(Black, Vector2.Zero, _color);
            }
        }
    }
}
