using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private float timeLost;

    private void Start()
    {
        pickup.Disappeared += OnDisappeared;
    }

    private void OnDisappeared(bool pickedUp)
    {
        if (pickedUp)
        {
            timelineController.GainTime(timeLost);
        }
    }
}
