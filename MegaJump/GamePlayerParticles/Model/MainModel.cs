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

        Player m_player = new Player(new Vector2(5f, 500f));
        Level m_level = new Level("Content/Englalevel.txt");
        float m_maxHeight=0f;
        bool m_gameOver = false;
       
        
        
        
        const float DISPLACEMENT_OF_LINE = 0.02f;

        internal void Update(float a_elapsedTimeSeconds, IModelObserver a_observer)
        {
            //Here is the place to distribute the update command to update methods in the 
            //instances of secondary model classes created above
            //Example:
            //m_pad.Update(a_elapsedTimeSeconds);
            float currentHeight = 500f - m_player.getPosition().Y;
            if (currentHeight > m_maxHeight) m_maxHeight = currentHeight;
            if (currentHeight < m_maxHeight-18f)
            {
                m_gameOver = true;
                
            }
            m_player.Update(a_elapsedTimeSeconds);

            //Check bottom level
            if (m_player.getPosition().Y > 500f)
            {
                m_player.SetPosition(m_player.getPosition().X, 500);
                
            }

            //Check left wall
            if (m_player.getPosition().X < 0f)
            {
                m_player.SetPosition(0f,m_player.getPosition().Y);
            }

            //Check right wall
            if (m_player.getPosition().X >m_level.getlevelWidth()-1f)
            {
                m_player.SetPosition(m_level.getlevelWidth() - 1f, m_player.getPosition().Y);
            }

            //Check for collisions
            if (m_gameOver == false)
            {
                //Get model coordinates for player upper left
                Vector2 modelPlayerCoordinates = m_player.getPosition();

                //Get all tiles
                MegaJump.Model.Level.Tile[,] m_tiles = m_level.getTiles();

                //Go through all tiles
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

                            //If distance is small enough for a collision (1f)
                            if (distancePlayerCoin < 0.95f)
                            {
                                //Take away coin
                                m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                                //Increase playeer speed
                                m_player.SetSpeed(m_player.getSpeed().X, -10f);
                                //Let view know of collision
                                a_observer.CollisionPlayerCoin();
                            }

                        }

                        //Check if it is a star
                        if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_STAR)
                        {
                            //Get model coordinates for star
                            Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                            //Calculate distance between star and player
                            float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                            //If distance is small enough for a collision (1f)
                            if (distancePlayerCoin < 1f)
                            {
                                //Take away coin
                                m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                                //Increase speed dramatically 
                                //(note: this is not what I want finally. I want a constant high speed with no gravity for a few seconds)
                                m_player.SetSpeed(m_player.getSpeed().X, -20f);
                                //Let view know about collision
                                //I want another method for this in IModelObserver (play another sound+particle system bound to player to visualize speed)
                                a_observer.CollisionPlayerCoin();
                            }

                        }

                        //Check if it is a brick
                        if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_BRICK)
                        {
                            //Get model coordinates for star
                            Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                            //Calculate distance between star and player
                            float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                            //If distance is small enough for a collision (1f)
                            if (distancePlayerCoin < 1f)
                            {
                                //Take away coin
                                m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                                //Increase speed dramatically 
                                //(note: this is not what I want finally. I want a constant high speed with no gravity for a few seconds)
                                m_player.SetSpeed(-m_player.getSpeed().X*0.75f, -m_player.getSpeed().Y*0.75f);
                                //m_player.SetPosition(m_player.getPosition().X, m_player.getPosition().Y-0.25f);
                                //Let view know about collision
                                //I want another method for this in IModelObserver (play another sound+particle system bound to player to visualize speed)
                                a_observer.CollisionPlayerCoin();

                            }

                        }

                        //Check if it is a hindrance
                        if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_BOMB)
                        {
                            //Get model coordinates for star
                            Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                            //Calculate distance between star and player
                            float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                            //If distance is small enough for a collision (1f)
                            if (distancePlayerCoin < 1f)
                            {
                                //Take away coin
                                m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                                //Increase speed dramatically 
                                //(note: this is not what I want finally. I want a constant high speed with no gravity for a few seconds)
                                m_player.SetSpeed(-m_player.getSpeed().X * 0.75f, -m_player.getSpeed().Y * 0.75f);
                                //m_player.SetPosition(m_player.getPosition().X, m_player.getPosition().Y-0.25f);
                                //Let view know about collision
                                //I want another method for this in IModelObserver (play another sound+particle system bound to player to visualize speed)
                                a_observer.CollisionBomb();
                                m_gameOver = true;
                            }

                        }

                        //Check if it is a hindrance
                        if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_MARBLE)
                        {
                            //Get model coordinates for star
                            Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                            //Calculate distance between star and player
                            float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                            //If distance is small enough for a collision (1f)
                            if (distancePlayerCoin < 1f)
                            {
                                //Rectify x and y of Megaman
                                //Check if MegaMan intersects at the top of brick
                                float modelX=modelPlayerCoordinates.X;
                                float modelY=modelPlayerCoordinates.Y;

                                if (modelPlayerCoordinates.Y < modelCoinCoordinates.Y)
                                {
                                    modelY = modelCoinCoordinates.Y - 1.02f;

                                    if (modelPlayerCoordinates.X > modelCoinCoordinates.X - 1f && modelPlayerCoordinates.X < modelCoinCoordinates.X)
                                    {
                                        modelX = modelCoinCoordinates.X - 1.0f;
                                    }
                                    else if (modelPlayerCoordinates.X > modelCoinCoordinates.X+0.7f)
                                    {
                                        modelX = modelCoinCoordinates.X + 1.0f;
                                    }
                                }

                                else if (modelPlayerCoordinates.Y > modelCoinCoordinates.Y)
                                {
                                    modelY = modelCoinCoordinates.Y + 1.02f;

                                    if (modelPlayerCoordinates.X > modelCoinCoordinates.X - 1f && modelPlayerCoordinates.X < modelCoinCoordinates.X)
                                    {
                                        modelX = modelCoinCoordinates.X - 1.0f;
                                    }
                                    else if (modelPlayerCoordinates.X > modelCoinCoordinates.X + 0.7f)
                                    {
                                        modelX = modelCoinCoordinates.X + 1.0f;
                                    }
                                }

                                    //else if (modelPlayerCoordinates.Y > modelCoinCoordinates.Y)
                                    //{
                                    //    modelY = modelCoinCoordinates.Y + 1.02f;
                                    //}
                                

                                //Check if MegaMan intersects at the right of brick
                                

                                //Check if MegaMan intersects at the left of brick
                                

                                m_player.SetPosition(modelX, modelY);
                                //Take away coin
                                //m_tiles[x, y] = MegaJump.Model.Level.Tile.T_EMPTY;
                                //Increase speed dramatically 
                                //(note: this is not what I want finally. I want a constant high speed with no gravity for a few seconds)
                                m_player.SetSpeed(0.0f, 0.0f);
                                //m_player.SetPosition(m_player.getPosition().X, m_player.getPosition().Y-0.25f);
                                //Let view know about collision
                                //I want another method for this in IModelObserver (play another sound+particle system bound to player to visualize speed)
                                //a_observer.CollisionPlayerCoin();

                            }

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

        
        //internal float getLineDisplacement()
        //{
        //    return DISPLACEMENT_OF_LINE;
        //}

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

        internal bool getGameOver()
        {
            return m_gameOver;
        }

        internal void makeMegamanFall()
        {
            m_player.SetGravity(m_player.getGravity().X, 25f);
        }

        internal void StartGame()
        {
            m_gameOver = false;
            m_maxHeight = 0f;
            m_player.Initialize(new Vector2(5f, 500f));
            m_level.Initialize("Content/Levell1.txt");
            
            
        }
    }
}
