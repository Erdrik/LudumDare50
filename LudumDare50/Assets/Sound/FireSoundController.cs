using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FireSoundController : MonoBehaviour
{
    [Serializable]
    public class FireSoundDangerLevelSettings
    {
        public float pitch;
        public float volume;
    }

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private AudioMixer fireSound;

    [SerializeField]
    private FireSoundDangerLevelSettings calmSettings;

    [SerializeField]
    private FireSoundDangerLevelSettings cautionSettings;

    [SerializeField]
    private FireSoundDangerLevelSettings dangerSettings;

    // Start is called before the first frame update
    void Start()
    {
        timelineController.DangerLevelChanged += OnDangerLevelChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDangerLevelChanged(TimelineController.DangerLevel dangerLevel)
    {
        switch (dangerLevel)
        {
            case TimelineController.DangerLevel.Calm: SetFireSound(calmSettings); break;
            case TimelineController.DangerLevel.Caution: SetFireSound(cautionSettings); break;
            case TimelineController.DangerLevel.Danger: SetFireSound(dangerSettings); break;
        }
    }

    private void SetFireSound(FireSoundDangerLevelSettings settings)
    {
        fireSound.SetFloat("Pitch", settings.pitch);
        fireSound.SetFloat("Volume", settings.volume);
    }
}
