using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemiesConfig
{
    public List<GameObject> EnemyPrefabs;
    public GameObject Selected;
}

[CreateAssetMenu(fileName = "GlobalConfigData", menuName = "Configurations/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    public const int MAX_TURNS = 5;
    public bool PlayWithCross;
    public EnemiesConfig EnemiesConfig;

    public GlobalConfig()
    {
        Default();
    }

    private void Awake()
    {
        Default();
    }

    private void Default()
    {
        PlayWithCross = true;
    }
}
