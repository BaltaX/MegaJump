  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaJump.Model
{
    class Level
    {
        Random random = new Random();
        public enum Tile
        {
            T_COIN,
            T_STAR,
            T_WEIGHT,
            T_EMPTY
        };

        //Width and height
        internal const int levelWidth = 10;
        internal const int levelHeight = 400;

        //The array of tiles
        internal Tile[,] m_tiles = new Tile[levelWidth, levelHeight];

        internal Level()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    
                    int tileAttValja;
                    tileAttValja = (int)((float)random.NextDouble() * 20.0f);

                    if (tileAttValja == 0)
                        m_tiles[x, y] = Tile.T_COIN;
                    else if (tileAttValja == 1) m_tiles[x, y] = Tile.T_STAR;
                    else m_tiles[x, y] = Tile.T_EMPTY;
                }
            
            }
        }

        public Tile[,] getTiles()
        {
            return m_tiles;
        }

        public int getlevelWidth()
        {
        return levelWidth;
        }

        public int getlevelHeight()
        {
            return levelHeight;
        }
    }
}
