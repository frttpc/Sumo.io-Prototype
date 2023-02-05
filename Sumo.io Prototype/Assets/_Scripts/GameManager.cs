using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public event Action OnWin;
    public event Action OnLose;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnLose += ChangeTimeScale;
        OnWin += ChangeTimeScale;
        ChangeTimeScale();
    }

    //When game is ended choose which event to invoke
    public void GameIsEnded(int code)
    {
        switch (code)
        {
            case 0:
                OnLose?.Invoke();
                break;
            case 1:
                OnWin?.Invoke();
                break;
            case 2:
                if (AIManager.Instance.GetMostPointsOfAI() > player.GetPoints())
                    OnLose?.Invoke();
                else
                    OnWin?.Invoke();
                break;
        }
    }

    //Stop and resume timeScale
    public void ChangeTimeScale()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    //Restart scene
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        ChangeTimeScale();
    }
}
