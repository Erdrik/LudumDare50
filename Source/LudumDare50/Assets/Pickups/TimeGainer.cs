using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGainer : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private float timeGained;

    // Start is called before the first frame update
    void Start()
    {
        pickup.Disappeared += OnDisappeared;
        if (timelineController == null)
        {
            timelineController = TimelineController.Instance;
        }
    }

    private void OnDisappeared(bool pickedUp)
    {
        if (pickedUp)
        {
            timelineController.GainTime(timeGained);
        }
    }
}
