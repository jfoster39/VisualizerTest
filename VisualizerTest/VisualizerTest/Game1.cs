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

namespace VisualizerTest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song mySong;
        Texture2D myTexture, backgroundTexture;
        Texture2D shapeTexture;

        VisualizationData visData = new VisualizationData();
        Color backgroundColor = Color.White;
        int barWidth = 5;
        int barHeight = 5;
        int ammount;
        int screenHeight = 500;
        int screenWidth = 500;

        public int getAverage(Point Between, VisualizationData visData) 
        {
            int average = 0;
            for (int i = Between.X; i < Between.Y; i++)
            {
                average += Convert.ToInt32(visData.Frequencies[i]);
            }
            int diff = Between.Y - Between.X + 1;
            return average / diff;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Loads the necessarry content for the "visualizer" including
        /// the song and background picture
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create 1x1 image
            myTexture = new Texture2D(GraphicsDevice, 1, 1);
            myTexture.SetData(new Color[] { Color.White });
            shapeTexture = new Texture2D(GraphicsDevice, 1, 1);
            shapeTexture.SetData(new Color[] { Color.MistyRose });

            backgroundTexture = Content.Load<Texture2D>("bg");
            mySong = Content.Load<Song>("song1");
            MediaPlayer.IsVisualizationEnabled = true;
            MediaPlayer.Play(mySong);
        }

        protected override void UnloadContent()
        {
            // Nothing needed here as of yet
        }

        /// <summary>
        /// Runs the logic for returning visualization data and 
        /// updating the visualizer. 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MediaPlayer.GetVisualizationData(visData);
            ammount = getAverage(new Point(0, 256), visData);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the visualizer should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Random rand = new Random();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), backgroundColor);

            if (MediaPlayer.State == MediaState.Playing)
            {
                for (int i = 0; i < 256; i++)
                {
                    backgroundColor = Color.FromNonPremultiplied(Convert.ToInt32(visData.Frequencies[ammount] * 255), 
                        Convert.ToInt32(visData.Frequencies[ammount] * 255), Convert.ToInt32(visData.Frequencies[ammount] * 255), 255);

                    spriteBatch.Draw(myTexture, new Rectangle(i*barWidth, (graphics.PreferredBackBufferHeight / 2) + 
                        Convert.ToInt32(i*visData.Samples[i]), barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 255));

                    spriteBatch.Draw(myTexture, new Rectangle(i * barWidth, (graphics.PreferredBackBufferHeight / 2) + 
                        Convert.ToInt32(i * visData.Samples[i]) + barHeight + 2,  barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 100));
                }
                
                // Here i'm trying to figure out how to draw things as the background
               // spriteBatch.Draw(shapeTexture, new Rectangle(rand.Next(screenWidth), rand.Next(screenHeight), 15, 15), Color.MistyRose);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
