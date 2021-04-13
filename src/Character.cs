using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    class Character
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool hasJumped;
        public Rectangle rectangle;

        // Hero animation
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public Rectangle animation;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 3, texture.Height);


            if (Keyboard.GetState().IsKeyDown(Keys.Right) && position.X < 1850)
            {
                velocity.X = 4f;
                texture = Content.Load<Texture2D>("textures\\heroRight");
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && position.X >= 0)
            {
                velocity.X = -4f;
                texture = Content.Load<Texture2D>("textures\\heroLeft");
            }

            else
            {
                velocity.X = 0f;
                texture = Content.Load<Texture2D>("textures\\heroStop");
            }

            float i = 1;
            velocity.Y += 0.15f * i;

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 2)
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
            animation = new Rectangle(72 * frames, 0, 72, 105);
        }
    }
}