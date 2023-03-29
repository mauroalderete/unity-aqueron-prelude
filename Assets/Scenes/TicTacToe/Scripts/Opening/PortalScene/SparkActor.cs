using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.PortalScene
{
    public class SparkActor : MonoBehaviour
    {
        public event EventHandler Blinked;

        void OnBlinked()
        {
            if (Blinked == null) { return; }
            Blinked(this, EventArgs.Empty);
        }
    }

}
