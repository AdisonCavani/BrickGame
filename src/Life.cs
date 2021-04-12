using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    public class Life
    {
        private ScoreManager _scoreManager;
        Rectangle animation;
        int life = 4;

        Texture2D texture;
        Vector2 position;

        public Life(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public void LoadContent(ContentManager content)
        {
            _scoreManager = ScoreManager.Load();
        }

        public void Update(GameTime gameTime, Vector2 playerPosition, int score)
        {
            if (playerPosition.Y > 1080)
            {
                life = 0;
            }

            if (life <= 0)
            {
                //soundEffects[1].CreateInstance().Play();
                playerPosition.X = 50;
                playerPosition.Y = 580;

                // ScoreManager
                _scoreManager.Add(new Score()
                {
                    PlayerName = "Adrian",
                    Value = score,
                }
                );

                ScoreManager.Save(_scoreManager);
                score = 0;
                life = 5;
                // End of "ScoreManager"
            }
            animation = new Rectangle(0, 32 * life, 160, 32);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, position, animation, Color.White);
        }
    }
}