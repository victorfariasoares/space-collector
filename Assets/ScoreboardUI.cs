using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // 5 campos de texto

    public void MostrarScoreboard()
    {
        int[] scores = GameController.GetScores();
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (scores[i] > 0)
                scoreTexts[i].text = (i + 1) + "º  " + scores[i] + " pts";
            else
                scoreTexts[i].text = (i + 1) + "º  ---";
        }
    }
}