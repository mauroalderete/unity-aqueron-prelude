using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [SerializeField] private GlobalConfig config;

    private int idx = 0;
    private GameObject enemy;

    void Start()
    {
        idx = 0;
        UpdateSelection();
    }

    public void OnPlayClick()
    {
        var anim = enemy.GetComponent<Animator>();
        anim.SetBool("hitted", true);
    }

    public void OnPreviousClick()
    {
        if (config.EnemiesConfig.EnemyPrefabs.Count == 0) { return; }

        idx--;
        if (idx < 0)
        {
            idx = config.EnemiesConfig.EnemyPrefabs.Count - 1;
        }

        UpdateSelection();
    }

    public void OnNextClick()
    {
        if (config.EnemiesConfig.EnemyPrefabs.Count == 0) { return; }

        idx++;
        if (idx > config.EnemiesConfig.EnemyPrefabs.Count - 1)
        {
            idx = 0;
        }
        
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        config.EnemiesConfig.Selected = config.EnemiesConfig.EnemyPrefabs[idx];
        if(enemy!= null)
        {
            Destroy(enemy);
        }

        enemy = Instantiate(config.EnemiesConfig.Selected, transform);
    }
}
