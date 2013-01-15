using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MegaJump.View
{
    class ParticleSystem
    {

        //Members
        private const int MAX_PARTICLES = 100;                          //Number of particles
        Vector2 GRAVITY = new Vector2(0f, 5f);
        Texture2D m_particle;                                     //The particle image
        Particle[] m_particles = new Particle[MAX_PARTICLES];      //The array of particles of type Particle
        Random rand = new Random();                            //For random speed
        bool allDead;                                        //Keep track if all particles are below floor
        Random r = new Random();
        bool m_isAlive;
        SpriteFont font;
        
        


        //Constructor
        public ParticleSystem(Vector2 a_modelPosition, ContentManager a_content)
        {
            LoadContent(a_content);
            allDead = false;
            m_isAlive = true;
            //Create particles; model position given by MainView
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                m_particles[i] = new Particle(a_modelPosition, new Vector2(randomDirection().X,randomDirection().Y),GRAVITY);

            }

        }

        //Creates random float between -1 and 1
        private Vector2 randomDirection()
        {
            int maxSpeed = 15;
            Vector2 randomDirection = new Vector2(((float)rand.NextDouble() * 2f) - 1f, ((float)rand.NextDouble() * 2f) - 1f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
            return randomDirection;
        }


        //Load the textures
        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_particle = a_content.Load<Texture2D>("particle");
            font = a_content.Load<SpriteFont>("myFont");
        }


        internal void UpdateAndDraw(double a_elapsedTimeTotalSeconds, SpriteBatch m_spriteBatch, Camera m_camera, float displaceMentY)
        {
            
            
            for (int i = 0; i < MAX_PARTICLES; i++)//Går igenom alla partiklar
            {
                   //Update particle speed and position
                    m_particles[i].Update(a_elapsedTimeTotalSeconds);

                    //Get view coordinates of particle

                    Vector2 particleModelPositions = m_particles[i].getPosition();

                    Vector2 viewCoordinates = m_camera.translateCoordinates(m_particles[i].getPosition(), 960, 640, displaceMentY);

                   



                    //Create destination rectangle
                    Rectangle dest = new Rectangle((int)viewCoordinates.X, (int)viewCoordinates.Y, 10, 10);

                    Rectangle dest2 = new Rectangle(320,450, 32, 32);



                    
                    //Print particle
                    m_spriteBatch.Draw(m_particle, dest, Color.White);
                    m_spriteBatch.DrawString(font, "Particle 1 Y: " + viewCoordinates.Y.ToString(), new Vector2(240, 500), Color.White);
                    m_spriteBatch.DrawString(font, "Particle 1 X: " + viewCoordinates.X.ToString(), new Vector2(240, 540), Color.White);


            }
            

            
        }

        internal bool getIsAlive()
        {
            return m_isAlive;
        }
    }
}
