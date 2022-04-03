using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private bool drums;

    [SerializeField]
    private bool cello;

    [SerializeField]
    private bool violin;

    [SerializeField]
    private AudioSource drumsSource;

    [SerializeField]
    private AudioSource celloSource;

    [SerializeField]
    private AudioSource violinSource;

    void Start()
    {
        timelineController.DangerLevelChanged += OnDangerLevelChanged;
    }

    void FixedUpdate()
    {
        drumsSource.mute = !drums;
        celloSource.mute = !cello;
        violinSource.mute = !violin;
    }

    private void OnDangerLevelChanged(TimelineController.DangerLevel dangerLevel)
    {
        switch (dangerLevel)
        {
            case TimelineController.DangerLevel.Calm:
            {
                drums = true;
                cello = false;
                violin = false;
                break;
            }
            case TimelineController.DangerLevel.Caution:
            {
                drums = true;
                cello = false;
                violin = false;
                break;
            }
            case TimelineController.DangerLevel.Danger:
            {
                drums = true;
                cello = true;
                violin = false;
                break;
            }
        }
    }
}
