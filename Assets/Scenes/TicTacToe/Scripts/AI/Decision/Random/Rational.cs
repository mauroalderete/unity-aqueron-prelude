using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Decision.Random
{
    public class Rational : IRandom
    {
        static readonly int[] centerIndex = { 4 };
        static readonly int[] sideIndex = { 1, 3, 5, 7 };
        static readonly int[] cornerIndex = { 0, 2, 6, 8 };

        public float CenterRate { get; private set; }
        public float SideRate { get; private set; }
        public float CornerRate { get; private set; }

        public Rational(float centerRate, float sideRate, float cornerRate)
        {
            CenterRate = centerRate;
            SideRate = sideRate;
            CornerRate = cornerRate;
        }

        public IDecisionMove GetMove(string gameState)
        {
            Dictionary<int, RangeInt> freeCells = new Dictionary<int, RangeInt>();

            int k = 0;
            MatchCollection matches = Regex.Matches(gameState, @"\.");
            foreach (Match match in matches)
            {
                int rate = 0;
                if ( centerIndex.Contains(match.Index) )
                {
                    rate = (int)Math.Round(CenterRate * 100);
                }

                if ( sideIndex.Contains(match.Index))
                {
                    rate = (int)Math.Round(SideRate * 100);
                }

                if (cornerIndex.Contains(match.Index))
                {
                    rate = (int)Math.Round(CornerRate * 100);
                }

                freeCells.Add(match.Index, new RangeInt(k, rate));
                k += rate;
            }

            float rand = UnityEngine.Random.Range(0, k);

            int cell = 0;
            foreach( KeyValuePair<int, RangeInt> freeCell in freeCells)
            {
                if ( rand >= freeCell.Value.start && rand <= freeCell.Value.start + freeCell.Value.length )
                {
                    cell = freeCell.Key;
                    break;
                }
            }

            return new DecisionMove(cell);
        }
    }

}
