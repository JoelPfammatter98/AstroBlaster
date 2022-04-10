using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject BulletObject;

    private void OnCollisionEnter(Collision collision) {
        GameObject.Destroy(BulletObject);
    }
}
