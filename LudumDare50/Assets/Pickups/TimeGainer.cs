using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGainer : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    [SerializeField]
    private float timeGained;

    // Start is called before the first frame update
    void Start()
    {
        pickup.PickedUp += OnPickedUp;
    }

    private void OnPickedUp()
    {
        Debug.Log("Picked up!");
    }
}
