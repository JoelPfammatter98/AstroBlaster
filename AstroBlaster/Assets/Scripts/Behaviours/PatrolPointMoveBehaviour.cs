using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointMoveBehaviour : MonoBehaviour
{
    [SerializeField] private List<Vector3> targetPoints = new List<Vector3> { new Vector3(-5, 0, 10), new Vector3(0, 0, 10), new Vector3(5, 5, 10) };
    [SerializeField] private float speed = 3f;
    [SerializeField] private Boolean offsetByStartingPosition = false;
    private Vector3 startingOffset = Vector3.zero;

    private Int32 nextPointIndex = 0;

    private void Start()
    {
        if (this.offsetByStartingPosition)
        {
            this.startingOffset = this.gameObject.transform.position;
        }
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, this.GetNextPoint()) < 0.1)
        {
            this.nextPointIndex++;
        }
        this.transform.Translate(this.GetMove(this.GetNextPoint(), this.speed), Space.World);
    }

    private Vector3 GetNextPoint()
    {
        return this.targetPoints[this.nextPointIndex % this.targetPoints.Count] + this.startingOffset;
    }

    private Vector3 GetMove(Vector3 target, float speed)
    {
        return (target - this.transform.position).normalized * speed * Time.deltaTime;
    }
}
