using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    class FinishFlag
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;

        float elapsed;
        float delay = 80f;
        int frames = 0;
        public Rectangle animation;

        public FinishFlag(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 30, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 29)
                {
                    frames = 0;
                }
                else
                {
                    {
                        frames++;
                    }
                    elapsed = 0;
                }
            }
            animation = new Rectangle(120 * frames, 0, 120, 90);
        }
    }
}
