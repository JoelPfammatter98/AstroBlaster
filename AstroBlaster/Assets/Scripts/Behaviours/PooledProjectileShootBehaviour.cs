using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledProjectileShootBehaviour : MonoBehaviour
{
    [SerializeField] private PooledObjectBehaviour projectilePool;
    [SerializeField] private Vector3 projectileMove = Vector3.forward * 10;
    [SerializeField] private Transform projectileOrigin;
    public UnityEvent OnShoot = new UnityEvent();

    void Start()
    {
        if (projectilePool == null) { Debug.LogWarning($"No projectile pool provided, {nameof(PooledProjectileShootBehaviour)} will instantiate objects."); }
        if (projectileOrigin == null) { projectileOrigin = this.gameObject.transform; }
    }

    public void Shoot()
     {
        var projectileRigidBody = this.InstantiateProjectile()?.GetComponent<Rigidbody>();
        if (projectileRigidBody == null)
        { 
            Debug.LogError($"{nameof(PooledProjectileShootBehaviour)} pooled object missing or lacks {nameof(Rigidbody)} component, projectile will not move.");
        }
        else
        {
            this.OnShoot.Invoke();

            // return object to pool if it got hit, but unfortunately we can't guarantee an event is present (doesnt unity provide one?)
            projectileRigidBody.gameObject.GetComponent<HittableBehaviour>()?.OnHit.AddListener(() => this.projectilePool.Release(projectileRigidBody.gameObject));

            projectileRigidBody.transform.position = this.projectileOrigin.position;
            projectileRigidBody.transform.rotation = this.projectileOrigin.rotation;
            projectileRigidBody.velocity = Vector3.zero;
            projectileRigidBody.velocity = Vector3.zero;
            projectileRigidBody.AddForce(this.projectileMove, ForceMode.Impulse);
        }
    }

    private GameObject InstantiateProjectile()
    {
        return this.projectilePool?.ClaimPooledObject();
    }
}
