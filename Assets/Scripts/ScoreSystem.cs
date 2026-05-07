using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    [Header("Score")]
    public int score = 0;
    public Text scoreText;

    [Header("Lives")]
    public int lives = 4;
    public Text livesText;

    public GameObject gameOverPanel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void RemoveLife(int amount)
    {
        lives -= amount;

        if (lives <= 0)
        {
            lives = 0;
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        livesText.text = "Lives : " + lives;
    }
}
