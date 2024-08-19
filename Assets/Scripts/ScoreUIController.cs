using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    public GameManager gm;
    public bool isPlayer1;

    TextMeshProUGUI tmp;
    int lastScore;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        lastScore = GetPlayerScore();
        tmp.text = lastScore.ToString();
    }

    void Update()
    {
        int score = GetPlayerScore();
        if (score != lastScore)
            tmp.text = score.ToString();
    }

    int GetPlayerScore()
    {
        return isPlayer1 ? gm.GetPlayer1Score() : gm.GetPlayer2Score(); ;
    }
}
