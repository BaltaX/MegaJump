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
using MegaJump.Model;
using MegaJump.Controller;

namespace MegaJump
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MasterController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager   m_graphics;
        Model.MainModel         m_mainModel = new Model.MainModel();
        View.MainView           m_mainView;
        bool                    m_gameOver = false;
        float                   m_gameOverTime;
        bool                    m_showMenu = true;
        IMGui                   m_IMGui;
        bool                    m_endOfGame=false;
        
        

        public MasterController()
        {
            m_graphics = new GraphicsDeviceManager(this);

            //Set the screen size
            m_graphics.IsFullScreen = false;
            m_graphics.PreferredBackBufferHeight = 960;
            m_graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Create a mainView, and send GraphicsDevice, so that spriteBatch can be used
            //if you want ContentManager to be member of MainView, then include it as an argument
            //but then you have to re-create the constructor in MainView
            m_mainView = new View.MainView(GraphicsDevice);

            //Load all assets
            m_mainView.LoadContent(Content);

            //Load IMGui
            m_IMGui = new IMGui(Content,GraphicsDevice,m_mainModel);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (m_showMenu)
            {
                int buttonSeparation = 75;
                int menuX = 280;
                int menuY = 685;

                this.IsMouseVisible = true;

                m_IMGui.DrawScores();

                if (m_IMGui.doButton(Mouse.GetState(), "Play", menuX, menuY))
                {
                    m_showMenu = false;
                    m_gameOverTime=0f;
                    m_mainModel.StartGame();
                    m_mainView.StartGame();
                    m_gameOver = false;
                    
                    return;
                }

                if (m_IMGui.doButton(Mouse.GetState(), "Continue", menuX, menuY+=buttonSeparation))
                {
                    m_showMenu = false;
                }

                if (m_IMGui.doButton(Mouse.GetState(), "Quit", menuX, menuY += buttonSeparation))
                {
                    this.Exit();
                }

               


                m_IMGui.setOldState(Mouse.GetState());
                
            
            }

            else if (m_mainModel.getClearedAllLevels() && m_endOfGame == false)
            {
                m_mainView.AllLevelsCleared();
                m_endOfGame = true;
                m_gameOverTime = (float)gameTime.ElapsedGameTime.Milliseconds;

            }


            else if (m_mainModel.getClearedAllLevels() && m_endOfGame == true)
            {
                //When game over, continue to update for one second
                m_gameOverTime = m_gameOverTime + (float)gameTime.ElapsedGameTime.Milliseconds;
                if (m_gameOverTime < 2000f)
                {
                    //Do nothing
                }

                //What to do after one second of game over 
                else
                {
                    m_showMenu = true;

                }

            }






            //First iteration where "game over" is encountered
            else if (m_mainModel.getGameOver() && m_gameOver == false)
            {
                m_mainView.GameEnd();
                //m_mainModel.makeMegamanFall();
                m_gameOver = true;
                m_gameOverTime = (float)gameTime.ElapsedGameTime.Milliseconds;


            }

            //The "logics" of what happens if game over
            else if (m_mainModel.getGameOver() && m_gameOver == true)
            {
                //When game over, continue to update for one second
                m_gameOverTime = m_gameOverTime + (float)gameTime.ElapsedGameTime.Milliseconds;
                if (m_gameOverTime < 2000f)
                {
                    m_mainModel.Update((float)gameTime.ElapsedGameTime.TotalSeconds, m_mainView);
                }

                //What to do after one second of game over 
                else
                {
                    m_showMenu = true;

                }
            }

            //If not game over
            else
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    m_mainModel.MovePlayerLeft();
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    m_mainModel.MovePlayerRight();
                }

                if (keyboardState.IsKeyDown(Keys.P))
                {
                    m_showMenu = true;
                }

                m_mainModel.Update((float)gameTime.ElapsedGameTime.TotalSeconds, m_mainView);

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (m_showMenu) return;


            else
            {
                GraphicsDevice.Clear(Color.White);

                //What should be drawn if game over
                if (m_mainModel.getGameOver())
                {
                    m_mainView.Draw(m_mainModel, gameTime.ElapsedGameTime.TotalSeconds);
                }

                //What should be drawn if not game over
                else
                {
                    m_mainView.Draw(m_mainModel, gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
            base.Draw(gameTime);
        }

        
    }
}
