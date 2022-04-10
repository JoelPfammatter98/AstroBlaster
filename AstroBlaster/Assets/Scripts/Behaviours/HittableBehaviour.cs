using UnityEngine;
using UnityEngine.Events;

public class HittableBehaviour : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        this.OnHit.Invoke();
    }
}
