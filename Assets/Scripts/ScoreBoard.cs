using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score;
    Text scoreTxt;

    const string scoreHeader = "score\n";

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = GetComponent<Text>();
        SetScoreTxt();
    }

    /// <summary>
    /// Called by string reference
    /// </summary>
    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        SetScoreTxt();
    }

    private void SetScoreTxt()
    {
        scoreTxt.text = scoreHeader + score.ToString();
    }
}
