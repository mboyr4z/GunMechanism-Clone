using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    private VaseManager vaseManager;

    private BulletManager bulletManager;

    private CanvasManager canvasManager;

    private GameState state;

    private int level;

    private bool isStopGame;

    void Start()
    {

        Physics.gravity = new Vector3(0,-9.8f,0);

        IsStopGame = true;

        SetState(GameState.InWaitPanel);

        vaseManager = VaseManager.instance;
        bulletManager = BulletManager.instance;
        canvasManager = CanvasManager.instance;
    }

    public int Level
    {
        get
        {

            return PlayerPrefs.GetInt(PlayerPreffs.Level.ToString());
        }
    }

    public bool IsStopGame { get => isStopGame; set => isStopGame = value; }

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt(PlayerPreffs.Level.ToString(), Level + 1);
    }

    public GameState GetState()
    {
        return state;
    }
    public void SetState(GameState value)
    {
        state = value;
    }

    


    public void CheckIsFinishGame()
    {
        StartCoroutine(CheckIsFinishGame(1f));
    }

    IEnumerator CheckIsFinishGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        int leftVaseCount = vaseManager.GetVaseCount();
        bool isThereBulletInMag = bulletManager.IsThereBulletInMag();

        if (leftVaseCount == 0)
        {
            IncreaseLevel();

            canvasManager.OpenMenu("winGamePanel");

            IsStopGame = true;
            Time.timeScale = 0;

            Actions.act_winLevel?.Invoke();

        }
        else if (!isThereBulletInMag)
        {
            canvasManager.OpenMenu("loseGamePanel");

            IsStopGame = true;
            Time.timeScale = 0;

            Actions.act_loseLevel?.Invoke();
        }
    }

}

public enum GameState
{
    InWaitPanel,
    LiftUpMagazine,
    PutBullets,
    DragMagazine,
    DragSlice,
    Shoot,
    InLosePanel,
    InWinPanel
}

public enum PlayerPreffs
{
    Level
}
