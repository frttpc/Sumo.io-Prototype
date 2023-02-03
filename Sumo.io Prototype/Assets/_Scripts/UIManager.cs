using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI enemies;
    [SerializeField] private Image pausePlayButton;

    [Space]
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private float totalTime;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {


    }

    public void ChangeLogo()
    {
        if (pausePlayButton.sprite == pauseIcon)
            pausePlayButton.sprite = playIcon;
        else
            pausePlayButton.sprite = pauseIcon;
    }

    public void IncreasePoints(int amount)
    {
        int currentPoints = int.Parse(points.text);
        currentPoints += amount;
        points.text = currentPoints.ToString();
    }
}
