using System;
using UnityEngine;

public class SingletonReferenceProviderBehaviour : MonoBehaviour
{
    private Guid ID = Guid.NewGuid();

    private static SingletonReferenceProviderBehaviour singletonInstance;
    private SingletonReferenceProviderBehaviour singleton {
        get
        {
            var singleton = singletonInstance ??= this;
            Debug.Log($"ID: {singleton.ID}");
            return singleton;
        }
    }

    private UIManager uiManager;
    private GameManager gameManager;
    private TargetController targetController;

    public void ClearAll()
    {
        singletonInstance = null;
    }

    private ScoreManager scoreManager;

    public UIManager UIManager => singleton.uiManager ??= FindObjectOfType<UIManager>();
    public GameManager GameManager => singleton.gameManager ??= FindObjectOfType<GameManager>();
    public TargetController TargetController => singleton.targetController ??= FindObjectOfType<TargetController>();
    public ScoreManager ScoreManager => singleton.scoreManager ??= FindObjectOfType<ScoreManager>();
}
