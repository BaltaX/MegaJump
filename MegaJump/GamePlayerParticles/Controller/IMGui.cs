using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MegaJump.Model;

namespace MegaJump.Controller
{
    class IMGui
    {
        SpriteBatch m_spriteBatch;
        ContentManager m_content;
        MouseState m_oldMouseState;
        SpriteFont m_spriteFont;
        SpriteFont m_menuFont;
        Texture2D m_texture;
        Texture2D m_backGround;
        MegaJump.Model.MainModel m_mainModel;
        

        
        public IMGui(Microsoft.Xna.Framework.Content.ContentManager a_content, GraphicsDevice a_graphicsDevice, MegaJump.Model.MainModel a_mainmodel)
        {
            // TODO: Complete member initialization
            m_content = a_content;
            m_spriteBatch = new SpriteBatch(a_graphicsDevice);
            m_spriteFont = a_content.Load<SpriteFont>("Courier New");
            m_menuFont = a_content.Load<SpriteFont>("MenuButtons");
            m_texture = a_content.Load<Texture2D>("button");
            m_backGround = a_content.Load<Texture2D>("MenuBackground");
            m_mainModel = a_mainmodel;
        }

        internal bool doButton(Microsoft.Xna.Framework.Input.MouseState a_mouseState, string a_text, int a_centerPosX, int a_centerPosY)
        {
            bool mouseOver = false;
            bool wasClicked = false;

            //If mouseover
            if ((a_centerPosX - a_mouseState.X) * (a_centerPosX - a_mouseState.X) < 300 * 300 && (a_centerPosY - a_mouseState.Y) * (a_centerPosY - a_mouseState.Y) < 30 * 30)
            {
                mouseOver = true;
                wasClicked = false;
                //If we click
                if (a_mouseState.LeftButton == ButtonState.Released && m_oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    wasClicked = true;
                    m_oldMouseState = a_mouseState;
                }
            }

            //How many characters in text?
            int numberOfCharacters = 0;
            foreach (char c in a_text)
            {
                if (char.IsLetter(c))
                {
                    numberOfCharacters++;
                }
            }


            //Position text
            Vector2 position = new Vector2(a_centerPosX+40-(numberOfCharacters*10), a_centerPosY );

            //Position button
            Rectangle destinationRectangle = new Rectangle(a_centerPosX - 150, a_centerPosY - 15, 400, 70);

            //Generate buttons
            m_spriteBatch.Begin();

            
            m_spriteBatch.Draw(m_texture, destinationRectangle, Color.LightGray);
            m_spriteBatch.DrawString(m_menuFont, a_text, position, Color.LightGray);

            m_spriteBatch.End();
            
            return wasClicked;
        }

        internal void setOldState(Microsoft.Xna.Framework.Input.MouseState a_mouseState)
        {
            m_oldMouseState = a_mouseState;
        }

        internal void DrawScores()
        {


            m_spriteBatch.Begin();
            //Create destination rectangle for background
            Rectangle backGroundDestRect = new Rectangle(0,0,640,960);

            //Draw background
            m_spriteBatch.Draw(m_backGround, backGroundDestRect, Color.White);

            
            
            
            m_spriteBatch.DrawString(m_spriteFont, m_mainModel.getScore().ToString(), new Vector2(272f, 113f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, m_mainModel.getRecordScore().ToString(), new Vector2(465f, 113f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, m_mainModel.getNumberOfCoins().ToString(), new Vector2(272f, 161f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, m_mainModel.getRecordCoins().ToString(), new Vector2(465f, 161f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, String.Format("{0:0.00}", m_mainModel.getMaxHeight()).ToString() + " m", new Vector2(272f, 209f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, String.Format("{0:0.00}", m_mainModel.getRecordHeight()).ToString() + " m", new Vector2(465f, 209f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, String.Format("{0:0.00}",m_mainModel.getElapsedTime()).ToString()+" s", new Vector2(272f, 257f), Color.LightGray);

            m_spriteBatch.End();

        }
    }
}
