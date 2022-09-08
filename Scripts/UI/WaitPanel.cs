using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaitPanel : MonoBehaviour
{
    [SerializeField] private Text levelText;

    [SerializeField] private Button btn_start;

    private GameManager gameManager;

    private CanvasManager canvasManager;


    private void OnEnable()
    {
        canvasManager = CanvasManager.instance;
        gameManager = GameManager.instance;

        SetLevelText();
    }

    

    private void Start()
    {
        btn_start.onClick.AddListener(StartGame);
    }



    private void StartGame()
    {
        canvasManager.OpenMenu("inGamePanel");
        gameManager.SetState(GameState.LiftUpMagazine);

        Time.timeScale = 1;
        gameManager.IsStopGame = false;

        Actions.act_gameStarted?.Invoke();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + (gameManager.Level + 1).ToString();
    }



}
