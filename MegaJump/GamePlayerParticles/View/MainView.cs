﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MegaJump.Model;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace MegaJump.View
{
    class MainView : IModelObserver
    {
        //All assets announced here
        Texture2D m_ball;
        Texture2D m_frame;
        Texture2D m_megaMan;
        Texture2D m_megaManDead;
        Texture2D m_background;
        Texture2D m_scoreBoard; 
        SoundEffect m_soundEffect;
        SoundEffect m_end;
        SoundEffect m_bomb;
        Song m_backGroundSong;
        SoundEffect m_brickSound;
        
        //...more assets here

        //Members declared
        private SpriteBatch m_spriteBatch;
        private int m_windowWidth;
        private int m_windowHeight;
        private Camera m_camera;
        private Texture2D m_texture;
        private MegaJump.Model.Level.Tile[,] m_tiles;
        private ParticleSystem m_particleSystem = new ParticleSystem(new Vector2(0.5f, 0.5f), 5);//Reference point for particle system
        SpriteFont font;
        bool gameOver = false;

        private int m_viewscale=64;
        private int m_textureTileSize = 64;
        bool m_showMenu = true;
        bool m_showAnnouncement = false;

        public MainView(GraphicsDevice a_graphicsDevice)
        {
            //Members initialization
            m_spriteBatch = new SpriteBatch(a_graphicsDevice);
            m_windowWidth = a_graphicsDevice.Viewport.Width;
            m_windowHeight = a_graphicsDevice.Viewport.Height;
            m_camera = new Camera();
            
        }

        internal void Draw(Model.MainModel a_mainModel, double a_elapsedTimeTotalSeconds)
        {
            
                //Variables used for drawing game
                //Vector2 modelPosition = a_mainModel.getPlayerPosition();

                m_tiles = a_mainModel.getTiles();

                //Destination rectangle for Megaman

                //Get model coordinates for Megaman
                Vector2 megamanModelPos = a_mainModel.getPlayerPosition();

                //Model height of viewPort
                float modelViewPortY = (float)m_windowHeight / 64f;

                //Calculate model displacement for y
                float modelDisplacementY = megamanModelPos.Y - (modelViewPortY / 2.0f);

                //Calculate view DisplacementY for Y
                int viewDisplacementY = (int)(modelDisplacementY * 64f);

                //Translate to view coordinates for megaman
                Vector2 megamanViewPos = megamanModelPos * 64f;

                //Create destination rectangle for megaman
                Rectangle destRectMegaman = new Rectangle((int)megamanViewPos.X, (int)megamanViewPos.Y - viewDisplacementY, 64, 64);

                //Create destination rectangle for background
                Rectangle sourceBackgroundRectangle = new Rectangle(0, 100, 64, 64);

                Rectangle destBackgroundRectangle = new Rectangle(0, -(int)((float)viewDisplacementY / 27f), 640, 1920);

                //Create destination rectangle for scoreboard
                Rectangle destScoreBoard = new Rectangle(0, 10, 640, 60);

                m_spriteBatch.Begin();

                //Draw background
                m_spriteBatch.Draw(m_background, destBackgroundRectangle, Color.White);



                //Draw level
                for (int x = 0; x < a_mainModel.getlevelWidth(); x++)
                {
                    for (int y = 0; y < a_mainModel.getlevelHeight(); y++)
                    {
                        //Source rectangle
                        Rectangle sourceRectangle = new Rectangle((int)m_tiles[x, y] * m_textureTileSize, 0, m_viewscale, m_viewscale);

                        //Destination rectangle
                        //y ska ändras här för att passa spelarens position! Dvs istället för 100 ska vi ha displacement
                        //Med 100 har allting "flyttats ned" 100 pixlar
                        Rectangle destRect = new Rectangle((x * m_viewscale), (y * m_viewscale) - viewDisplacementY, m_viewscale, m_viewscale);

                        m_spriteBatch.Draw(m_texture, destRect, sourceRectangle, Color.White);


                    }
                }

                //Draw Megaman if not dead
                if (!gameOver)
                    m_spriteBatch.Draw(m_megaMan, destRectMegaman, Color.White);

                else
                    m_spriteBatch.Draw(m_megaManDead, destRectMegaman, Color.White);

                //Draw scoreboard
                m_spriteBatch.Draw(m_scoreBoard, destScoreBoard, Color.White);

                //Draw height score
                m_spriteBatch.DrawString(font, "Height: " + ((int)((400 - a_mainModel.getPlayerPosition().Y+a_mainModel.getAccumulatedHeight()))).ToString(), new Vector2(20, 30), Color.White);

                //Draw elapsed time
                m_spriteBatch.DrawString(font, "Time: " + String.Format("{0:0.00}", a_mainModel.getElapsedTime()), new Vector2(160, 30), Color.White);

                //Draw number of coins
                m_spriteBatch.DrawString(font, "Coins: " + a_mainModel.getNumberOfCoins().ToString(), new Vector2(300, 30), Color.White);

                //Draw score
                m_spriteBatch.DrawString(font, "Score: " + a_mainModel.getScore().ToString(), new Vector2(440, 30), Color.White);

                if (m_showAnnouncement)
                {

                    m_spriteBatch.DrawString(font, "GET READY FOR NEW LEVEL", new Vector2(300, 500), Color.White);
                }

               


                m_spriteBatch.End();
            
           
            
        }

        

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            //Load all texture2d assets here
            m_particleSystem.LoadContent(a_content);
            m_ball = a_content.Load<Texture2D>("Ball2");
            m_frame = a_content.Load<Texture2D>("Line");
            m_texture = a_content.Load<Texture2D>("Sprites");
            m_megaMan = a_content.Load<Texture2D>("Megaman");
            m_megaManDead = a_content.Load<Texture2D>("Megaman_dead");
            m_background = a_content.Load<Texture2D>("Background");
            m_scoreBoard = a_content.Load<Texture2D>("ScoreBoard");
            font = a_content.Load<SpriteFont>("myFont");
            m_soundEffect = a_content.Load<SoundEffect>("glass-clink");
            m_end = a_content.Load<SoundEffect>("end");
            m_bomb = a_content.Load<SoundEffect>("Bomb");
            m_backGroundSong = a_content.Load<Song>("Soundtrack2");
            m_brickSound = a_content.Load<SoundEffect>("Bricksound");

            //Denna ska inte vara här!
            MediaPlayer.Play(m_backGroundSong);
        }

        public void CollisionPlayerCoin()
        {
            Random rand=new Random();
            m_soundEffect.Play(0.4f,(float)(rand.NextDouble())*1.2f-1f,0f);
        }

        public void CollisionBomb()
        {
            m_bomb.Play();
        }


        public void GameEnd()
        {
            m_end.Play();
            gameOver = true;
        }

        internal void StartGame()
        {
            gameOver = false;
           
        }


        public void DrawAnnouncement(bool p)
        {
            m_showAnnouncement = p;
        }


        public void CollisionBrick()
        {
            Random rand = new Random();
            m_brickSound.Play(0.3f, (float)(rand.NextDouble()) * 1.2f - 1f, 0f);
        }
    }
}
