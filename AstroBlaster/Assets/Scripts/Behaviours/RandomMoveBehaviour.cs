using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMoveBehaviour : MonoBehaviour
{
    [SerializeField] private Int32 distancePerSecond = 10;
    [SerializeField] private Boolean FixXAxis = false;
    [SerializeField] private Boolean FixYAxis = false;
    [SerializeField] private Boolean FixZAxis = false;
    [SerializeField] private float changeDirectionIntervalSeconds = 2;

    private float timeOfLastDirectionChange = 0;

    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        this.direction = this.GetRandomDirection();
    }

    private void Update()
    {
        if (this.WillChangeDirection()) { 
            this.direction = GetRandomDirection();
            this.timeOfLastDirectionChange = Time.realtimeSinceStartup;
        }
        this.transform.Translate(this.direction * distancePerSecond * Time.deltaTime);
    }

    private bool WillChangeDirection()
    {
        return (Time.realtimeSinceStartup - this.timeOfLastDirectionChange) > changeDirectionIntervalSeconds;
    }

    private Vector3 GetRandomDirection()
    {
        var directions = new List<Vector3>();
        if (!FixXAxis) directions.AddRange(new[] { Vector3.right, Vector3.left });
        if (!FixYAxis) directions.AddRange(new[] { Vector3.up, Vector3.down });
        if (!FixZAxis) directions.AddRange(new[] { Vector3.forward, Vector3.back });

        if (!directions.Any()) { return Vector3.zero; }

        var random = UnityEngine.Random.Range(0, directions.Count);
        return directions[random];
    }
}
