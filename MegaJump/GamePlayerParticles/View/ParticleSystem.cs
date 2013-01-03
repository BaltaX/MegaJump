using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MegaJump.View
{
    class ParticleSystem
    {

        //Members
        private const int MAX_PARTICLES = 10000;                          //Number of particles
        Texture2D m_particle;                                     //The particle image
        Particle[] m_particles = new Particle[MAX_PARTICLES];      //The array of particles of type Particle
        Random rand = new Random();                            //For random speed
        bool allDead;                                        //Keep track if all particles are below floor
        int m_numberOfExplosions;                           //Given by MainView
        Random r = new Random();
        SpriteFont font;
        int m_numberOfParticlesAlive;

        //Constructor
        public ParticleSystem(Vector2 a_modelPosition, int a_numberOfExplosions)
        {
            m_numberOfExplosions = a_numberOfExplosions;
            allDead = false;

            //Create particles; model position given by MainView
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                m_particles[i] = new Particle(a_modelPosition, randomDirection());

            }

        }

        //Creates random float between -1 and 1
        private Vector2 randomDirection()
        {
            int maxSpeed = 1;
            Vector2 randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
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


        internal void UpdateAndDraw(double a_elapsedTimeTotalSeconds, SpriteBatch m_spriteBatch, Camera m_camera)
        {
            m_numberOfParticlesAlive = 0;
            //Assume all particles are dead
            allDead = true;
            for (int i = 0; i < MAX_PARTICLES; i++)//Går igenom alla partiklar
            {
                //If particle[i] is alive
                if (m_particles[i].isAlive())
                {
                    m_numberOfParticlesAlive++;

                    //Set "All particles are dead" to false
                    allDead = false;

                    //Update particle speed and position
                    m_particles[i].Update(a_elapsedTimeTotalSeconds);

                    //Get view coordinates of particle
                    Vector2 viewCoordinates = m_camera.translateCoordinates(m_particles[i].getPosition(), 1000, 1000,64f);

                    //Create destination rectangle
                    Rectangle dest = new Rectangle((int)viewCoordinates.X, (int)viewCoordinates.Y, 20, 20);


                    float rotation = (float)r.NextDouble();
                    //Print particle
                    m_spriteBatch.Draw(m_particle, dest, null, Color.White, rotation, new Vector2(10f, 10f), SpriteEffects.None, 0f);
                }


            }
            m_spriteBatch.DrawString(font, "Butterflies alive: " + m_numberOfParticlesAlive.ToString(), new Vector2(20, 45), Color.White);
            if (m_numberOfExplosions > -1)
            {
                m_spriteBatch.DrawString(font, "Explosions left: " + m_numberOfExplosions.ToString(), new Vector2(20, 75), Color.White);
            }


            else
            {
                m_spriteBatch.DrawString(font, "Explosions left: 0", new Vector2(20, 75), Color.White);
            }

            if (allDead)
            {
                m_numberOfExplosions--;
                //To only let particles respawn if we have not reached the number of explosions
                if (m_numberOfExplosions > 0)
                {
                    allDead = false;
                    for (int j = 0; j < MAX_PARTICLES; j++)//Går igenom alla partiklar
                    {
                        m_particles[j].resetParticle();
                    }
                }
            }
        }
    }
}
