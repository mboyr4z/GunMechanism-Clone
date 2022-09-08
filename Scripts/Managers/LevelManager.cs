using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        createLevel();
    }
    private void createLevel()
    {
        DestroyLevel();
        Level curLevel = (Level)Resources.Load("Level" + ((gameManager.Level % GetTotalLevelCount()) + 1) .ToString());
        Instantiate(curLevel.levelPrefab);
    }

 

    private void DestroyLevel()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private int GetTotalLevelCount()
    {
        int count;

        for (count = 1; ; count++)
        {
            if (Resources.Load("Level" + count))
            {
            }
            else
            {
                break;
            }

        }
        return count - 1;
    }
}
