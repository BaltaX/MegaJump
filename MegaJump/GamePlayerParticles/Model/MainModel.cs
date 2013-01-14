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

        Player m_player = new Player(new Vector2(5f, 400f));
        //Level m_level;
        
        List<Level> m_levels=new List<Level>();
        float m_maxHeight=0f;
        bool m_gameOver = false;
        float m_recordHeight = 0;
        float m_timeElapsed = 0;
        int m_numberOfCoins = 0;
        int m_recordCoins = 0;
        int m_score = 0;
        int m_recordScore = 0;
        int m_currentLevel = 0;
        float m_accumulatedHeight = 0;
        float m_currentHeight = 0;
        bool m_NewLevelAnnounced = false;
        float m_levelAnnouncementTime = 0;
        bool m_allLevelsCleared = false;
        const float DISPLACEMENT_OF_LINE = 0.02f;

        public MainModel()
        { 
            
        //Add the levels

            m_levels.Add(new Level("Content/Levell1.txt"));
            m_levels.Add(new Level("Content/Level2.txt"));
            m_levels.Add(new Level("Content/Level2.txt"));
        }


        internal void Update(float a_elapsedTimeSeconds, IModelObserver a_observer)
        {
            if (!m_NewLevelAnnounced)
            {
                //Here is the place to distribute the update command to update methods in the 
                //instances of secondary model classes created above
                //Example:
                //m_pad.Update(a_elapsedTimeSeconds);

                //Update elapsed time
                m_timeElapsed += a_elapsedTimeSeconds;

                //Update score (only if it has increased)
                int intermediateScore = (int)(m_maxHeight * m_numberOfCoins - (m_timeElapsed * 10f));

                if (intermediateScore > m_score)
                    m_score = intermediateScore;


                float currentHeight = 400f - m_player.getPosition().Y + m_accumulatedHeight;
                if (currentHeight > m_maxHeight) m_maxHeight = currentHeight;
                if (currentHeight < m_maxHeight - 17f)
                {
                    m_gameOver = true;
                    
                    if (m_maxHeight > m_recordHeight)
                    {
                        m_recordHeight = m_maxHeight;
                    }

                    if (m_numberOfCoins > m_recordCoins)
                    {
                        m_recordCoins = m_numberOfCoins;
                    }

                    if (m_score > m_recordScore)
                    {
                        m_recordScore = m_score;
                    }



                }

                if (currentHeight > 400f + m_accumulatedHeight)
                {
                    if (m_currentLevel+1 < m_levels.Count())
                    {
                        NextLevel();
                        a_observer.LevelAnnouncement(m_currentLevel + 1);
                    }

                    else
                    {
                        m_allLevelsCleared = true;
                        return;
                    }
                }

                m_player.Update(a_elapsedTimeSeconds);

                //Check bottom level
                if (m_player.getPosition().Y > 400f)
                {
                    m_player.SetPosition(m_player.getPosition().X, 400);
                    m_gameOver = true;

                }

                //Check left wall
                if (m_player.getPosition().X < 0f)
                {
                    m_player.SetPosition(0f, m_player.getPosition().Y);
                }

                //Check right wall
                if (m_player.getPosition().X > m_levels[m_currentLevel].getlevelWidth() - 1f)
                {
                    m_player.SetPosition(m_levels[m_currentLevel].getlevelWidth() - 1f, m_player.getPosition().Y);
                }

                //Check for collisions
                if (m_gameOver == false)
                {
                    //Get model coordinates for player upper left
                    Vector2 modelPlayerCoordinates = m_player.getPosition();

                    //Get all tiles
                    MegaJump.Model.Level.Tile[,] m_tiles = m_levels[m_currentLevel].getTiles();

                    //Go through all tiles
                    for (int x = 0; x < m_levels[m_currentLevel].getlevelWidth(); x++)
                    {
                        for (int y = 0; y < m_levels[m_currentLevel].getlevelHeight(); y++)
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
                                    //Increase coins collected
                                    m_numberOfCoins++;
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
                                    m_player.SetSpeed(-m_player.getSpeed().X * 0.75f, -m_player.getSpeed().Y * 0.75f);
                                    //m_player.SetPosition(m_player.getPosition().X, m_player.getPosition().Y-0.25f);
                                    //Let view know about collision
                                    //I want another method for this in IModelObserver (play another sound+particle system bound to player to visualize speed)
                                    a_observer.CollisionBrick();

                                }

                            }

                            //Check if it is a bomb
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

                            //Check if it is a bomb
                            if (m_tiles[x, y] == MegaJump.Model.Level.Tile.T_FOG)
                            {
                                //Get model coordinates for star
                                Vector2 modelCoinCoordinates = new Vector2((float)x, (float)y);

                                //Calculate distance between star and player
                                float distancePlayerCoin = Vector2.Distance(modelCoinCoordinates, modelPlayerCoordinates);

                                //If distance is small enough for a collision (1f)
                                if (distancePlayerCoin < 1f)
                                {
                                    m_player.SetSpeed(m_player.getSpeed().X * (1 - a_elapsedTimeSeconds*0.75f), m_player.getSpeed().Y*(1 - a_elapsedTimeSeconds*0.75f));
                                    //m_player.SetPosition(m_player.getPosition().X, m_player.getPosition().Y-0.25f);
                                    
                                }

                            }



                            //Check if it is a MARBLE
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
                                    float modelX = modelPlayerCoordinates.X;
                                    float modelY = modelPlayerCoordinates.Y;

                                    if (m_player.getPosition().X < 0.1)
                                    {
                                        m_player.SetPosition(m_player.getPosition().X + 8.5f, m_player.getPosition().Y);
                                        int i;
                                    }

                                    if (m_player.getPosition().X > 8.9f)
                                    {
                                        m_player.SetPosition(m_player.getPosition().X - 8.5f, m_player.getPosition().Y);
                                    }
                                }

                            }

                        }

                    }
                }
            }
            
            //If we announce new level
            else 
            {
                //Keep track of the time
                m_levelAnnouncementTime += a_elapsedTimeSeconds;
                if (m_levelAnnouncementTime < 5f)
                {
                    a_observer.DrawAnnouncement(true);
                    
                }

                else
                {
                    a_observer.DrawAnnouncement(false);
                    m_levelAnnouncementTime=0;
                    m_NewLevelAnnounced=false;
                }
            
            }
        }

        private void NextLevel()
        {
            m_accumulatedHeight+=400f;
            m_maxHeight = 0f;
            m_player.Initialize(new Vector2(5f, 400f));
            m_currentLevel++;
            m_NewLevelAnnounced = true;
            //Check that we have not reached the end of levels here

            //Get current level url
            string currentLevelUrl = m_levels[m_currentLevel].getPath();

            m_levels[m_currentLevel].Initialize(currentLevelUrl);
            
        }

        


        //Getters and setters
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
            return m_levels[m_currentLevel].getTiles();
        }

        public int getlevelWidth()
        {
            return m_levels[m_currentLevel].getlevelWidth();
        }

        public int getlevelHeight()
        {
            return m_levels[m_currentLevel].getlevelHeight(); ;
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
            m_currentLevel = 0;
            m_accumulatedHeight = 0;
            m_player.Initialize(new Vector2(5f, 400f));


            //Get current level url
            string currentLevelUrl = m_levels[m_currentLevel].getPath();

            m_levels[m_currentLevel].Initialize(currentLevelUrl);
            m_timeElapsed = 0;
            m_numberOfCoins = 0;
            m_score = 0;
            
        }

        internal float getMaxHeight()
        {
            return m_maxHeight;
        }

        internal float getRecordHeight()
        {
            return m_recordHeight;
        }

        internal float getElapsedTime()
        {
            return m_timeElapsed;
        }

        internal int getNumberOfCoins()
        {
            return m_numberOfCoins;
        }

        internal int getRecordCoins()
        {
            return m_recordCoins;
        }

        internal int getScore()
        {
            return m_score;
        }

        internal int getRecordScore()
        {
            return m_recordScore;
        }

        internal float getAccumulatedHeight()
        {
            return m_accumulatedHeight;
        }

        internal bool getClearedAllLevels()
        {
            return m_allLevelsCleared;
        }
    }
}
