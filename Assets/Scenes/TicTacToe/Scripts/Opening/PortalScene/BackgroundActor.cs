using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.PortalScene
{
    public class BackgroundActor : MonoBehaviour
    {
        public event EventHandler Arrived;

        void OnArrived()
        {
            if (Arrived == null) { return; }
            Arrived(this, EventArgs.Empty);
        }
    }

}
