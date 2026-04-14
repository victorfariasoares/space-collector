using UnityEngine;

public static class GameController
{
    private static int collectableCount;
    private static float timer;
    private static bool timerRunning;
    private static int score;
    private static int playerLives;
    private static bool initialized = false;
    private static int totalColetaveis;

    public static bool gameOver { get; private set; }
    public static bool venceu { get; private set; }
    public static float Timer => timer;
    public static int Score => score;
    public static int Lives => playerLives;

    public static void Init(int total)
    {
        totalColetaveis = total;
        collectableCount = total;
        timer = 0f;
        timerRunning = true;
        score = 0;
        playerLives = 3;
        gameOver = false;
        venceu = false;
        initialized = true;
    }

    public static void EnsureInit()
    {
        if (!initialized) Init(6);
    }

    public static void UpdateTimer(float deltaTime)
    {
        if (timerRunning && !gameOver)
            timer += deltaTime;
    }

    public static void Collect()
    {
        collectableCount--;
        score += 100;
        if (collectableCount <= 0)
            collectableCount = totalColetaveis;
    }

    public static void LoseLife()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            timerRunning = false;
            venceu = false;
            gameOver = true;
            SalvarScore(score); // ← salva o score ao perder
        }
    }

    // Salva top 5 scores usando PlayerPrefs
    public static void SalvarScore(int novoScore)
    {
        int[] scores = GetScores();
        scores[4] = novoScore;
        System.Array.Sort(scores);
        System.Array.Reverse(scores);
        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        PlayerPrefs.Save();
    }

    public static int[] GetScores()
    {
        int[] scores = new int[5];
        for (int i = 0; i < 5; i++)
            scores[i] = PlayerPrefs.GetInt("Score" + i, 0);
        return scores;
    }
}