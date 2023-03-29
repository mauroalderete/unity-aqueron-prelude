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
            result = result.Replace("¡", string.Empty);
            result = result.Replace("¿", string.Empty);
            result = result.Replace('á', 'a');
            result = result.Replace('é', 'e');
            result = result.Replace('í', 'i');
            result = result.Replace('ó', 'o');
            result = result.Replace('ú', 'u');
            result = result.Replace('ñ', 'n');
            result = result.Replace('Á', 'A');
            result = result.Replace('É', 'E');
            result = result.Replace('Í', 'I');
            result = result.Replace('Ó', 'O');
            result = result.Replace('Ú', 'U');
            result = result.Replace('Ñ', 'N');

            return result;
        }
    }
}
