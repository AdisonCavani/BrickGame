using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BrickGame.Draw;
using System.Threading.Tasks;

namespace BrickGame
{
    class Coin1
    {
        Texture2D coin1Texture;
        Vector2 coin1Position;
        Rectangle coin1Rectangle;

        // Animation
        Rectangle coin1Animation;
        float coin1Elapsed;
        float coin1Delay = 100f;
        int coin1Frames = 0;

        bool coin1NewLoop = true;
        int coin1Speed;

        Random random = new Random();

        DrawCoinSpeed drawCoin1Speed = new DrawCoinSpeed();
        DrawCoinPosition drawCoin1Position = new DrawCoinPosition();

        int score;

        public Coin1(Texture2D newTexture)
        {
            coin1Texture = newTexture;

        }

        public async Task UpdateAsync(GameTime gameTime, Vector2 playerPosition, Rectangle playerRectangle)
        {
            if (coin1NewLoop == true)
            {
                coin1Position.Y = -500;
                coin1Position.X = drawCoin1Position.Position1(random);
                coin1Speed = drawCoin1Speed.Speed1();
                coin1NewLoop = false;
            }

            coin1Position.Y += coin1Speed;

            if (coin1Position.Y > 1080)
            {
                coin1NewLoop = true;
            }

            // Animation
            coin1Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (coin1Elapsed >= coin1Delay)
            {
                if (coin1Frames >= 9)
                {
                    coin1Frames = 0;
                }
                else
                {
                    {
                        coin1Frames++;
                    }
                    coin1Elapsed = 0;
                }
            }
            coin1Animation = new Rectangle(46 * coin1Frames, 0, 46, 47);

            // Collision
            coin1Rectangle = new Rectangle((int)coin1Position.X, (int)coin1Position.Y, coin1Texture.Width / 10, coin1Texture.Height);

            if (coin1Rectangle.Intersects(playerRectangle))
            {

                score++;
                await Task.Delay(100);
                coin1NewLoop = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(coin1Texture, coin1Position, coin1Animation, Color.White);
        }
    }
}
