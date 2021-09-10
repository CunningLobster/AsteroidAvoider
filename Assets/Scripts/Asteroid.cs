using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player == null)
        {
            return;
        }
        
        player.Crash();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
