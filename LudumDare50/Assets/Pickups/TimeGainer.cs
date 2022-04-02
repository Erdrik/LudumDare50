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
        pickup.PickedUp += OnPickedUp;
        if (timelineController == null)
        {
            timelineController = TimelineController.Instance;
        }
    }

    private void OnPickedUp()
    {
        timelineController.GainTime(timeGained);
    }
}
