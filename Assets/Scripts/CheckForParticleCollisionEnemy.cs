using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForParticleCollisionEnemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="FallingWaterParticles")
        {
            transform.parent.GetComponent<CrazyEnemyController>().OnFire = false;
            transform.parent.GetChild(0).gameObject.SetActive(false);
        }
    }
}
