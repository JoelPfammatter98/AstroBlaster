using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void IncreaseScore() {
        score = score + 5;
        UIManager.Instance.UpdateScore(score);
    }

    public int CalculateFinalScore() {
        int FinalScore = FuelManager.Instance.GetFuel() + score;
        return FinalScore;
    }
}
