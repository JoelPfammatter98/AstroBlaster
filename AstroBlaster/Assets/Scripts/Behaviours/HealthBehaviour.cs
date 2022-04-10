using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private Int32 initialHealthAmount = 100;
    private Int32 currentHealthAmount;
    [SerializeField] private Int32 maxHealthAmount = 100;
    [SerializeField] private Int32 minHealthAmount = 0;
    [SerializeField] private UnityEvent OnHealthReachesMinimum = new UnityEvent();
    [SerializeField] private UnityEvent OnHealthReachesMaximum = new UnityEvent();
    [SerializeField] private UnityEvent<int> OnHealthUpdate = new UnityEvent<int>();

    public void Start()
    {
        this.currentHealthAmount = this.initialHealthAmount;
    }

    public void IncrementHealth()
    {
        if (this.currentHealthAmount >= maxHealthAmount)
        {
            return;
        }
        this.OnHealthUpdate.Invoke(++this.currentHealthAmount);
        if (this.currentHealthAmount == maxHealthAmount)
        {
            this.OnHealthReachesMaximum.Invoke();
        }
    }

    public void DecrementHealth()
    {
        if (this.currentHealthAmount <= minHealthAmount)
        {
            return;
        }
        this.OnHealthUpdate.Invoke(--this.currentHealthAmount);
        if (this.currentHealthAmount == minHealthAmount)
        {
            this.OnHealthReachesMinimum.Invoke();
        }
    }
}
