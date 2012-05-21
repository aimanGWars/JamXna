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

            scoreFormat = "Test Score: {0:f}";
            scoreDimensions = hudFont.MeasureString(String.Format(scoreFormat, score));
            scorePosition = new Vector2(game.GraphicsDevice.Viewport.Width - scoreDimensions.X, 0);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            scoreString = String.Format(scoreFormat, score);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin();
            //spriteBatch.DrawString(hudFont, scoreString, scorePosition, Color.White);
            //spriteBatch.End();
            Color myTransparentColor = new Color(0, 0, 0, 127);

            Vector2 stringDimensions = hudFont.MeasureString(String.Format(scoreFormat, score));
            float width = stringDimensions.X;
            float height = stringDimensions.Y;

            Rectangle backgroundRectangle = new Rectangle();
            backgroundRectangle.Width = (int)width + 10;
            backgroundRectangle.Height = (int)height + 10;
            backgroundRectangle.X = (int)scorePosition.X - 5;
            backgroundRectangle.Y = (int)scorePosition.Y - 5;

            Texture2D dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { myTransparentColor });

            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, backgroundRectangle, myTransparentColor);
            spriteBatch.DrawString(hudFont, scoreString, scorePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
