using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Code references : https://github.com/PePoDev/egypt-bubble/blob/master/Managers/ScreenManager.cs
namespace BubblePuzzle
{
    public class ScreenManager
    {
        public ContentManager Content { private set; get; }

        //Create enum state
        public enum GameScreenName
        {
            MenuScreen,
            PlayScreen
        }
        private GameScreen CurrentGameScreen;

        public ScreenManager()
        {
            CurrentGameScreen = new TitleScreen();
        }
        public void LoadScreen(GameScreenName _ScreenName)
        {
            switch (_ScreenName)
            {
                case GameScreenName.MenuScreen:
                    CurrentGameScreen = new MenuScreen();
                    break;
                case GameScreenName.PlayScreen:
                    CurrentGameScreen = new PlayScreen();
                    break;
            }
            CurrentGameScreen.LoadContent();
        }
        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            CurrentGameScreen.LoadContent();
        }
        public void UnloadContent()
        {
            CurrentGameScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            
            CurrentGameScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentGameScreen.Draw(spriteBatch);
        }
        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }
    }
}
