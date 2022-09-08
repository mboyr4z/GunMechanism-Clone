using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class WinGamePanel : MonoBehaviour
{
    [SerializeField] private Button btn_nextLevel;

    [SerializeField] private Text levelText;

    private GameManager gameManager;

    private void Start()
    {
        btn_nextLevel.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
    }



    private void OnEnable()
    {
        if (gameManager == null)
            gameManager = GameManager.instance;

        SetLevelText();

    }

    private void SetLevelText()
    {
        levelText.text = "Level "+ (gameManager.Level).ToString();
    }

}
