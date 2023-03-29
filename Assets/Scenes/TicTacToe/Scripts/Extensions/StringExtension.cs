using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static string Replace(this string str, int startIndex, int lenght, string newStr)
    {
        if (lenght< 0) {
            throw new ArgumentOutOfRangeException("lenght", lenght, "Must be zero or positive value");
        }

        return str.Substring(0, startIndex) +
            newStr +
            str.Substring(startIndex + lenght);
    }
}
