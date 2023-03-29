using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Utils
{
    public static class DialogueCurator
    {
        public static string Curate(string message)
        {
            string result = message;

            result = result.TrimStart(' ');
            result = result.TrimEnd(' ');
            result = result.Replace("�", string.Empty);
            result = result.Replace("�", string.Empty);
            result = result.Replace('�', 'a');
            result = result.Replace('�', 'e');
            result = result.Replace('�', 'i');
            result = result.Replace('�', 'o');
            result = result.Replace('�', 'u');
            result = result.Replace('�', 'n');
            result = result.Replace('�', 'A');
            result = result.Replace('�', 'E');
            result = result.Replace('�', 'I');
            result = result.Replace('�', 'O');
            result = result.Replace('�', 'U');
            result = result.Replace('�', 'N');

            return result;
        }
    }
}
