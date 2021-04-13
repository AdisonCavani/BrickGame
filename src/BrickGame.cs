﻿using System;
using System.Linq;
using BrickGame.Draw;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace BrickGame
{
    public class BrickGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Game states
        int gameState;
        int previousGameState;

        Character player;
        FinishFlag finishFlag;

        List<Platform> platforms = new List<Platform>();

        // Sounds
        List<SoundEffect> soundEffects;
        Song music;

        Texture2D splashScreen;
        Texture2D winnerScreen;
        Texture2D loserScreen;

        SpriteFont stratum2bold45;
        SpriteFont stratum2bold95;

        Texture2D background1;
        Vector2 background1Pos;
        Texture2D background1Color;
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

        Texture2D brick1;
        Vector2 brick1Pos;
        Rectangle brick1Rect;
        Texture2D brick2;
        Vector2 brick2Pos;
        Rectangle brick2Rect;
        Texture2D brick3;
        Vector2 brick3Pos;
        Rectangle brick3Rect;

        Texture2D coin1;
        Vector2 coin1Pos;
        Rectangle coin1Rect;

        // Health
        Texture2D health;
        Vector2 healthPos;
        int life = 5;

        // Score variables
        SpriteFont font;
        int score;
        int lastScore;
        private ScoreManager _scoreManager;

        // Animations
        Rectangle coinAnim;
        Rectangle lifeAnim;

        // Coin animation
        float coin1Elapsed;
        float coin1Delay = 100f;
        int coin1Frames = 0;

        // Brick animation
        Vector2 orgin = new Vector2(0, 0);
        float angle = 0;

        float angle1 = 0;
        Vector2 orgin1 = new Vector2(0, 0);

        // Creating object reference to get random speed and position value
        Random random = new Random();

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

        float fps;
        float frametime;
        bool showFPS;
        SpriteFont notoSansBold;

        // Resolution
        public const int ResolutionNativeWidth = 1920; // Native resolution
        public const int ResolutionNativeHeight = 1080; // Native resolution
        public float ResolutionTargetWidth;
        public float ResolutionTargetHeight;
        public float scaleX;
        public float scaleY;
        Matrix ResolutionScale;

        public BrickGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content\\assets";
            soundEffects = new List<SoundEffect>();

            _graphics.SynchronizeWithVerticalRetrace = false; // Disable V-Sync
            IsFixedTimeStep = true; // Cap FPS to 60
            IsMouseVisible = true;
            showFPS = false; // Show FPS

            Window.Title = "BrickGame";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //_graphics.PreferMultiSampling = true;
            //GraphicsDevice.PresentationParameters.MultiSampleCount = 8;

            // TODO: use this.Content to load your game content here

            splashScreen = Content.Load<Texture2D>("screens\\splash");
            winnerScreen = Content.Load<Texture2D>("screens\\winner");
            loserScreen = Content.Load<Texture2D>("screens\\gameOver");

            stratum2bold45 = Content.Load<SpriteFont>("fonts\\Stratum2Bold45");
            stratum2bold95 = Content.Load<SpriteFont>("fonts\\Stratum2Bold95");

            player = new Character(Content.Load<Texture2D>("textures\\heroStop"), new Vector2(50, 720));

            finishFlag = new FinishFlag(Content.Load<Texture2D>("textures\\finishFlag"), new Vector2(460, 30));

            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform_background"), new Vector2(0, 827)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(175, 720)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(425, 630)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(710, 530)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(930, 422)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1180, 695)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1420, 590)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1675, 700)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1800, 590)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1715, 480)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1800, 370)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1715, 260)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1800, 150)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1575, 125)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1300, 350)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(1050, 240)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(720, 240)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(500, 350)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(300, 450)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(120, 520)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(30, 410)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(120, 300)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(220, 190)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(450, 120)));

            // Sounds
            soundEffects.Add(Content.Load<SoundEffect>("sounds\\CoinSoundFX"));
            soundEffects.Add(Content.Load<SoundEffect>("sounds\\GameOverSoundFX"));
            soundEffects.Add(Content.Load<SoundEffect>("sounds\\LostLifeSoundFX"));

            music = Content.Load<Song>("sounds\\LegoMusic");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            // Game resolution
            ResolutionTargetWidth = GraphicsDevice.DisplayMode.Width;
            ResolutionTargetHeight = GraphicsDevice.DisplayMode.Height;

            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width; // Target resolution
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height; // Target resolution

            scaleX = (float)_graphics.PreferredBackBufferWidth / ResolutionNativeWidth;
            scaleY = (float)_graphics.PreferredBackBufferHeight / ResolutionNativeHeight;
            ResolutionScale = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            _graphics.IsFullScreen = true; // Enable / disable fullscreen
            _graphics.HardwareModeSwitch = false; // Fix fullscreen flickering
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

            // ScoreManger
            _scoreManager = ScoreManager.Load();

        }

        protected override async void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            if (gameState == 0 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                previousGameState = 0;
                gameState = 1;
            }

            if (gameState == 2 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                previousGameState = 2;
                gameState = 1;
            }

            if (gameState == 3 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                previousGameState = 3;
                gameState = 1;
            }

            if (previousGameState == 2 || previousGameState == 3) // Reset positions on game restart
            {
                player.position.X = 50;
                player.position.Y = 720;

                brick1NewLoop = true;
                brick2NewLoop = true;
                brick3NewLoop = true;

                coin1NewLoop = true;

                smallCloud1Pos.X = -100;
                smallCloud2Pos.X = -625;

                bigCloud1Pos.X = -300;
                bigCloud2Pos.X = -790;
                bigCloud3Pos.X = -575;

                smallCloudMirrorPos.X = -880;

                previousGameState = 0;

            }
            // TODO: Add your update logic here

            if (gameState == 1)
            {
                player.Update(gameTime, Content);
                finishFlag.Update(gameTime);



                foreach (Platform platform in platforms)
                {
                    if (finishFlag.rectangle.Intersects(player.rectangle) && player.rectangle.isOnTopOf(platform.rectangle))
                    {
                        // ScoreManager
                        _scoreManager.Add(new Score()
                        {
                            PlayerName = "Adrian",
                            Value = score,
                        }
                        );

                        ScoreManager.Save(_scoreManager);
                        lastScore = score;
                        score = 0;
                        life = 5;
                        // End of "ScoreManager"

                        gameState = 2;
                    }

                    if (player.rectangle.isOnTopOf(platform.rectangle))
                    {
                        player.velocity.Y = 0f;
                        player.hasJumped = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && player.hasJumped == false && player.rectangle.isOnTopOf(platform.rectangle))
                    {
                        player.position.Y -= 18f;
                        player.velocity.Y = -5f;
                        player.hasJumped = true;
                    }
                }

                if (player.position.Y > 1080 + player.texture.Height)
                {
                    life = 0;
                }

                if (life <= 0)
                {
                    soundEffects[1].CreateInstance().Play();

                    // ScoreManager
                    _scoreManager.Add(new Score()
                    {
                        PlayerName = "Adrian",
                        Value = score,
                    }
                    );

                    ScoreManager.Save(_scoreManager);
                    lastScore = score;
                    score = 0;
                    life = 5;
                    // End of "ScoreManager"

                    gameState = 3;
                }


                // Falling objects movement
                // Brick1
                if (brick1NewLoop == true)
                {
                    brick1Speed = drawBrickSpeed.Speed1();
                    brick1Pos.X = drawBrickPosition.Position1(random);
                    brick1Pos.Y = -100;
                    brick1NewLoop = false;
                }

                brick1Rect = new Rectangle((int)brick1Pos.X, (int)brick1Pos.Y, brick1.Width, brick1.Height);
                brick1Pos.Y += brick1Speed;

                if (brick1Pos.Y > ResolutionNativeHeight)
                {
                    score++;
                    brick1NewLoop = true;
                }

                if (brick1Rect.Intersects(player.rectangle))
                {
                    life -= 1;
                    brick1NewLoop = true;
                    if (life >= 1)
                    {
                        soundEffects[2].CreateInstance().Play();
                    }
                }

                // Brick2
                if (brick2NewLoop == true)
                {
                    brick2Speed = drawBrickSpeed.Speed2();
                    brick2Pos.X = drawBrickPosition.Position2(random);
                    brick2Pos.Y = -100;
                    brick2NewLoop = false;
                }

                brick2Rect = new Rectangle((int)brick2Pos.X, (int)brick2Pos.Y, brick2.Width, brick2.Height);
                brick2Pos.Y += brick2Speed;

                if (brick2Pos.Y > ResolutionNativeHeight)
                {
                    score++;
                    brick2NewLoop = true;
                }

                if (brick2Rect.Intersects(player.rectangle))
                {
                    life -= 1;
                    brick2NewLoop = true;
                    if (life >= 1)
                    {
                        soundEffects[2].CreateInstance().Play();
                    }
                }

                // Brick3
                if (brick3NewLoop == true)
                {
                    brick3Speed = drawBrickSpeed.Speed3();
                    brick3Pos.X = drawBrickPosition.Position3(random);
                    brick3Pos.Y = -100;
                    brick3NewLoop = false;
                }

                brick3Rect = new Rectangle((int)brick3Pos.X, (int)brick3Pos.Y, brick3.Width, brick3.Height);
                brick3Pos.Y += brick3Speed;

                if (brick3Pos.Y > ResolutionNativeHeight)
                {
                    score++;
                    brick3NewLoop = true;
                }

                if (brick3Rect.Intersects(player.rectangle))
                {
                    life -= 1;
                    brick3NewLoop = true;
                    if (life >= 1)
                    {
                        soundEffects[2].CreateInstance().Play();
                    }
                }

                // Coin1
                if (coin1NewLoop == true)
                {
                    coin1Speed = drawCoinSpeed.Speed1();
                    coin1Pos.X = drawCoinPosition.Position1(random);
                    coin1Pos.Y = -500;
                    coin1NewLoop = false;
                }

                coin1Rect = new Rectangle((int)coin1Pos.X, (int)coin1Pos.Y, coin1.Width / 10, coin1.Height);
                coin1Pos.Y += coin1Speed;

                if (coin1Pos.Y > ResolutionNativeHeight)
                {
                    coin1NewLoop = true;
                }

                if (coin1Rect.Intersects(player.rectangle))
                {
                    score += 10;
                    coin1NewLoop = true;
                    if (life >= 1)
                    {
                        soundEffects[0].CreateInstance().Play();
                    }
                }
                // End of "Colision"

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
                if (smallCloud1Pos.X > ResolutionNativeWidth)
                {
                    smallCloud1Pos.X = -180;
                }

                smallCloud2Pos.X += 1;
                if (smallCloud2Pos.X > ResolutionNativeWidth)
                {
                    smallCloud2Pos.X = -180;
                }

                bigCloud1Pos.X += 1;
                if (bigCloud1Pos.X > ResolutionNativeWidth)
                {
                    bigCloud1Pos.X = -180;
                }

                bigCloud2Pos.X += 1;
                if (bigCloud2Pos.X > ResolutionNativeWidth)
                {
                    bigCloud2Pos.X = -180;
                }

                smallCloudMirrorPos.X += 1;
                if (smallCloudMirrorPos.X > ResolutionNativeWidth)
                {
                    smallCloudMirrorPos.X = -180;
                }

                bigCloud3Pos.X += 1;
                if (bigCloud3Pos.X > ResolutionNativeWidth)
                {
                    bigCloud3Pos.X = -180;
                }
                // End of "Cloud animation"
            }

            frametime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            fps = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gameState == 0)
            {
                _spriteBatch.Begin(transformMatrix: ResolutionScale);
                _spriteBatch.Draw(splashScreen, new Vector2(0, 0), null, Color.White);
                _spriteBatch.DrawString(stratum2bold45, string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + " - " + c.Value).ToArray()), new Vector2(885, 590), Color.White);
                _spriteBatch.End();
            }

            if (gameState == 1)
            {
                _spriteBatch.Begin(transformMatrix: ResolutionScale);
                _spriteBatch.Draw(background1, background1Pos, Color.White);
                _spriteBatch.Draw(smallCloudMirror, smallCloudMirrorPos, Color.White);
                _spriteBatch.Draw(bigCloud3, bigCloud3Pos, Color.White);
                _spriteBatch.Draw(background1Color, background1ColorPos, Color.White);
                _spriteBatch.Draw(smallCloud1, smallCloud1Pos, Color.White);
                _spriteBatch.Draw(smallCloud2, smallCloud2Pos, Color.White);
                _spriteBatch.Draw(bigCloud1, bigCloud1Pos, Color.White);
                _spriteBatch.Draw(bigCloud2, bigCloud2Pos, Color.White);

                foreach (Platform platform in platforms)
                {
                    platform.Draw(_spriteBatch);
                }

                _spriteBatch.Draw(finishFlag.texture, finishFlag.position, finishFlag.animation, Color.White);
                _spriteBatch.Draw(brick1, brick1Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
                _spriteBatch.Draw(brick2, brick2Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
                _spriteBatch.Draw(brick3, brick3Pos, null, Color.White, angle, orgin, 1.0f, SpriteEffects.None, 1);
                _spriteBatch.Draw(coin1, coin1Pos, coinAnim, Color.White);
                _spriteBatch.Draw(player.texture, player.position, player.animation, Color.White);
                _spriteBatch.Draw(health, healthPos, lifeAnim, Color.White);
                //_spriteBatch.DrawString(stratum2bold45, "Score: " + lastScore, new Vector2(20, 70), Color.Black);
                //_spriteBatch.DrawString(stratum2bold45, "Highscores:\n" + string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(20, 200), Color.Black);

                if (showFPS == true)
                {
                    _spriteBatch.DrawString(notoSansBold, "FPS: " + fps.ToString("0"), new Vector2((ResolutionNativeWidth - 85), 10), Color.White, angle1, orgin1, 1.0f, SpriteEffects.None, 1);
                    _spriteBatch.DrawString(notoSansBold, frametime.ToString("0.0") + " ms", new Vector2((ResolutionNativeWidth - 85), 30), Color.White, angle1, orgin1, 1.0f, SpriteEffects.None, 1);
                }
                _spriteBatch.End();
            }

            if (gameState == 2)
            {
                _spriteBatch.Begin(transformMatrix: ResolutionScale);
                _spriteBatch.Draw(winnerScreen, new Vector2(0, 0), null, Color.White);
                _spriteBatch.DrawString(stratum2bold95, lastScore.ToString(), new Vector2(1130, 285), Color.White);
                _spriteBatch.DrawString(stratum2bold45, string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + " - " + c.Value).ToArray()), new Vector2(885, 655), Color.White);
                _spriteBatch.End();
            }

            if (gameState == 3)
            {
                _spriteBatch.Begin(transformMatrix: ResolutionScale);
                _spriteBatch.Draw(loserScreen, new Vector2(0, 0), null, Color.White);
                _spriteBatch.DrawString(stratum2bold95, lastScore.ToString(), new Vector2(1130, 285), Color.White);
                _spriteBatch.DrawString(stratum2bold45, string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + " - " + c.Value).ToArray()), new Vector2(885, 655), Color.White);
                _spriteBatch.End();
            }

            // Used for debugging, e.g.: printing value
            //_spriteBatch.DrawString(font, "Debug: " + value, new Vector2(20, 720), Color.Black);



            base.Draw(gameTime);
        }
    }
}