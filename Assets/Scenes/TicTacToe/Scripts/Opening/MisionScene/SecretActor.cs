using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.MisionScene
{
    public class SecretActor : MonoBehaviour
    {
        public event EventHandler Showed;

        void OnShowed()
        {
            if (Showed == null) { return; }
            Showed(this, EventArgs.Empty);
        }
    }

}
