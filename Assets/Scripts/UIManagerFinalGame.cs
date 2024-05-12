using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerFinalGame : MonoBehaviour
{
    public TextMeshProUGUI txtBox;
    int currentScore = 0;
    public GameObject panelWin;
    public GameObject paneLost;
    public static UIManagerFinalGame Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
       
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
        
    }

    public void AddScore()
    {
        currentScore++;
        txtBox.text = "Boxes: " + currentScore.ToString();

        if (currentScore >= 10)
        {
            ShowWinPanel();
        }
    }

    public void ShowWinPanel()
    {
        panelWin.SetActive(true);
    }

    public void ShowFailPanel()
    {
        paneLost.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    
}
