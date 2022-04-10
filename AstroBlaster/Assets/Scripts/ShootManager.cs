using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField] private PooledObjectBehaviour Bullets;
    [SerializeField] private GameObject  PlayerShootPositionLeft;
    [SerializeField] private GameObject  PlayerShootPositionRight;
    [SerializeField] private AudioSource BlasterSound;
    [SerializeField] private float bulletSpeed = 20f;
    private Rigidbody BulletRigidBody;
    private bool ShootLeft  = true;
    private GameObject NewBullet;
    private float BulletAdjust;

    private static ShootManager _instance;
    public static ShootManager Instance { get { return _instance; } }

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
        BlasterSound = Instantiate(BlasterSound, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
    
    public void PlayerShoot() {
        if(UIManager.Instance.GetOverheatBlock() == false) {
            
            if(ShootLeft == true) {
                BulletAdjust = 0.4f;
                NewBullet = Bullets.ClaimPooledObject();
                NewBullet.transform.position = PlayerShootPositionLeft.transform.position;
                NewBullet.transform.rotation = Quaternion.identity;
                ShootLeft = false;
            }
            else {
                BulletAdjust = -0.4f;
                NewBullet = Bullets.ClaimPooledObject();
                NewBullet.transform.position = PlayerShootPositionRight.transform.position;
                NewBullet.transform.rotation = Quaternion.identity;
                ShootLeft = true;
            }
            NewBullet.GetComponent<HittableBehaviour>()?.OnHit.AddListener(() => Bullets.Release(NewBullet));
            NewBullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            BulletRigidBody = NewBullet.GetComponent<Rigidbody>();
            BulletRigidBody.AddForce(new Vector3(BulletAdjust, 0f, bulletSpeed), ForceMode.Impulse);
            UIManager.Instance.UpdateOverheatUp();
            BlasterSound.Play();
        }
        
    }
}
