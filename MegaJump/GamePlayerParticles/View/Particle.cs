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
        private static Vector2 GRAVITY = new Vector2(0, 0.3f);
        private bool m_isAlive;
        private Vector2 m_initialPosition; //To be abble to respawn with same position
        private Vector2 m_initialSpeed;    //To be able to respawn with same speed
        private Vector2 m_position;
        private Vector2 m_particleSpeed;

        public Particle(Vector2 a_modelPosition, Vector2 a_randomSpeed)
        {
            //m_particleSpeed = new Vector2(0.1f, -0.f) ;
            m_particleSpeed = a_randomSpeed;
            m_position = a_modelPosition;
            m_initialSpeed = a_randomSpeed;
            m_initialPosition = a_modelPosition;

            m_isAlive = true;
        }

        internal void Update(double a_elapsedTimeTotalSeconds)
        {
            if (m_isAlive)
            {
                //v1 = v0 + a *t
                m_particleSpeed = m_particleSpeed + GRAVITY * (float)a_elapsedTimeTotalSeconds;

                //s1 = s0 + var * t
                m_position = m_position + m_particleSpeed * (float)a_elapsedTimeTotalSeconds;

                //Check y position
                if (m_position.Y > 1.0f) m_isAlive = false;
            }

        }

        public void resetParticle()
        {
            m_isAlive = true;
            m_position = m_initialPosition;
            m_particleSpeed = m_initialSpeed;
        }

        public Vector2 getPosition()
        {
            return m_position;
        }

        public Vector2 getSpeed()
        {
            return m_particleSpeed;
        }

        public bool isAlive()
        {
            return m_isAlive;
        }

    }
}
