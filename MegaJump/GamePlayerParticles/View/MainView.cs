using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace MegaJump.View
{
    class MainView
    {
        //All assets announced here
        Texture2D m_ball;
        Texture2D m_frame;
        Texture2D m_megaMan;
        //...more assets here

        //Members declared
        private SpriteBatch m_spriteBatch;
        private int m_windowWidth;
        private int m_windowHeight;
        private Camera m_camera;
        private Texture2D m_texture;
        private MegaJump.Model.Level.Tile[,] m_tiles;
        private ParticleSystem m_particleSystem = new ParticleSystem(new Vector2(0.5f, 0.5f), 5);//Reference point for particle system
        

        private int m_viewscale=64;
        private int m_textureTileSize = 64;


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
            Vector2 modelPosition = a_mainModel.getPlayerPosition();
            //float modelBallRadius = a_mainModel.getPlayerRadius();
            Vector2 viewCoordinates = m_camera.translateCoordinates(modelPosition, m_windowHeight, m_windowWidth);
            int viewLineDisplacementX = (int)(a_mainModel.getLineDisplacement() * m_windowWidth);
            int viewLineDisplacementY = (int)(a_mainModel.getLineDisplacement() * m_windowHeight);
            m_tiles = a_mainModel.getTiles();
            
            //Destination rectangle for Megaman

            //Get model coordinates for Megaman
            Vector2 megamanModelPos = a_mainModel.getPlayerPosition();

            //Translate to model coordinates for megaman
            Vector2 megamanViewPos=m_camera.translateCoordinates(megamanModelPos,m_windowHeight,m_windowWidth);

            //Create destination rectangle for megaman
            Rectangle destRectMegaman = new Rectangle((int)megamanViewPos.X, (int)megamanViewPos.Y, 64, 64);

            
            m_spriteBatch.Begin();

                //Draw level
                for (int x = 0; x < a_mainModel.getlevelWidth(); x++)
                { 
                    for(int y=0; y<a_mainModel.getlevelHeight();y++)
                    {
                        //Source rectangle
                        Rectangle sourceRectangle = new Rectangle((int)m_tiles[x, y] * m_textureTileSize, 0, m_viewscale, m_viewscale);

                        //Destination rectangle
                        Rectangle destRect = new Rectangle(x * m_viewscale, y * m_viewscale, m_viewscale, m_viewscale);

                        m_spriteBatch.Draw(m_texture, destRect, sourceRectangle, Color.White);
                    }
                }

                //Draw Megaman
                m_spriteBatch.Draw(m_megaMan, destRectMegaman, Color.White);

            m_spriteBatch.End();


        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            //Load all texture2d assets here
            m_particleSystem.LoadContent(a_content);
            //This is called from MasterController
            m_ball = a_content.Load<Texture2D>("Ball2");
            m_frame = a_content.Load<Texture2D>("Line");
            m_texture = a_content.Load<Texture2D>("Sprites");
            m_megaMan = a_content.Load<Texture2D>("Megaman");
        }
    }
}
