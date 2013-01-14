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
        private const int MAX_PARTICLES = 1;                          //Number of particles
        Texture2D m_particle;                                     //The particle image
        Particle[] m_particles = new Particle[MAX_PARTICLES];      //The array of particles of type Particle
        Random rand = new Random();                            //For random speed
        bool allDead;                                        //Keep track if all particles are below floor
        int m_numberOfExplosions;                           //Given by MainView
        Random r = new Random();
        


        //Constructor
        public ParticleSystem(Vector2 a_modelPosition, int a_numberOfExplosions, ContentManager a_content)
        {
            LoadContent(a_content);
            m_numberOfExplosions = a_numberOfExplosions;
            allDead = false;
            
            //Create particles; model position given by MainView
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                m_particles[i] = new Particle(a_modelPosition, new Vector2(3f,-8f));

            }

        }

        //Creates random float between -1 and 1
        //private Vector2 randomDirection()
        //{
        //    int maxSpeed = 30;
        //    Vector2 randomDirection = new Vector2((float)rand.NextDouble() - 15f, (float)rand.NextDouble() - 15f);
        //    //normalize to get it spherical vector with length 1.0
        //    randomDirection.Normalize();
        //    //add random length between 0 to maxSpeed
        //    randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
        //    return randomDirection;
        //}


        //Load the textures
        internal void LoadContent(Microsoft.Xna.Framework.Content.ContentManager a_content)
        {
            m_particle = a_content.Load<Texture2D>("particle");
        }


        internal void UpdateAndDraw(double a_elapsedTimeTotalSeconds, SpriteBatch m_spriteBatch, Camera m_camera)
        {
            
            
            for (int i = 0; i < MAX_PARTICLES; i++)//Går igenom alla partiklar
            {
                   //Update particle speed and position
                    m_particles[i].Update(a_elapsedTimeTotalSeconds);

                    //Get view coordinates of particle

                    Vector2 particleModelPositions = m_particles[i].getPosition();
                    Vector2 viewCoordinates = m_camera.translateCoordinates(m_particles[i].getPosition(), 640, 960,0f);

                   



                    //Create destination rectangle
                    Rectangle dest = new Rectangle((int)viewCoordinates.X, (int)viewCoordinates.Y, 20, 20);


                    
                    //Print particle
                    m_spriteBatch.Draw(m_particle, dest, Color.White);
                


            }
            

            
        }
    }
}
