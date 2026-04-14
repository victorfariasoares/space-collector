using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD durante o jogo")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    [Header("Painel End Game")]
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameTitleText; // ← novo!
    public TextMeshProUGUI finalTimeText;
    public TextMeshProUGUI finalScoreText;

    private void Start()
    {
        int total = GameObject.FindGameObjectsWithTag("Coletavel").Length;
        GameController.Init(total);
        endGamePanel.SetActive(false);
    }

    private void Update()
    {
        GameController.UpdateTimer(Time.deltaTime);

        int minutos = Mathf.FloorToInt(GameController.Timer / 60f);
        int segundos = Mathf.FloorToInt(GameController.Timer % 60f);
        timerText.text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
        scoreText.text = "Score: " + GameController.Score;
        livesText.text = "Vidas: " + GameController.Lives;

        if (GameController.gameOver && !endGamePanel.activeSelf)
        {
            endGamePanel.SetActive(true);
            int m = Mathf.FloorToInt(GameController.Timer / 60f);
            int s = Mathf.FloorToInt(GameController.Timer % 60f);
            finalTimeText.text = "Tempo: " + m.ToString("00") + ":" + s.ToString("00");
            finalScoreText.text = "Score: " + GameController.Score;

            // Muda o título conforme resultado
            if (endGameTitleText != null)
                endGameTitleText.text = GameController.venceu ? "Missão Cumprida!" : "Game Over";
        }
    }
}