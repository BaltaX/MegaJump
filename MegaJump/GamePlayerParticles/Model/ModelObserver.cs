using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaJump.Model
{
    interface IModelObserver
    {
        void CollisionPlayerCoin();

        void GameEnd();

        void CollisionBomb();

        void DrawAnnouncement(bool p);
    }
}
