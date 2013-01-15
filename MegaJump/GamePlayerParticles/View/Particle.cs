using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.View
{
    class Particle
    {
        //private Microsoft.Xna.Framework.Vector2 m_modelPosition;
        private Vector2 m_gravity;// = new Vector2(0, 1f);
        
        private Vector2 m_initialPosition; //To be abble to respawn with same position
        private Vector2 m_initialSpeed;    //To be able to respawn with same speed
        private Vector2 m_position;
        private Vector2 m_particleSpeed;
        //private List<Vector2> m_particleSpeeds=new List<Vector2>();

        public Particle(Vector2 a_modelPosition, Vector2 a_speed, Vector2 a_gravity)
        {
            
            m_particleSpeed = a_speed;
            m_position = a_modelPosition;
            m_initialSpeed = a_speed;
            m_initialPosition = a_modelPosition;
            m_gravity = a_gravity;

           
        }

        internal void Update(double a_elapsedTimeTotalSeconds)
        {
            
                //v1 = v0 + a *t
                m_particleSpeed = m_particleSpeed + m_gravity * (float)a_elapsedTimeTotalSeconds;

                //s1 = s0 + var * t
                m_position = m_position + m_particleSpeed * (float)a_elapsedTimeTotalSeconds;

               
           

        }

        

        public Vector2 getPosition()
        {
            return m_position;
        }

        public Vector2 getSpeed()
        {
            return m_particleSpeed;
        }

        

    }
}
