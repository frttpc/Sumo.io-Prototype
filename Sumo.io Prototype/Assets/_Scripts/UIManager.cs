using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI enemies;
    [SerializeField] private Image pausePlayButton;
    [SerializeField] private GameObject layer;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Sprite pauseIcon;
    [SerializeField] private Sprite playIcon;
    [SerializeField] private float totalTime;

    private int previousSecond = int.MaxValue;
    private bool isEnded = false;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnLose += LoseScreen;
        GameManager.Instance.OnWin += WinScreen;

        points.text = "0";
        enemies.text = AIManager.Instance.GetAICount().ToString();
    }

    private void Update()
    {
        Timer();
    }

    //Basic Timer, if ended call GameManager
    private void Timer()
    {
        totalTime -= Time.deltaTime;
        if (totalTime >= 0)
        {
            if (previousSecond != (int)totalTime)
            {
                previousSecond = (int)totalTime;
                timer.text = ((int)totalTime).ToString();
            }
        }
        else if (!isEnded)
        {
            GameManager.Instance.GameIsEnded(2);
            isEnded = true;
        }
    }

    //Start Stop Logo Change
    public void ChangeLogo()
    {
        if (pausePlayButton.sprite == pauseIcon)
            pausePlayButton.sprite = playIcon;
        else
            pausePlayButton.sprite = pauseIcon;
    }

    public void DecreaseEnemyCountText(int count)
    {
        enemies.text = count.ToString();
    }

    //Edit points text
    public void IncreasePoints(int amount)
    {
        int currentPoints = int.Parse(points.text);
        currentPoints += amount;
        points.text = currentPoints.ToString();
    }

    //Show Win Screen
    public void WinScreen()
    {
        pausePlayButton.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        enemies.gameObject.SetActive(false);
        layer.SetActive(true);
        winText.SetActive(true);
        restartButton.SetActive(true);
    }

    //Show Lose Screen
    public void LoseScreen()
    {
        pausePlayButton.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        enemies.gameObject.SetActive(false);
        layer.SetActive(true);
        loseText.SetActive(true);
        restartButton.SetActive(true);
    }

}
