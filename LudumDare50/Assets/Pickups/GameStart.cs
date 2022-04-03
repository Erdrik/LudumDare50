using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    [SerializeField]
    private WaveController waveController;

    // Start is called before the first frame update
    void Start()
    {
        pickup.PickedUp += OnPickedUp;
    }

    private void OnPickedUp()
    {
        waveController.ProgressWave();
    }
}
