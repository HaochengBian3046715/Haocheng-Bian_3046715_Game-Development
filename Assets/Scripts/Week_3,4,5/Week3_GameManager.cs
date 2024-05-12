using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week3_GameManager : MonoBehaviour
{
    public static Week3_GameManager Instance { get; private set; }
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore()
    {
        UiManager.Instance.UpdateScore();
    }

    public void Life(int life)
    {
        UiManager.Instance.UpdateLife(life);
        if (life <= 0)
            RestartLevel();
    }

    public void RestartLevel()
    {
        UiManager.Instance.RestartScene();
    }
}
