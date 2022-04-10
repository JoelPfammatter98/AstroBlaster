using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    private int Fuel = 100;

    private static FuelManager _instance;
    public static FuelManager Instance { get { return _instance; } }

    private void Awake()
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

    private void Start() {
        ManageFuel();
    }

    private void ManageFuel() {
        StartCoroutine(DecreaseFuel(2));
    }
 
    IEnumerator DecreaseFuel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Fuel = Fuel - 10;
        UIManager.Instance.UpdateFuel(Fuel);
        if(Fuel == 0) {
            UIManager.Instance.GameOver();
        }
        else {
            ManageFuel();
        }
    }

    public int GetFuel() {
        return Fuel;
    }

    public void StopFuelDecrease() {
        StopAllCoroutines();
    }
}
