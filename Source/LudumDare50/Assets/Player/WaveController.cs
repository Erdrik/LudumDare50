using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Serializable]
    public class WaveDetails
    {
        public float startTime;
        public AudioClip narration;
        public string subtitle;
        public List<GameObject> enable;
    }

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private AudioSource narrator;

    [SerializeField]
    private TextMeshProUGUI subtitleText;

    [SerializeField]
    private List<WaveDetails> waves;

    [SerializeField]
    private int nextWave;

    [SerializeField]
    private bool wavesRemaining;

    private void Start()
    {
        wavesRemaining = waves.Count > 0;
        nextWave = 1;
        PlayWave(waves[0]);
        foreach (var waveDetails in waves)
        {
            foreach (var toEnable in waveDetails.enable)
            {
                toEnable.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (wavesRemaining)
        {
            var currentTime = timelineController.TotalTime;
            var nextWaveDetails = waves[nextWave];
            if (currentTime > nextWaveDetails.startTime)
            {
                ProgressWave();
            }
        }
    }

    public void ProgressWave()
    {
        var nextWaveDetails = waves[nextWave];
        PlayWave(nextWaveDetails);
        IncrementWave();
    }

    private void PlayWave(WaveDetails waveDetails)
    {
        if (waveDetails.narration != null) {
            narrator.PlayOneShot(waveDetails.narration);
        }
        subtitleText.SetText(waveDetails.subtitle);
        foreach (var enableTarget in waveDetails.enable)
        {
            enableTarget.SetActive(true);
        }
    }

    private void IncrementWave()
    {
        ++nextWave;
        if (nextWave >= waves.Count)
        {
            wavesRemaining = false;
        }
    }
}
