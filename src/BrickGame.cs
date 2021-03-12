using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BrickGame
{
    public class BrickGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D background1;
        Texture2D background1Color;
        Vector2 background1Pos;
        Vector2 background1ColorPos;

        Texture2D smallCloud1;
        Vector2 smallCloud1Pos;
        Texture2D smallCloud2;
        Vector2 smallCloud2Pos;
        Texture2D smallCloudMirror;
        Vector2 smallCloudMirrorPos;
        Texture2D bigCloud1;
        Vector2 bigCloud1Pos;
        Texture2D bigCloud2;
        Vector2 bigCloud2Pos;
        Texture2D bigCloud3;
        Vector2 bigCloud3Pos;

        Texture2D hero;
        Vector2 heroPos;
        Texture2D heroLeft;
        Texture2D heroRight;

        Texture2D brick1;
        Vector2 brick1Pos;
        Texture2D brick2;
        Vector2 brick2Pos;
        Texture2D brick3;
        Vector2 brick3Pos;

        Texture2D coin1;
        Vector2 coin1Pos;

        // Health
        Texture2D health;
        Vector2 healthPos;
        int life = 5;

        // Score variables
        SpriteFont font;
        private int score;
        private ScoreManager _scoreManager;

        // Animations
        Rectangle heroAnim;
        Rectangle coinAnim;
        Rectangle lifeAnim;

        // Hero animation
        float heroElapsed;
        float heroDelay = 200f;
        int heroFrames = 0;

        // Coin animation
        float coin1Elapsed;
        float coin1Delay = 100f;
        int coin1Frames = 0;

        // Brick animation
        Vector2 orgin = new Vector2(0, 0);
        float angle = 0;

        // Jump variables
        bool jump = false;
        float jumpTime = 0.0f;
        float startY = 0;

        // Creating object reference to get random speed and position value
        DrawBrickSpeed drawBrickSpeed = new DrawBrickSpeed();
        DrawBrickPosition drawBrickPosition = new DrawBrickPosition();

        DrawCoinSpeed drawCoinSpeed = new DrawCoinSpeed();
        DrawCoinPosition drawCoinPosition = new DrawCoinPosition();

        // Brick1 variables
        public int brick1Speed;
        public int brick1NewPos;
        public bool brick1NewLoop = true;

        // Brick2 variables
        public int brick2Speed;
        public int brick2NewPos;
        public bool brick2NewLoop = true;

        // Brick3 variables
        public int brick3Speed;
        public int brick3NewPos;
        public bool brick3NewLoop = true;

        // Coin1 variables
        public int coin1Speed;
        public int coin1NewPos;
        public bool coin1NewLoop = true;

        // Sounds
        SoundEffect effect;
        Song song;

        double fps;
        SpriteFont notoSansBold;

        // Resolution
        Matrix Scale;
        const int TargetWidth = 1024;
        const int TargetHeight = 576;

        public BrickGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = false; // Disable V-Sync
            IsFixedTimeStep = true; // Cap FPS to 60
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            float scaleX = _graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = _graphics.PreferredBackBufferHeight / TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content\\assets";

            // TODO: use this.Content to load your game content here

            _scoreManager = ScoreManager.Load();


            // Game resolution
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            // Background loading
            background1 = Content.Load<Texture2D>("textures\\background1_blue");
            smallCloudMirror = Content.Load<Texture2D>("textures\\smallCloudMirror");
            smallCloudMirrorPos.X = -880;
            smallCloudMirrorPos.Y = 190;

            bigCloud3 = Content.Load<Texture2D>("textures\\bigCloud");
            bigCloud3Pos.X = -575;
            bigCloud3Pos.Y = 160;

            background1Color = Content.Load<Texture2D>("textures\\background1");
            hero = Content.Load<Texture2D>("textures\\heroStop");

            smallCloud1 = Content.Load<Texture2D>("textures\\smallCloud");
            smallCloud1Pos.X = -100;
            smallCloud1Pos.Y = 100;

            smallCloud2 = Content.Load<Texture2D>("textures\\smallCloud");
            smallCloud2Pos.X = -625;
            smallCloud2Pos.Y = 85;

            bigCloud1 = Content.Load<Texture2D>("textures\\bigCloud");
            bigCloud1Pos.X = -300;
            bigCloud1Pos.Y = 280;

            bigCloud2 = Content.Load<Texture2D>("textures\\bigCloud");
            bigCloud2Pos.X = -790;
            bigCloud2Pos.Y = 200;

            // Hero starting position
            heroPos.X = 640;
            heroPos.Y = 479;
            startY = heroPos.Y;

            // Brick loading
            brick1 = Content.Load<Texture2D>("textures\\brick1");
            brick2 = Content.Load<Texture2D>("textures\\brick2");
            brick3 = Content.Load<Texture2D>("textures\\brick3");

            // Coin loading
            coin1 = Content.Load<Texture2D>("textures\\coin");

            // Font loading
            font = Content.Load<SpriteFont>("fonts\\Font");
            notoSansBold = Content.Load<SpriteFont>("fonts\\notoSansBold");

            // Health bar
            health = Content.Load<Texture2D>("textures\\health");
            healthPos.X = 20;
            healthPos.Y = 20;

            // Sound
            effect = Content.Load<SoundEffect>("sounds\\CoinSoundFX");
            song = Content.Load<Song>("sounds\\ObvilonSoundtrack");

            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            if (life <= 0)
            {
                // ScoreManager
                _scoreManager.Add(new Score()
                {
                    PlayerName = "Adison",
                    Value = score,
                }
                );

                ScoreManager.Save(_scoreManager);
                score = 0;
                life = 5;
                // End of "ScoreManager"
            }

            KeyboardState klawisz = Keyboard.GetState();

            if (klawisz.IsKeyDown(Keys.Left) || klawisz.IsKeyDown(Keys.A))
            {
                if (heroPos.X > 0)
                {
                    heroPos.X -= 5;
                    hero = Content.Load<Texture2D>("textures\\heroLeft");
                }
            }

            if (klawisz.IsKeyDown(Keys.Right) || klawisz.IsKeyDown(Keys.D))
            {
                if (heroPos.X < 1220)
                {
                    heroPos.X += 5;
                    hero = Content.Load<Texture2D>("textures\\heroRight");
                }
            }

            if (klawisz.IsKeyUp(Keys.Left) && klawisz.IsKeyUp(Keys.Right) && (klawisz.IsKeyUp(Keys.A) && klawisz.IsKeyUp(Keys.D)))
            {
                hero = Content.Load<Texture2D>(assetName: "textures\\heroStop");
            }

            if (jump)
            {
                heroPos.Y += jumpTime;
                jumpTime += 1;
                if (heroPos.Y >= startY)
                {
                    heroPos.Y = startY;
                    jump = false;
                }
            }

            else
            {
                if (klawisz.IsKeyDown(Keys.Up) || klawisz.IsKeyDown(Keys.Space) || klawisz.IsKeyDown(Keys.W))
                {
                    jump = true;
                    jumpTime = -10;
                }
            }

            // Falling objects movement
            // Brick1

            if (brick1NewLoop == true)
            {
                brick1Speed = drawBrickSpeed.Speed1();
                brick1NewPos = drawBrickPosition.Position1();
                brick1Pos.X = brick1NewPos;
                brick1Pos.Y = -100;
                brick1NewLoop = false;
            }

            brick1Pos.Y += brick1Speed;

            if (brick1Pos.Y > 780)
            {
                score++;
                brick1NewLoop = true;
            }

            // Brick2
            if (brick2NewLoop == true)
            {
                brick2Speed = drawBrickSpeed.Speed2();
                brick2NewPos = drawBrickPosition.Position2();
                brick2Pos.X = brick2NewPos;
                brick2Pos.Y = -100;
                brick2NewLoop = false;
            }

            brick2Pos.Y += brick2Speed;

            if (brick2Pos.Y > 780)
            {
                score++;
                brick2NewLoop = true;
            }

            // Brick3
            if (brick3NewLoop == true)
            {
                brick3Speed = drawBrickSpeed.Speed3();
                brick3NewPos = drawBrickPosition.Position3();
                brick3Pos.X = brick3NewPos;
                brick3Pos.Y = -100;
                brick3NewLoop = false;
            }

            brick3Pos.Y += brick3Speed;

            if (brick3Pos.Y > 780)
            {
                score++;
                brick3NewLoop = true;
            }


            // Coin1
            if (coin1NewLoop == true)
            {
                coin1Speed = drawCoinSpeed.Speed1();
                coin1NewPos = drawCoinPosition.Position1();
                coin1Pos.X = coin1NewPos;
                coin1Pos.Y = -500;
                coin1NewLoop = false;
            }

            coin1Pos.Y += coin1Speed;

            if (coin1Pos.Y > 720)
            {
                coin1NewLoop = true;
            }

            // End of "Falling objects movement"


            // Colision

            // Brick1
            if ((brick1Pos.Y + 44 > heroPos.Y) && (brick1Pos.X + 57 > heroPos.X) && (brick1Pos.X < heroPos.X + 48) && (heroPos.Y >= brick1Pos.Y))
            {
                brick1NewLoop = true;
                life -= 1;
            }

            // Brick 2
            if ((brick2Pos.Y + 44 > heroPos.Y) && (brick2Pos.X + 57 > heroPos.X) && (brick2Pos.X < heroPos.X + 48) && (heroPos.Y >= brick2Pos.Y))
            {
                brick2NewLoop = true;
                life -= 1;
            }

            // Brick3
            if ((brick3Pos.Y + 44 > heroPos.Y) && (brick3Pos.X + 57 > heroPos.X) && (brick3Pos.X < heroPos.X + 48) && (heroPos.Y >= brick3Pos.Y))
            {
                brick3NewLoop = true;
                life -= 1;
            }

            // Coin1
            if ((coin1Pos.Y + 47 > heroPos.Y) && (coin1Pos.X + 46 > heroPos.X) && (coin1Pos.X < heroPos.X + 46) && (heroPos.Y >= coin1Pos.Y))
            {
                coin1NewLoop = true;
                score += 10;
                effect.Play();
            }
            // End of "Colision"


            // Hero animation
            heroElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (heroElapsed >= heroDelay)
            {
                if (heroFrames >= 2)
                {
                    heroFrames = 0;
                }
                else
                {
                    {
                        heroFrames++;
                    }
                    heroElapsed = 0;
                }
            }
            heroAnim = new Rectangle(48 * heroFrames, 0, 48, 70);
            // End of "Hero animation"


            // Coin animation
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
            coinAnim = new Rectangle(46 * coin1Frames, 0, 46, 47);
            // End of "Coin animation"


            // Health animation
            lifeAnim = new Rectangle(0, 32 * life, 160, 32);

            // Brick rotation
            //angle += 0.01f;
            // End of "Brick rotation"

            // Cloud animation
            smallCloud1Pos.X += 1;
            if (smallCloud1Pos.X > 1280)
            {
                smallCloud1Pos.X = -120;
            }

            smallCloud2Pos.X += 1;
            if (smallCloud2Pos.X > 1280)
            {
                smallCloud2Pos.X = -120;
            }

            bigCloud1Pos.X += 1;
            if (bigCloud1Pos.X > 1280)
            {
                bigCloud1Pos.X = -120;
            }

            bigCloud2Pos.X += 1;
            if (bigCloud2Pos.X > 1280)
            {
                bigCloud2Pos.X = -120;
            }

            smallCloudMirrorPos.X += 1;
            if (smallCloudMirrorPos.X > 1280)
            {
                smallCloudMirrorPos.X = -120;
            }

            bigCloud3Pos.X += 1;
            if (bigCloud3Pos.X > 1280)
            {
                bigCloud3Pos.X = -120;
            }
            // End of "Cloud animation"

            fps = 1f / gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(background1, background1Pos, Color.White);
            _spriteBatch.Draw(smallCloudMirror, smallCloudMirrorPos, Color.White);
            _spriteBatch.Draw(bigCloud3, bigCloud3Pos, Color.White);
            _spriteBatch.Draw(background1Color, background1ColorPos, Color.White);
            _spriteBatch.Draw(smallCloud1, smallCloud1Pos, Color.White);
            _spriteBatch.Draw(smallCloud2, smallCloud2Pos, Color.White);
            _spriteBatch.Draw(bigCloud1, bigCloud1Pos, Color.White);
            _spriteBatch.Draw(bigCloud2, bigCloud2Pos, Color.White);
            _spriteBatch.Draw(hero, heroPos, heroAnim, Color.White);
            _spriteBatch.Draw(brick1, brick1Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
            _spriteBatch.Draw(brick2, brick2Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
            _spriteBatch.Draw(brick3, brick3Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
            _spriteBatch.Draw(coin1, coin1Pos, coinAnim, Color.White);
            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, 70), Color.Black);
            _spriteBatch.DrawString(notoSansBold, "FPS: " + fps.ToString("0"), new Vector2(1190, 10), Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
            _spriteBatch.DrawString(font, "Highscores:\n" + string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(20, 100), Color.Black);
            _spriteBatch.Draw(health, healthPos, lifeAnim, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}