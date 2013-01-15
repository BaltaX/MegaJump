using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.View
{
    class Camera
    {

        internal Vector2 translateCoordinates(Vector2 a_modelPosition, int a_windowHeight, int a_windowWidth, float a_modelDisplacementY)
        {
            //Variables used for translating model coordinates to view coordinates
            
            float scaleX = (float)a_windowWidth/10;//dividera med 10
            float scaleY = (float)a_windowHeight/25;//dividera med 25

            //Calculation of View coordinates
            int viewX = (int)(a_modelPosition.X * scaleX);
            int viewY = (int)((a_modelPosition.Y-a_modelDisplacementY)*scaleY) ;

            return new Vector2(viewX, viewY);

        }
    }
}
