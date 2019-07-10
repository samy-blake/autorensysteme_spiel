using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour
{
    private ParticleSystem ps;

    // Alternative: Start
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Alternative: isPlaying
        if (!ps.IsAlive())
        {
            Destroy(gameObject);
            //gameObject.SetActive(false); // Alternative für Pooling
        }
    }
}
