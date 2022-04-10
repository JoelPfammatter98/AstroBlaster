using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PooledObjectBehaviour : MonoBehaviour
{
    private Stack<GameObject> freeObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int initialPoolSize = 0;
    [SerializeField] private Transform pooledObjectsParent;

    public void Start()
    {
        this.freeObjects = new Stack<GameObject>();
        this.objectToPool ??= new GameObject();

        for (int i = 0; i < initialPoolSize; i++)
        {
            this.Release(this.InstantiatePoolObject(true));
        }
    }

    public GameObject ClaimPooledObject()
    {
        var obj = this.TryPop(this.freeObjects);
        if (obj == null)
        {
            obj = this.InstantiatePoolObject(false);
        }
        obj.SetActive(true);
        return obj;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        this.freeObjects.Push(obj);
    }

    private GameObject InstantiatePoolObject(bool initializing)
    {
        if (!initializing) Debug.LogWarning($"Object pool too small, additional pool objects are being instantiated. Consider increasing initial object pool size (current: {this.initialPoolSize}).", this);
        return Instantiate(objectToPool, this.pooledObjectsParent);
    }

    private GameObject TryPop(Stack<GameObject> stack)
    {
        try
        {
            return stack.Pop();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}
