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
        

        //Constructor
        public Player(Vector2 a_position)
        {
            //Initialize members
            m_position = a_position;
            m_speed = new Vector2(0.0f, -10.0f);
            
        }

        //Method updates player model position in each time step
        internal void Update(float a_elapsedTimeSeconds)
        {
            //Define the gravity
            Vector2 gravity = new Vector2(0.0f, 12f);

            //Integrate position
            m_position = m_position + m_speed * a_elapsedTimeSeconds + gravity * a_elapsedTimeSeconds * a_elapsedTimeSeconds;

            //Integrate position
            m_speed = m_speed + a_elapsedTimeSeconds * gravity;





            m_position.X = m_position.X + (a_elapsedTimeSeconds * m_speed.X);
            m_position.Y = m_position.Y + (a_elapsedTimeSeconds * m_speed.Y);
        }

        internal Vector2 getPosition()
        {
            return m_position;
        }

        internal Vector2 getSpeed()
        {
            return m_speed;
        }

       
        internal void reverseSpeedX()
        {
            m_speed.X = m_speed.X * (-1);
        }

        internal void reverseSpeedY()
        {
            m_speed.Y = m_speed.Y * (-1);
        }

        internal void SetPosition(float p, float p_2)
        {
            m_position = new Vector2(p, p_2);
        }

        internal void SetSpeed(float p, float p_2)
        {
            m_speed = new Vector2(p, p_2);
        }

        internal void MoveLeft()
        {
            m_position.X = m_position.X - 0.1f;
        }

        internal void MoveRight()
        {
            m_position.X = m_position.X + 0.1f;
        }
    }
}

