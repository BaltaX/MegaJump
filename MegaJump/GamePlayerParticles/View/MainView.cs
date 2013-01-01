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
        //...more assets here

        //Members declared
        private SpriteBatch m_spriteBatch;
        private int m_windowWidth;
        private int m_windowHeight;
        private Camera m_camera;
        private ParticleSystem m_particleSystem = new ParticleSystem(new Vector2(0.5f, 0.5f), 5);//Reference point for particle system


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
            float modelBallRadius = a_mainModel.getPlayerRadius();
            Vector2 viewCoordinates = m_camera.translateCoordinates(modelPosition, m_windowHeight, m_windowWidth);
            int viewLineDisplacementX = (int)(a_mainModel.getLineDisplacement() * m_windowWidth);
            int viewLineDisplacementY = (int)(a_mainModel.getLineDisplacement() * m_windowHeight);

            //Create destination rectangle for ball (needs integers)

            Rectangle m_destinationRect = new Rectangle((int)viewCoordinates.X - (int)(modelBallRadius * m_windowWidth / 2), (int)viewCoordinates.Y - (int)(modelBallRadius * m_windowHeight / 2), (int)(modelBallRadius * m_windowWidth), (int)(modelBallRadius * m_windowHeight));

            //the first 2 parameters must be changed if we change position
            //For this we use model coordinates and scale and replace them
            //We get those from a_mainModel, which was sent to this method

            //Create destination rectangle for frame line
            Rectangle line = new Rectangle(viewLineDisplacementX, viewLineDisplacementY, m_windowWidth - (viewLineDisplacementX * 2), m_windowHeight - (viewLineDisplacementY * 2));

            m_spriteBatch.Begin();

            //Partikelsystemet (Elden) Ritas ut inne i partikelsystemet! - OBS att kameran skickas med, och spriteBatch!!
            m_particleSystem.UpdateAndDraw(a_elapsedTimeTotalSeconds, m_spriteBatch, m_camera);

            //m_spriteBatch.Draw(m_frame, line, Color.White);
            //m_spriteBatch.Draw(m_ball, m_destinationRect, Color.White);

            m_spriteBatch.End();


        }

        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            //Load all texture2d assets here
            m_particleSystem.LoadContent(a_content);
            //This is called from MasterController
            m_ball = a_content.Load<Texture2D>("Ball2");
            m_frame = a_content.Load<Texture2D>("Line");
        }
    }
}
