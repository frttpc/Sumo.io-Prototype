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
        else
            GameManager.Instance.GameIsEnded(3);
    }

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

    public void IncreasePoints(int amount)
    {
        int currentPoints = int.Parse(points.text);
        currentPoints += amount;
        points.text = currentPoints.ToString();
    }

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
