using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BrickGame.Draw;

namespace BrickGame
{
    class Brick1
    {
        Texture2D texture1;
        Vector2 position1;
        Rectangle rectangle1;

        bool brick1NewLoop = true;
        int brick1Speed;

        Random random = new Random();

        DrawBrickSpeed drawBrick1Speed = new DrawBrickSpeed();
        DrawBrickPosition drawBrick1Position = new DrawBrickPosition();

        public Brick1(Texture2D newTexture)
        {
            texture1 = newTexture;
        }

        public async Task UpdateAsync(GameTime gameTime, Vector2 playerPosition, Rectangle playerRectangle)
        {
            if (brick1NewLoop == true)
            {
                position1.Y = -100;
                position1.X = drawBrick1Position.Position1(random);
                brick1Speed = drawBrick1Speed.Speed1();
                brick1NewLoop = false;
            }

            if (position1.Y > 1080)
            {
                brick1NewLoop = true;
            }

            position1.Y += brick1Speed;

            // Collision
            rectangle1 = new Rectangle((int)position1.X, (int)position1.Y, texture1.Width, texture1.Height);

            if (rectangle1.Intersects(playerRectangle))
            {
                await Task.Delay(100);
                brick1NewLoop = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture1, position1, Color.White);
        }
    }

    class Brick2
    {
        Rectangle rectnagle2;
        Texture2D texture2;
        Vector2 position2;
        bool brick2NewLoop = true;

        int brick2Speed;

        Random random = new Random();

        DrawBrickSpeed drawBrick2Speed = new DrawBrickSpeed();
        DrawBrickPosition drawBrick2Position = new DrawBrickPosition();

        public Brick2(Texture2D newTexture)
        {
            texture2 = newTexture;
        }

        public async Task UpdateAsync(GameTime gameTime, Vector2 playerPosition, Rectangle playerRectangle)
        {
            if (brick2NewLoop == true)
            {
                position2.Y = -100;
                position2.X = drawBrick2Position.Position2(random);
                brick2Speed = drawBrick2Speed.Speed2();
                brick2NewLoop = false;
            }

            position2.Y += brick2Speed;

            if (position2.Y > 1080)
            {
                brick2NewLoop = true;
            }

            // Collision
            rectnagle2 = new Rectangle((int)position2.X, (int)position2.Y, texture2.Width, texture2.Height);

            if (rectnagle2.Intersects(playerRectangle))
            {
                await Task.Delay(100);
                brick2NewLoop = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position2, Color.White);
        }
    }

    class Brick3
    {
        Rectangle rectnagle3;
        Texture2D texture3;
        Vector2 position3;
        bool brick3NewLoop = true;

        int brick3Speed;

        Random random = new Random();

        DrawBrickSpeed drawBrick3Speed = new DrawBrickSpeed();
        DrawBrickPosition drawBrick3Position = new DrawBrickPosition();

        public Brick3(Texture2D newTexture)
        {
            texture3 = newTexture;

        }

        public async Task UpdateAsync(GameTime gameTime, Vector2 playerPosition, Rectangle playerRectangle)
        {
            if (brick3NewLoop == true)
            {
                position3.Y = -100;
                position3.X = drawBrick3Position.Position3(random);
                brick3Speed = drawBrick3Speed.Speed3();
                brick3NewLoop = false;
            }

            position3.Y += brick3Speed;

            if (position3.Y > 1080)
            {
                brick3NewLoop = true;
            }

            if (position3.Y > 1080)
            {
                brick3NewLoop = true;
            }

            // Collision
            rectnagle3 = new Rectangle((int)position3.X, (int)position3.Y, texture3.Width, texture3.Height);

            if (rectnagle3.Intersects(playerRectangle))
            {
                await Task.Delay(100);
                brick3NewLoop = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture3, position3, Color.White);
        }
    }

}