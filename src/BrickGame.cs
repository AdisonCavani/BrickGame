using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace BrickGame
{
    public class BrickGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ScoreManager _scoreManager;
        private int score;

        Character player;

        List<Platform> platforms = new List<Platform>();

        // Bricks list
        Brick1 brick1;
        Brick2 brick2;
        Brick3 brick3;

        // Coins list
        Coin1 coin1;

        Life life;

        // Sounds
        List<SoundEffect> soundEffects;
        Song music;

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

        SpriteFont font;

        float angle1 = 0;
        Vector2 orgin1 = new Vector2(0, 0);

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
            showFPS = true; // Show FPS

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
            _scoreManager = ScoreManager.Load();

            //_graphics.PreferMultiSampling = true;
            //GraphicsDevice.PresentationParameters.MultiSampleCount = 8;

            // TODO: use this.Content to load your game content here

            life = new Life(Content.Load<Texture2D>("textures\\health"), new Vector2(20, 20));

            player = new Character(Content.Load<Texture2D>("textures\\heroStop"), new Vector2(50, 590));

            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(30, 700)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(400, 580)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(800, 460)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(500, 340)));
            platforms.Add(new Platform(Content.Load<Texture2D>("textures\\platforms\\Platform2_small"), new Vector2(100, 220)));

            brick1 = (new Brick1(Content.Load<Texture2D>("textures\\brick1")));
            brick2 = (new Brick2(Content.Load<Texture2D>("textures\\brick2")));
            brick3 = (new Brick3(Content.Load<Texture2D>("textures\\brick3")));

            coin1 = (new Coin1(Content.Load<Texture2D>("textures\\coin")));

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

            // Font loading
            font = Content.Load<SpriteFont>("fonts\\Font");
            notoSansBold = Content.Load<SpriteFont>("fonts\\notoSansBold");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            player.Update(gameTime, Content);
            life.Update(gameTime, player.position, score);

            _ = brick1.UpdateAsync(gameTime, player.position, player.rectangle);
            _ = brick2.UpdateAsync(gameTime, player.position, player.rectangle);
            _ = brick3.UpdateAsync(gameTime, player.position, player.rectangle);

            _ = coin1.UpdateAsync(gameTime, player.position, player.rectangle);

            foreach (Platform platform in platforms)
            {
                if (player.rectangle.isOnTopOf(platform.rectangle))
                {
                    player.velocity.Y = 0f;
                    player.hasJumped = false;                   
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && player.hasJumped == false && player.rectangle.isOnTopOf(platform.rectangle))
                {
                    player.position.Y -= 17f;
                    player.velocity.Y = -6f;
                    player.hasJumped = true;
                }
            }

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

            frametime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            fps = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: ResolutionScale);
            _spriteBatch.Draw(background1, background1Pos, Color.White);
            _spriteBatch.Draw(smallCloudMirror, smallCloudMirrorPos, Color.White);
            _spriteBatch.Draw(bigCloud3, bigCloud3Pos, Color.White);
            _spriteBatch.Draw(background1Color, background1ColorPos, Color.White);
            _spriteBatch.Draw(smallCloud1, smallCloud1Pos, Color.White);
            _spriteBatch.Draw(smallCloud2, smallCloud2Pos, Color.White);
            _spriteBatch.Draw(bigCloud1, bigCloud1Pos, Color.White);
            _spriteBatch.Draw(bigCloud2, bigCloud2Pos, Color.White);

            foreach(Platform platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }

            brick1.Draw(_spriteBatch);
            brick2.Draw(_spriteBatch);
            brick3.Draw(_spriteBatch);

            coin1.Draw(_spriteBatch);

            _spriteBatch.Draw(player.texture, player.position, player.animation, Color.White);

            life.Draw(_spriteBatch);

            _spriteBatch.DrawString(font, "Highscores:\n" + string.Join("\n", _scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(20, 100), Color.Black);
            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, 70), Color.Black);

            if (showFPS == true)
            {
                _spriteBatch.DrawString(notoSansBold, "FPS: " + fps.ToString("0"), new Vector2((ResolutionNativeWidth - 85), 10), Color.White, angle1, orgin1, 1.0f, SpriteEffects.None, 1);
                _spriteBatch.DrawString(notoSansBold,frametime.ToString("0.0") + " ms", new Vector2((ResolutionNativeWidth - 85), 30), Color.White, angle1, orgin1, 1.0f, SpriteEffects.None, 1);
            }

            // Used for debugging, e.g.: printing value
            //_spriteBatch.DrawString(font, "Debug: " + ResolutionTargetHeight, new Vector2(20, 720), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}