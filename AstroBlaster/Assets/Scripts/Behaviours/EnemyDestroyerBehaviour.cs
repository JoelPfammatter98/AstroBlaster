using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SingletonReferenceProviderBehaviour))]
public class EnemyDestroyerBehaviour : MonoBehaviour, IEnemyDestroyer
{
    private SingletonReferenceProviderBehaviour singletonReferenceProviderBehaviour;

    void Start()
    {
        this.singletonReferenceProviderBehaviour = this.GetComponent<SingletonReferenceProviderBehaviour>();
    }

    public void DestroyEnemy(GameObject enemy)
    {
        this.singletonReferenceProviderBehaviour.TargetController.DestroyEnemy(enemy);
    }
}