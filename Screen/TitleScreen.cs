using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/Screen/SplashScreen.cs
namespace BubblePuzzle
{
    class TitleScreen : GameScreen
    {
        //Set font
        private Vector2 fontSize;

        //Set update color alpha
        private Color _Color; 

        //Set Texture and SpriteFont
        private SpriteFont Arial;
        private Texture2D Logo , translation;

        //Set value of alpha in color for fade logo and text
        private int alpha;
        //Set order of index to display splash screen
        private int displayIndex;
        //Set elapsed time in game
        private float _timer;
        //Set update function when _timer is more than _timePerUpdate
        private float _timePerUpdate;
        //Set true will fade in / set false will fade out
        private bool isShow; 

        public TitleScreen()
        {
            isShow = true;
            _timePerUpdate = 0.05f;
            displayIndex = 0;
            alpha = 0;
            _Color = new Color(255, 255, 255, alpha);
        }
        public override void LoadContent()
        {
            base.LoadContent();
            Arial = content.Load<SpriteFont>("Arial");
            Logo = content.Load<Texture2D>("MenuScreen/Logo");
            translation = content.Load<Texture2D>("MenuScreen/translationScreen");
        }
        public override void UnloadContent() { base.UnloadContent(); }
        public override void Update(GameTime gameTime)
        {
            //Add elapsed time to _timer
            _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            if (_timer >= _timePerUpdate)
            {
                if (isShow)
                {
                    //fade in screen
                    alpha += 5;
                    //When fade in finish
                    if (alpha >= 200)
                    {
                        isShow = false;
                        //Screen Transition
                        if (displayIndex == 2) ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                    }
                }
                else
                {
                    //fade out screen
                    alpha -= 100;
                    //When fade out finish
                    if (alpha <= 0)
                    {
                        isShow = true;
                        //Change display index and set next display
                        displayIndex++;
                        if (displayIndex == 1)
                        {
                            _Color = Color.Black;
                            _timePerUpdate -= 0.015f;
                        }
                        else if (displayIndex == 2)
                        {
                            _timePerUpdate += 0.03f;
                            _Color = Color.SaddleBrown;
                        }
                    }
                }
                _timer -= _timePerUpdate;
                _Color.A = (byte)alpha;
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (displayIndex)
            {
                case 0:
                    spriteBatch.Draw(Logo, new Vector2((Singleton.Instance.UISize.X - Logo.Width) / 2, (Singleton.Instance.UISize.Y - Logo.Height) / 2), _Color);
                    break;
                case 1:
                    spriteBatch.Draw(translation, Vector2.Zero,Color.White);
                    break;
            }

        }
    }
}
