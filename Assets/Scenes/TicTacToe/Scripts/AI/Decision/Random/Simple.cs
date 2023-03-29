using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Decision.Random
{
    public class Simple : IRandom
    {
        public IDecisionMove GetMove(string gameState)
        {
            List<int> freeCells = new List<int>();

            MatchCollection matches = Regex.Matches(gameState, @"\.");
            foreach (Match match in matches)
            {
                freeCells.Add(match.Index);
            }

            int cell = freeCells.ElementAt(UnityEngine.Random.Range(0, freeCells.Count));

            return new DecisionMove(cell);
        }
    }
}
