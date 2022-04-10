using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float MovementSpeed = 0.1f;
    static bool LockTop = false;
    static bool LockBottom = false;
    static bool LockLeft = false;
    static bool LockRight = false;

    private static PlayerMovement _instance;
    public static PlayerMovement Instance { get { return _instance; } }

    private void Start()
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

    void Update()
    {
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && LockTop == false) {
            Vector3 PlayerOldPosition = Player.transform.position;
            Vector3 PlayerNewPosition = PlayerOldPosition;
            PlayerNewPosition.y = PlayerNewPosition.y + MovementSpeed;
            Player.transform.position = Vector3.Lerp(PlayerOldPosition, PlayerNewPosition, Time.deltaTime);
            LockBottom = false;
            
        }
        else if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && LockBottom == false) {
            Vector3 PlayerOldPosition = Player.transform.position;
            Vector3 PlayerNewPosition = PlayerOldPosition;
            PlayerNewPosition.y = PlayerNewPosition.y - MovementSpeed;
            Player.transform.position = Vector3.Lerp(PlayerOldPosition, PlayerNewPosition, Time.deltaTime);
            LockTop = false;
        }
        else if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && LockLeft == false) {
            Vector3 PlayerOldPosition = Player.transform.position;
            Vector3 PlayerNewPosition = PlayerOldPosition;
            PlayerNewPosition.x = PlayerNewPosition.x - MovementSpeed;
            Player.transform.position = Vector3.Lerp(PlayerOldPosition, PlayerNewPosition, Time.deltaTime);
            LockRight = false;
            
        }
        else if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && LockRight == false) {
            Vector3 PlayerOldPosition = Player.transform.position;
            Vector3 PlayerNewPosition = PlayerOldPosition;
            PlayerNewPosition.x = PlayerNewPosition.x + MovementSpeed;
            Player.transform.position = Vector3.Lerp(PlayerOldPosition, PlayerNewPosition, Time.deltaTime);
            LockLeft = false;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            ShootManager.Instance.PlayerShoot();      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FieldTriggerTop") {
            LockTop = true;
        }
        if(other.gameObject.tag == "FieldTriggerBottom") {
            LockBottom = true;
        }
        if(other.gameObject.tag == "FieldTriggerLeft") {
            LockLeft = true;
        }
        if(other.gameObject.tag == "FieldTriggerRight") {
            LockRight = true;
        }
    }

    public void ResetAllLocks() {
        LockTop = false;
        LockBottom = false;
        LockLeft = false;
        LockRight = false;
    }

}
