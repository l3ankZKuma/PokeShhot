using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/Screen/MenuScreen.cs
namespace BubblePuzzle
{
    //Extra Mechanics : Watch "About(Credits)" Menu and "Ranking" Menu
    class MenuScreen : GameScreen
    {
        //Set MenuPage
        private Color _Color = new Color(250, 250, 250, 0);
        private Texture2D BG, Black;
        private Texture2D StartH, AboutH, RankingH, ExitH, back;
        private SpriteFont Arial;
        private Vector2 fontSize;

        //Set Sound
        private SoundEffectInstance ClickUISFX, translateMenuToPlay, SelectUISFX;

        //Set MainScene
        private bool isFinishFade = false;
        private bool isShowRanking = false, showAbout = false;
        private bool isMainScreen = true;

        //Mouse Hover
        private bool mhStart = false, mhAbout = false, mhRanking = false, mhExit = false, mhBack = false;
        private bool mhsStart = false, mhsAbout = false, mhsRanking = false, mhsExit = false;

        //Set timer function
        private float _timer = 0.0f;
        private float timerPerUpdate = 0.03f;
        private int alpha = 255;

        public void Initial()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //Texture2D
            BG = content.Load<Texture2D>("MenuScreen/BG");
            Black = content.Load<Texture2D>("Black");
            StartH = content.Load<Texture2D>("MenuScreen/startButton");
            AboutH = content.Load<Texture2D>("MenuScreen/optionButton");
            RankingH = content.Load<Texture2D>("MenuScreen/scoresButton");
            ExitH = content.Load<Texture2D>("MenuScreen/exitButton");
            back = content.Load<Texture2D>("MenuScreen/backButton");
            
            //Fonts
            Arial = content.Load<SpriteFont>("Arial");
            //Sounds
            ClickUISFX = content.Load<SoundEffect>("UI_SoundPack8_Error_v1").CreateInstance();
            translateMenuToPlay = content.Load<SoundEffect>("Musics/MenuScreen/translationScreen").CreateInstance();
            SelectUISFX = content.Load<SoundEffect>("UI_SoundPack11_Select_v14").CreateInstance();
            //Call Init
            Initial();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            SelectUISFX.Volume = Singleton.Instance.sfxVolume;
            ClickUISFX.Volume = Singleton.Instance.sfxVolume;
            translateMenuToPlay.Volume = Singleton.Instance.sfxVolume;

            Singleton.Instance.MousePrevious = Singleton.Instance.MouseCurrent;
            Singleton.Instance.MouseCurrent = Mouse.GetState();
            if (isMainScreen)
            {
                //Click start game
                if ((Singleton.Instance.MouseCurrent.X > 516 && Singleton.Instance.MouseCurrent.Y > 247) && (Singleton.Instance.MouseCurrent.X < 798 && Singleton.Instance.MouseCurrent.Y < 267))
                {
                    mhStart = true;
                    if (!mhsStart)
                    {
                        SelectUISFX.Play();
                        mhsStart = true;
                    }
                    //When left mouse click , the game will go to PlayScreen enum state
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        translateMenuToPlay.Play();
                        ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.PlayScreen);
                    }
                }
                else
                {
                    mhStart = false;
                    mhsStart = false;
                }

                //Click About
                if ((Singleton.Instance.MouseCurrent.X > 1044 && Singleton.Instance.MouseCurrent.Y > 178) && (Singleton.Instance.MouseCurrent.X < 1218 && Singleton.Instance.MouseCurrent.Y < 694))
                {
                    mhAbout = true;
                    if (!mhsAbout)
                    {
                        SelectUISFX.Play();
                        mhsAbout = true;
                    }
                    //When left mouse click , the game will go to About menu
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        showAbout = true;
                        isMainScreen = false;
                        ClickUISFX.Play();
                    }
                }
                else
                {
                    mhsAbout = false;
                    mhAbout = false;
                }

                //Click Ranking
                if ((Singleton.Instance.MouseCurrent.X > 514 && Singleton.Instance.MouseCurrent.Y > 335) && (Singleton.Instance.MouseCurrent.X < 803 && Singleton.Instance.MouseCurrent.Y < 360))
                {
                    mhRanking = true;
                    if (!mhsRanking)
                    {
                        SelectUISFX.Play();
                        mhsRanking = true;
                    }
                    //When left mouse click , the game will go to Ranking menu
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        isShowRanking = true;
                        isMainScreen = false;
                        ClickUISFX.Play();
                    }
                }
                else
                {
                    mhRanking = false;
                    mhsRanking = false;
                }

                //Click Exit
                if ((Singleton.Instance.MouseCurrent.X > 513 && Singleton.Instance.MouseCurrent.Y > 387) && (Singleton.Instance.MouseCurrent.X < 803 && Singleton.Instance.MouseCurrent.Y < 412))
                {
                    mhExit = true;
                    if (!mhsExit)
                    {
                        SelectUISFX.Play();
                        mhsExit = true;
                    }
                    //When left mouse click , the game exits
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        translateMenuToPlay.Play();
                        Singleton.Instance.cmdExit = true;
                    }
                }
                else
                {
                    mhExit = false;
                    mhsExit = false;
                }
            }
            else
            {
                //Click Back
                if ((Singleton.Instance.MouseCurrent.X > (1230 - back.Width) && Singleton.Instance.MouseCurrent.Y > 50) && (Singleton.Instance.MouseCurrent.X < 1230 && Singleton.Instance.MouseCurrent.Y < (50 + back.Height)))
                {
                    mhBack = true;
                    //When left mouse click , the game wii back to the MainMenu
                    if (Singleton.Instance.MouseCurrent.LeftButton == ButtonState.Pressed && Singleton.Instance.MousePrevious.LeftButton == ButtonState.Released)
                    {
                        isMainScreen = true;
                        showAbout = false;
                        isShowRanking = false;
                        ClickUISFX.Play();
                    }
                }
                else
                {
                    mhBack = false;
                }
                
            }

            //fade out screen
            if (!isFinishFade)
            {
                _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                if (_timer >= timerPerUpdate)
                {
                    alpha -= 5;
                    _timer -= timerPerUpdate;
                    if (alpha <= 5)
                    {
                        isFinishFade = true;
                    }
                    _Color.A = (byte)alpha;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BG, Vector2.Zero, Color.White);
            //Draw Mouse Hover 
            if (mhAbout)
            {
                spriteBatch.Draw(AboutH, Vector2.Zero, Color.White);
            }
            if (mhExit)
            {
                spriteBatch.Draw(ExitH, Vector2.Zero, Color.White);
            }
            if (mhRanking)
            {
                spriteBatch.Draw(RankingH, Vector2.Zero, Color.White);
            }
            if (mhStart)
            {
                spriteBatch.Draw(StartH, Vector2.Zero, Color.White);
            }

            //Draw UI when is NOT MainMenu
            if (!isMainScreen)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                if (mhBack)
                {
                    spriteBatch.Draw(back, new Vector2(1230 - back.Width, 50), Color.OrangeRed);
                }
                else
                {
                    spriteBatch.Draw(back, new Vector2(1230 - back.Width, 50), Color.White);
                }

                //Draw Leaderboard Screen
                if (isShowRanking)
                {
                    fontSize = Arial.MeasureString("Ranking");
                    spriteBatch.DrawString(Arial, "Ranking", new Vector2(Singleton.Instance.UISize.X / 2 - fontSize.X / 2, 125), Color.White);

                    //Works when GameOver
                    if (Singleton.Instance.BestTime != null)
                    {
                        fontSize = Arial.MeasureString("Best Time : " + Singleton.Instance.BestTime);
                        spriteBatch.DrawString(Arial, "Best Time : " + Singleton.Instance.BestTime, new Vector2(Singleton.Instance.UISize.X / 2 - fontSize.X / 2, 350), Color.White);

                        fontSize = Arial.MeasureString("Best Score : " + Singleton.Instance.BestScore);
                        spriteBatch.DrawString(Arial, "Best Score : " + Singleton.Instance.BestScore, new Vector2(Singleton.Instance.UISize.X / 2 - fontSize.X / 2, 425), Color.White);
                    }
                    else
                    {
                        fontSize = Arial.MeasureString("No Infomation");
                        spriteBatch.DrawString(Arial, "No Infomation", new Vector2(Singleton.Instance.UISize.X / 2 - fontSize.X / 2, 350), Color.White);
                    }
                }
            }

            //Draw fade out screen
            if (!isFinishFade)
            {
                spriteBatch.Draw(Black, Vector2.Zero, _Color);
            }
        }
    }
}
