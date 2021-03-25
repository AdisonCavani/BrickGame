using Microsoft.Xna.Framework;

namespace BrickGame
{
    public class Camera
    {
        public Vector2 position;
        public Matrix transform { get; private set; }
        public float delay { get; set; } = 3.0f;

        public Camera (Vector2 pos) { this.position = pos; }

        public void Update (Vector2 pos, GameTime gameTime)
        {
            position.X += ((pos.X - position.X) - BrickGame.ResolutionNativeWidth / 2) * delay;
            position.Y += ((pos.Y - position.Y) - BrickGame.ResolutionNativeHeight / 2) * delay;

            transform = Matrix.CreateTranslation((int)-position.X, position.Y, 0);
        }
    }
}
