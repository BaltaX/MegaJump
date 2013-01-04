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

        Player m_player = new Player(new Vector2(5f, 320f));
        Level m_level = new Level();
       
        
        
        
        const float DISPLACEMENT_OF_LINE = 0.02f;

        internal void Update(float a_elapsedTimeSeconds, IModelObserver a_observer)
        {
            //Here is the place to distribute the update command to update methods in the 
            //instances of secondary model classes created above
            //Example:
            //m_pad.Update(a_elapsedTimeSeconds);

            m_player.Update(a_elapsedTimeSeconds);

            //Check bottom level
            if (m_player.getPosition().Y > 320f)
            {
                m_player.SetPosition(m_player.getPosition().X, 320f);
                
            }

            //Check for collisions

            //Get model coordinates for player upper left
            Vector2 modelPlayerCoordinates = m_player.getPosition();
            //Go through all tiles
            MegaJump.Model.Level.Tile[,] m_tiles = m_level.getTiles();

            for (int x = 0; x < m_level.getlevelWidth(); x++)
            {
                for (int y = 0; y < m_level.getlevelHeight(); y++)
                { 
                //Check if it is a coin
                    if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_COIN)
                    {
                        //Get model coordinates for coin
                        Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                        //Calculate distance between coin and player
                        float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                        //Determine if distance is little enough to imply collision (64)
                        if (distancePlayerCoin < 1f)
                        {
                            m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                            m_player.SetSpeed(m_player.getSpeed().X, -10f);
                            a_observer.CollisionPlayerCoin();
                        }

                    }

                }
            
            }
        }


        //POCO
        internal Vector2 getPlayerPosition()
        {
            return m_player.getPosition();
        }


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
