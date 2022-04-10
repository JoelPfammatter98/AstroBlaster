using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetController : MonoBehaviour, IEnemyDestroyer
{
    [SerializeField] private Int32 enemyAmount = 10;
    [SerializeField] private PooledObjectBehaviour pooledTargetObject;
    [SerializeField] private Vector3 spawnPositionOffset = Vector3.zero;
    [SerializeField] private Quaternion spawnRotationOffset = Quaternion.identity;
    [SerializeField] private Transform spawnOrigin;
    [SerializeField] private UnityEvent OnAllTargetsKilled = new UnityEvent();
    private bool placedTargets;
    private List<GameObject> targets = new List<GameObject>();
    private Int32 liveTargets;

    private void Start()
    {
        if (this.spawnOrigin == null) { this.spawnOrigin = this.gameObject.transform; }
        if (this.pooledTargetObject == null) { throw new InvalidOperationException("Can't have target controller without object pool."); }
        this.liveTargets = enemyAmount;
    }

    public void DestroyEnemy(GameObject target)
    {
        this.liveTargets--;
        Debug.Log($"Target destroyed, {this.liveTargets} remaining");
        this.pooledTargetObject.Release(target);
        if (this.liveTargets <= 0)
          {
              this.OnAllTargetsKilled.Invoke();
        }
    }

    private void Update()
    {
        if (!this.placedTargets)
        {
            this.PlaceTargets(enemyAmount);
            this.placedTargets = true;
        }
    }

    public void PlaceTargets(Int32 amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject target = this.pooledTargetObject?.ClaimPooledObject();
            this.targets.Add(target);
            var spawnPoint = this.GetSpawnCoordinates(i);
            target.transform.position = spawnPoint.position;
            target.transform.rotation = spawnPoint.rotation * spawnRotationOffset;
        }
    }

    private (Vector3 position, Quaternion rotation) GetSpawnCoordinates(Int32 offsetMultiplier)
    {
        return (spawnOrigin.position + spawnPositionOffset * offsetMultiplier, spawnOrigin.rotation);
    }
}
