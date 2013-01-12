using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MegaJump.Controller
{
    class IMGui
    {
        SpriteBatch m_spriteBatch;
        ContentManager m_content;
        MouseState m_oldMouseState;
        SpriteFont m_spriteFont;
        Texture2D m_texture;
        

        
        public IMGui(Microsoft.Xna.Framework.Content.ContentManager a_content, GraphicsDevice a_graphicsDevice)
        {
            // TODO: Complete member initialization
            m_content = a_content;
            m_spriteBatch = new SpriteBatch(a_graphicsDevice);
            m_spriteFont = a_content.Load<SpriteFont>("Courier New");
            m_texture = a_content.Load<Texture2D>("button");
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
    }
}
