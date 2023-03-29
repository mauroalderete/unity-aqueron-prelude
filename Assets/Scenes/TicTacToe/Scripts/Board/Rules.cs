using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class Rules : MonoBehaviour
{
    public enum GameResult
    {
        CrossWin,
        CircleWin,
        Tie,
        Unfinished,
    }

    private static string[] crossVictory = {
        "xxx......",
        "...xxx...",
        "......xxx",
        "x..x..x..",
        ".x..x..x.",
        "..x..x..x",
        "x...x...x",
        "..x.x.x.."};

    private static string[] circleVictory = {
        "ooo......",
        "...ooo...",
        "......ooo",
        "o..o..o..",
        ".o..o..o.",
        "..o..o..o",
        "o...o...o",
        "..o.o.o.."};

    public static bool CrossWin(string gameDatagram)
    {
        return Evaluate(gameDatagram, crossVictory);
    }

    public static bool CircleWin(string gameDatagram)
    {
        return Evaluate(gameDatagram, circleVictory);
    }

    public static bool SomeWinner(string gameDatagram)
    {
        return CrossWin(gameDatagram) || CircleWin(gameDatagram);
    }

    public static bool Tie(string gameDatagram)
    {
        if ( gameDatagram.Count(d => d == '.') == 0 )
        {
            return !SomeWinner(gameDatagram);
        }

        return false;
    }

    public static bool EndGame(string gameDatagram)
    {
        return gameDatagram.Count(d => d == '.') == 0 || SomeWinner(gameDatagram);
    }

    public static GameResult Result(string gameDatagram)
    {
        if ( CrossWin(gameDatagram) )
        {
            return GameResult.CrossWin;
        }

        if ( CircleWin(gameDatagram))
        {
            return GameResult.CircleWin;
        }

        if (Tie(gameDatagram) )
        {
            return GameResult.Tie;
        }

        return GameResult.Unfinished;
    }

    private static bool Evaluate(string gameDatagram, string[] evaluatorPattern)
    {
        foreach (string pattern in evaluatorPattern)
        {
            Match m = Regex.Match(gameDatagram, pattern);
            if (m.Success)
            {
                return true;
            }
        }

        return false;
    }

    public static int Openings(string gameState, string mark)
    {
        int lines = 0;

        gameState = gameState.Replace(".", mark);
        string win = string.Join(string.Empty, mark, mark, mark);

        string[] openings = {
            // files
            gameState.Substring(0, 3),
            gameState.Substring(3, 3),
            gameState.Substring(6),
            // columns
            string.Join(string.Empty, gameState[0], gameState[3], gameState[6]),
            string.Join(string.Empty, gameState[1], gameState[4], gameState[7]),
            string.Join(string.Empty, gameState[2], gameState[5], gameState[8]),
            // diagonals
            string.Join(string.Empty, gameState[0], gameState[4], gameState[8]),
            string.Join(string.Empty, gameState[2], gameState[4], gameState[8]),
        };

        foreach (string line in openings)
        {
            if ( line == win )
            {
                lines++;
            }
        }

        return lines;
    }
}
