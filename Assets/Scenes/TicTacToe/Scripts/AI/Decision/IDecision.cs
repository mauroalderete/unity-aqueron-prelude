using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decision
{
    public interface IDecisionMove
    {
        public int Cell { get; }
    }


    public interface IDecisionMoveProvider
    {
        IDecisionMove GetMove(string gameState);
    }
}
