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
        Texture2D m_texture;
        MegaJump.Model.MainModel m_mainModel;
        

        
        public IMGui(Microsoft.Xna.Framework.Content.ContentManager a_content, GraphicsDevice a_graphicsDevice, MegaJump.Model.MainModel a_mainmodel)
        {
            // TODO: Complete member initialization
            m_content = a_content;
            m_spriteBatch = new SpriteBatch(a_graphicsDevice);
            m_spriteFont = a_content.Load<SpriteFont>("Courier New");
            m_texture = a_content.Load<Texture2D>("button");
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

            //Position text
            Vector2 position = new Vector2(a_centerPosX - 150, a_centerPosY - 15);

            //Position button
            Rectangle destinationRectangle = new Rectangle(a_centerPosX - 150, a_centerPosY - 15, 300, 30);

            //Generate buttons
            m_spriteBatch.Begin();

            
            m_spriteBatch.Draw(m_texture, destinationRectangle, Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, a_text, position, Color.LightGray);

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
            m_spriteBatch.DrawString(m_spriteFont, "Your height was: "+String.Format("{0:0.00}",m_mainModel.getMaxHeight()).ToString(), new Vector2(100f,100f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Record height is: " + String.Format("{0:0.00}",m_mainModel.getRecordHeight()).ToString(), new Vector2(100f, 140f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Time: " + String.Format("{0:0.00}",m_mainModel.getElapsedTime()).ToString()+" seconds", new Vector2(100f, 180f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Collected coins: " + m_mainModel.getNumberOfCoins(), new Vector2(100f, 220f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Record collected coins: " + m_mainModel.getRecordCoins(), new Vector2(100f, 260f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Score: " + m_mainModel.getScore(), new Vector2(100f, 300f), Color.LightGray);
            m_spriteBatch.DrawString(m_spriteFont, "Record: " + m_mainModel.getRecordScore(), new Vector2(100f, 340f), Color.LightGray);


            m_spriteBatch.End();

        }
    }
}
