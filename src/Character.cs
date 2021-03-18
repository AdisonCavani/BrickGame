using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    class Character
    {
        Texture2D texture;
        Vector2 position;
        public Vector2 velocity;
        public bool hasJumped;
        public Rectangle rectangle;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            hasJumped = true;
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 5f;
                texture = Content.Load<Texture2D>("textures\\heroRight");
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -5f;
                texture = Content.Load<Texture2D>("textures\\heroLeft");
            }

            else
            {
                velocity.X = 0f;
                texture = Content.Load<Texture2D>("textures\\heroStop");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 20f;
                velocity.Y = -5f;
                hasJumped = true;
            }

            float i = 1;
            velocity.Y += 0.15f * i;

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
