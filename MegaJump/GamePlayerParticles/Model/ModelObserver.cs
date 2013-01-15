using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MegaJump.Model
{
    interface IModelObserver
    {
        void CollisionPlayerCoin();

        void GameEnd();

        //void CollisionBomb(); ****DELETED

        void DrawAnnouncement(bool p);

        void CollisionBrick();

        void LevelAnnouncement(int p);

        void CollisionBomb(Vector2 vector2);
    }
}
