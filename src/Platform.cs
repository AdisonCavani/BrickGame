using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    class Platform
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle rectangle;

        public Platform(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            rectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}

static class RectangleHelper
{
    const int penetrationMargin = 6;
    public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return (r1.Bottom >= r2.Top - penetrationMargin &&
            r1.Bottom <= r2.Top + 5 &&
            r1.Right >= r2.Left + 25 && // Left
            r1.Left <= r2.Right - 20); // Right
    }
}