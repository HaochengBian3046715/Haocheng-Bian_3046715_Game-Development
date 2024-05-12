using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI txtScore;    
    [SerializeField] TextMeshProUGUI txtTotLife;
   



    static int LevelIndex = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

      
     

    }


    void Start()
    {
        //InitLevel();
        InitLevel();
    }


    private void Update()
    {
        
    }

    public void InitLevel()
    {
        currentScore = 0;
        txtScore.text = "Score: " + currentScore.ToString();

        currentLife = 3;
        txtTotLife.text = "0" + currentLife.ToString();
    }
    

    public void UpdateLife(int life)
    {
        txtTotLife.text = life.ToString();
    }
  

    int currentScore = 0;
    public void UpdateScore()
    {
        int s = Random.Range(5, 16);
        currentScore += s;
        txtScore.text = "Score: " + currentScore.ToString();
    }

    int currentLife = 3;
    

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

  

    public void Exit()
    {
        Application.Quit();
    }

    public void Paused()
    {
        Time.timeScale = 0;
     

    }
    public void Resume()
    {
        Time.timeScale = 1;
        
    }
}
