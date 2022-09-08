using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoseGamePanel : MonoBehaviour
{
    [SerializeField] private Button btn_restart;

    [SerializeField] private Text levelText;

    private GameManager gameManager;
    private void Start()
    {
        btn_restart.onClick.AddListener(()=> {
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
        levelText.text = "Level "+ (gameManager.Level + 1).ToString();
    }




}
