using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSourceController : MonoBehaviour
{
    public float maxdist = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="FallingWaterParticles" && transform.position.y<maxdist)
        { 
            transform.position += new Vector3(0, 0.03f, 0);
        }
    }
}
