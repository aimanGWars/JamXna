using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace _2dgame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HUD : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteFont hudFont;
        Game game;
        protected SpriteBatch spriteBatch;

        public float score;
        Vector2 scorePosition;
        Vector2 scoreDimensions;
        string scoreString;
        string scoreFormat;

        public int lives;
        string livesString;
        string livesFormat;
        Vector2 livesPosition;
        Vector2 livesDimension;

        public HUD(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// method for loading hud content
        /// </summary>
        protected override void LoadContent()
        {
            
            //create spritebatch object
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load font into hudFont
            hudFont = game.Content.Load<SpriteFont>("myFont");

            score = 999999999;
            lives = 5;

            scoreFormat = "Student Score: {0:f}";
            livesFormat = "Lives left:{0:d}";
            scoreDimensions = hudFont.MeasureString(String.Format(scoreFormat, score));
            livesDimension = hudFont.MeasureString(String.Format(livesFormat, lives));
            scorePosition = new Vector2(game.GraphicsDevice.Viewport.Width - scoreDimensions.X, 0);
            livesPosition = new Vector2(0, game.GraphicsDevice.Viewport.Height - livesDimension.Y);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            scoreString = String.Format(scoreFormat, score);
            livesString = String.Format(livesFormat, lives);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin();
            //spriteBatch.DrawString(hudFont, scoreString, scorePosition, Color.White);
            //spriteBatch.End();
            Color myTransparentColor = new Color(0, 0, 0, 127);

            Vector2 stringDimensions = hudFont.MeasureString(String.Format(scoreFormat, score));
            float scoreWidth = stringDimensions.X;
            float scoreHeight = stringDimensions.Y;
            Vector2 livesDimensions = hudFont.MeasureString(String.Format(livesFormat, lives));
            float livesWidth = livesDimensions.X;
            float livesHeight = livesDimensions.Y;

            Rectangle scoreBackgroundRectangle = new Rectangle();
            scoreBackgroundRectangle.Width = (int)scoreWidth + 10;
            scoreBackgroundRectangle.Height = (int)scoreHeight + 10;
            scoreBackgroundRectangle.X = (int)scorePosition.X - 5;
            scoreBackgroundRectangle.Y = (int)scorePosition.Y - 5;

            Rectangle livesBackgroundRectangle = new Rectangle();
            livesBackgroundRectangle.Width = (int)livesWidth + 10;
            livesBackgroundRectangle.Height = (int)livesHeight + 10;
            livesBackgroundRectangle.X = (int)livesPosition.X - 5;
            livesBackgroundRectangle.Y = (int)livesPosition.Y - 5;


            Texture2D dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { myTransparentColor });

            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, scoreBackgroundRectangle, myTransparentColor);
            spriteBatch.Draw(dummyTexture, livesBackgroundRectangle, myTransparentColor);
            spriteBatch.DrawString(hudFont, scoreString, scorePosition, Color.White);
            spriteBatch.DrawString(hudFont,livesString, livesPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
