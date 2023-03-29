using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class Range<T>
    {
        [SerializeField] public T From;
        [SerializeField] public T To;
    }

}
