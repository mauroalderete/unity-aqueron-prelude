using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decision
{
    public class DecisionMove : IDecisionMove
    {
        public int Cell { get; private set; }

        public DecisionMove(int cell)
        {
            Cell = cell;
        }
    }

}
