using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.PortalScene
{
    public class PortalActor : MonoBehaviour
    {
        public event EventHandler Traversed;

        void OnTraversed()
        {
            if (Traversed == null) { return; }
            Traversed(this, EventArgs.Empty);
        }
    }

}
