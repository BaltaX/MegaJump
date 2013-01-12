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
            T_BRICK,
            T_BOMB,
            T_FOG,
            T_MARBLE,
            T_EMPTY
        };

        //Width and height
        internal const int levelWidth = 10;
        internal const int levelHeight = 500;

        //The array of tiles
        internal Tile[,] m_tiles;
        internal Level(string a_path)
        {
            GenerateLevel(a_path);
        }

        private void GenerateLevel(string a_path)
        {
            //a_path should be like "Content/LevelName.txt")
            //Level text file should be put in Content folder, 
            //setting Build action to None and Copy to output set to Always
            string whole_file = System.IO.File.ReadAllText(a_path);

            //Split into lines
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            //Determine number of rows and columns
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;

            //Allocate 2d array
            string[,] values = new string[num_cols, num_rows];

            //Load array
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[c, r] = line_r[c];
                }
            
            }

            //Create tile array
            m_tiles = new Tile[num_cols, num_rows];
            for (int x = 0; x < num_cols; x++)
            {
                for (int y = 0; y < num_rows; y++)
                {
                    
                    switch(values[x,y])
                    {
                        case  "0": m_tiles[x, y] = Tile.T_COIN;
                            break;
                        case "1": m_tiles[x, y] = Tile.T_STAR;
                            break;
                        case "2": m_tiles[x, y] = Tile.T_WEIGHT;
                            break;
                        case "3": m_tiles[x, y] = Tile.T_BRICK;
                            break;
                        case "4": m_tiles[x, y] = Tile.T_BOMB;
                            break;
                        case "5": m_tiles[x, y] = Tile.T_FOG;
                            break;
                        case "6": m_tiles[x, y] = Tile.T_MARBLE;
                            break;
                        case "7": m_tiles[x, y] = Tile.T_EMPTY;
                            break;


                    }
                }
            }
        }//End of GenerateLevel()

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

        internal void Initialize(string a_stringPath)
        {
            GenerateLevel(a_stringPath);
        }
    }
}
