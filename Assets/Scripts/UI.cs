using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Transform startUI;
    [SerializeField] private Transform gameUI;
    [SerializeField] private Transform restartUI;

    public void GameStartUIEnable(bool enable)
    {
        startUI.gameObject.SetActive(enable);
        gameUI.gameObject.SetActive(false);
        restartUI.gameObject.SetActive(false);
    }

    public void GameUIEnable(bool enable)
    {
        startUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(enable);
        restartUI.gameObject.SetActive(false);
    }

    public void GameRestartUIEnable(bool enable)
    {
        startUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        restartUI.gameObject.SetActive(enable);
    }
}
