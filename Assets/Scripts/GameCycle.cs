using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Game,
    Restart
}

public class GameCycle : MonoBehaviour
{
    public GameState State;

    private UI myUI;

    private void Start()
    {
        Initing();
    }

    private void Initing()
    {
        myUI = FindObjectOfType<UI>();
        State = GameState.Start;
    }

    private void GameCycleUpdate()
    {
        if (Input.GetMouseButtonDown(0) && State == GameState.Start)
        {
            State = GameState.Game;
        }
        switch (State)
        {
            case GameState.Start:
                myUI.GameStartUIEnable(true);
                break;
            case GameState.Game:
                myUI.GameUIEnable(true);
                break;

            case GameState.Restart:
                myUI.GameRestartUIEnable(true);
                break;
        }
    }

    private void Update()
    {
        GameCycleUpdate();
    }

    public void GameRestart()
    {
        SetPause(false); // play game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
