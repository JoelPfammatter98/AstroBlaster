using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    /// <summary>
    /// This class is necessary because order of Unity Event Handler execution is not defined.
    /// </summary>
    public class DestroyBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyDestroyerBehaviour enemyDestroyer;
        [SerializeField] private AudioSource sound;

        public void PlayAndDestroy(GameObject target)
        {
            var tempSoundObj = GameObject.Instantiate(this.sound);
            tempSoundObj.Play();
            StartCoroutine(DestroyAfterSound(tempSoundObj));
            this.enemyDestroyer?.DestroyEnemy(target);
        }

        public IEnumerator DestroyAfterSound(UnityEngine.Object obj)
        {
            yield return new WaitForSeconds(5);
            Destroy(obj);
            StopCoroutine(DestroyAfterSound(obj));
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
