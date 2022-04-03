using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    [SerializeField]
    private float lifetimeMilliseconds;

    private DateTime deathTime;

    // Start is called before the first frame update
    void Start()
    {
        deathTime = DateTime.UtcNow.AddMilliseconds(lifetimeMilliseconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DateTime.UtcNow > deathTime)
        {
            pickup.Disappear();
        }
    }
}
