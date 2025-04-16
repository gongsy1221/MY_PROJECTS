using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private PlayerStep playerStep;
    private TextMeshProUGUI currentScoreText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI bestScoreText;

    private int score = 0;
    private int currentScore = 0;
    private int bestScore = 0;
    private int distance = 1;
    private int multiply = 1;

    private void OnEnable()
    {
        playerStep.StepAction += AddDistance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        playerStep = FindObjectOfType<PlayerStep>();

        currentScoreText = GameObject.Find("CurrentScoretext").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score/ScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = GameObject.Find("BestScore/BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        score = 0;
        currentScore = 0;

        currentScoreText.text = $"Step  :  {currentScore.ToString("D3")}";
    }


    public void AddDistance()
    {
        score += distance * multiply;
        currentScore = score;
        currentScoreText.text = $"Step  :  {currentScore.ToString("D3")}";
    }

    public void CheckBestScore()
    {
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
    }

    public void SetGameOver()
    {
        scoreText.text = currentScore.ToString();
        CheckBestScore();
        bestScoreText.text = bestScore.ToString();
    }

    private void OnDestroy()
    {
        playerStep.StepAction -= AddDistance;
    }
}
