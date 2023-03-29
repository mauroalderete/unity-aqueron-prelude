using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TestTools;

public class MinimaxTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void SimpleExecuteFullPasses()
    {
        // string gameState = "xoxox....";

        // Assert.AreEqual( true, resp.cell == 6 || resp.cell== 8, $"reps.cell is {resp.cell}" );
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MinimaxTestsWithEnumeratorPasses()
    {
        // string gameState = "o...xx.ox";

        // Assert.AreEqual(true, resp.cell == 2 || resp.cell == 3, $"reps.cell is {resp.cell}");

        yield return null;
    }
}
