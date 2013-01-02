using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.Model
{
    class MainModel
    {

        //Here is the place to create instances of objects in game like player, ball, pad etc.
        //Tip: create the instance here and let VS create a stub
        //They should be created in respective folders
        //Example
        //Pad m_pad = new Pad();
        //Player m_player = new Player();

        Player m_player = new Player(new Vector2(0.5f, 0.8f));
        Level m_level = new Level();
        const float DISPLACEMENT_OF_LINE = 0.02f;

        internal void Update(float a_elapsedTimeSeconds)
        {
            //Here is the place to distribute the update command to update methods in the 
            //instances of secondary model classes created above
            //Example:
            //m_pad.Update(a_elapsedTimeSeconds);

            m_player.Update(a_elapsedTimeSeconds);

            //Check bottom level
            if (m_player.getPosition().Y > 0.8) m_player.SetPosition(m_player.getPosition().X, 0.8f);

            //Variables used for game logic for bouncing ball
            Vector2 m_position = m_player.getPosition();
            //float m_radius = m_player.getPlayerRadius();

            //Game logic for bouncing ball
            //if (m_position.X > 1 - (m_radius / 2) - DISPLACEMENT_OF_LINE || m_position.X < 0 + (m_radius / 2) + DISPLACEMENT_OF_LINE) m_player.reverseSpeedX();//Horizontal bounce
            //if (m_position.Y > 1 - (m_radius / 2) - DISPLACEMENT_OF_LINE || m_position.Y < 0 + (m_radius / 2) + DISPLACEMENT_OF_LINE) m_player.reverseSpeedY();//Vertical bounce
        }

        internal Vector2 getPlayerPosition()
        {
            return m_player.getPosition();
        }

        //internal float getPlayerRadius()
        //{
        //    return m_player.getPlayerRadius();
        //}

        internal float getLineDisplacement()
        {
            return DISPLACEMENT_OF_LINE;
        }

        public Level.Tile[,] getTiles()
        {
            return m_level.getTiles();
        }

        public int getlevelWidth()
        {
            return m_level.getlevelWidth();
        }

        public int getlevelHeight()
        {
            return  m_level.getlevelHeight(); ;
        }


        internal void MovePlayerLeft()
        {
            m_player.MoveLeft();
        }

        internal void MovePlayerRight()
        {
            m_player.MoveRight();
        }
    }
}
