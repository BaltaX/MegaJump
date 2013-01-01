using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.View
{
    class Camera
    {

        internal Vector2 translateCoordinates(Vector2 a_modelPosition, int a_windowHeight, int a_windowWidth)
        {
            //Variables used for translating model coordinates to view coordinates
            int displacementX = 0;
            int displacementY = 0;
            float scaleX = (float)a_windowWidth;
            float scaleY = (float)a_windowHeight;

            //Calculation of View coordinates
            int viewX = (int)(a_modelPosition.X * scaleX) + displacementX;
            int viewY = (int)(a_modelPosition.Y * scaleY) + displacementY;

            return new Vector2(viewX, viewY);

        }
    }
}
