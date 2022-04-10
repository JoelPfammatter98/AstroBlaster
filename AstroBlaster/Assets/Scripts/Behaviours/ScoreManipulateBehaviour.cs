using System;
using UnityEngine;

[RequireComponent(typeof(SingletonReferenceProviderBehaviour))]
public class ScoreManipulateBehaviour : MonoBehaviour
{
    private ScoreManager scoreManager;

    public void Start()
    {
        this.scoreManager = GetComponent<SingletonReferenceProviderBehaviour>().ScoreManager;
    }

    public void AddScore(Int32 amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.scoreManager.IncreaseScore();
        }
    }
}
