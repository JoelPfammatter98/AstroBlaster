using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SingletonReferenceProviderBehaviour))]
public class GameOverTriggerBehaviour : MonoBehaviour, IGameOverTrigger
{
    private SingletonReferenceProviderBehaviour singletonReferenceProviderBehaviour;

    void Start()
    {
        this.singletonReferenceProviderBehaviour = this.GetComponent<SingletonReferenceProviderBehaviour>();
    }

    public void GameOver()
    {
        this.singletonReferenceProviderBehaviour.UIManager.GameOver();
    }
}
