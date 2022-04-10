using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SingletonReferenceProviderBehaviour))]
public class GameManager : MonoBehaviour
{
    private SingletonReferenceProviderBehaviour singleReferenceProvider;

    private void Start()
    {
        this.singleReferenceProvider = GetComponent<SingletonReferenceProviderBehaviour>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.singleReferenceProvider.ClearAll();
            PlayerMovement.Instance.ResetAllLocks();  
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            Time.timeScale = 1;
        }
    }
}
