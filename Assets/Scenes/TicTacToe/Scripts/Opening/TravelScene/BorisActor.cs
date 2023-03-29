using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.TravelScene
{
    public class BorisActor : MonoBehaviour
    {
        public event EventHandler Outted;

        void OnOutted()
        {
            if (Outted == null) { return; }
            Outted(this, EventArgs.Empty);
        }
    }

}