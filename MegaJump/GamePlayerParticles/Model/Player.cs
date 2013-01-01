using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.Model
{
    class Player
    {
        //Declare Members
        Vector2 m_position;// = new Vector2(0.1f, 0.1f);
        Vector2 m_speed;// = new Vector2(0.5f, 0.4f);
        float m_radius;// = (float)0.1;

        //Constructor
        public Player(Vector2 a_position, Vector2 a_speed, float a_radius)
        {
            //Initialize members
            m_position = a_position;
            m_speed = a_speed;
            m_radius = a_radius;
        }

        //Method updates player model position in each time step
        internal void Update(float a_elapsedTimeSeconds)
        {
            m_position.X = m_position.X + (a_elapsedTimeSeconds * m_speed.X);
            m_position.Y = m_position.Y + (a_elapsedTimeSeconds * m_speed.Y);
        }

        internal Vector2 getPosition()
        {
            return m_position;
        }

        internal float getPlayerRadius()
        {
            return m_radius;
        }

        internal void reverseSpeedX()
        {
            m_speed.X = m_speed.X * (-1);
        }

        internal void reverseSpeedY()
        {
            m_speed.Y = m_speed.Y * (-1);
        }
    }
}

