using UnityEngine;
using UnityEngine.Events;

public class RandomActionRunBehaviour : MonoBehaviour
{
    [SerializeField] private float minProbability;
    [SerializeField] private float probabilityIncrease;
    [SerializeField] private IncreaseType increaseType = IncreaseType.Multiplicative;
    [SerializeField] private double rollIntervalSeconds = 1;

    public UnityEvent OnAction = new UnityEvent();
    private double currentProbability;
    private double lastCheckTime = 0;

    private void Start()
    {
        currentProbability = minProbability;
    }

    public enum IncreaseType { Multiplicative, Additive }

    private void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }
        if (lastCheckTime + rollIntervalSeconds > Time.timeSinceLevelLoad)
        {
            return;
        }
        lastCheckTime = Time.timeSinceLevelLoad;
        bool willDoAction = Random.Range(0f, 1f) <= currentProbability;
        if (willDoAction)
        {
            currentProbability = minProbability;
            this.OnAction.Invoke();
        }
        else
        {
            currentProbability = increaseType == IncreaseType.Multiplicative ? currentProbability * probabilityIncrease : currentProbability + probabilityIncrease;
        }
    }
}
