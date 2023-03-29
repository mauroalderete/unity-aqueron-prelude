using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.FallScene
{
    public class FallActor : MonoBehaviour
    {
        public event EventHandler Falled;

        private void OnFall()
        {
            if (Falled == null) { return; }
            Falled(this, EventArgs.Empty);
        }
    }

}
