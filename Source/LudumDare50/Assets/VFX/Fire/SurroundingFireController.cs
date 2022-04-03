using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SurroundingFireController : MonoBehaviour
{
    [Serializable]
    public class FadeController
    {
        public DateTime fadeInTime = DateTime.MinValue;
        public DateTime fadeOutTime = DateTime.MinValue;

        public float fadeTimeMilliseconds = 3000.0f;

        public float GetFade(DateTime timeNow)
        {
            var fadeInEnd = fadeInTime.AddMilliseconds(fadeTimeMilliseconds);
            var fadeOutEnd = fadeOutTime.AddMilliseconds(fadeTimeMilliseconds);
            if (fadeInTime > fadeOutEnd) // if we finished fading out, just fade in
            {
                if (timeNow > fadeInEnd) // if we have finished fading in
                {
                    return 0.0f;
                }
                else
                {
                    return 1.0f - ((float)(timeNow - fadeInTime).TotalMilliseconds) / fadeTimeMilliseconds;
                }
            }
            else if (fadeInTime > fadeOutTime) // if we started fading in before we finished fading out
            {
                var fadeIn = 1.0f - ((float)(timeNow - fadeInTime).TotalMilliseconds) / fadeTimeMilliseconds;
                var fadeOut = ((float)(timeNow - fadeOutTime).TotalMilliseconds) / fadeTimeMilliseconds;
                return Mathf.Min(fadeIn, fadeOut);
            }
            else // if we are just fading out
            {
                if (timeNow > fadeOutEnd) // if we have finished fading out
                {
                    return 1.0f;
                }
                else
                {
                    return ((float)(timeNow - fadeOutTime).TotalMilliseconds) / fadeTimeMilliseconds;
                }
            }
        }
    }

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private VisualEffect calmFire;

    [SerializeField]
    private VisualEffect cautionFire;

    [SerializeField]
    private VisualEffect dangerFire;

    [SerializeField]
    private FadeController calmFade = new FadeController();

    [SerializeField]
    private FadeController cautionFade = new FadeController();

    [SerializeField]
    private FadeController dangerFade = new FadeController();

    private TimelineController.DangerLevel currentDangerLevel;

    // Start is called before the first frame update
    void Start()
    {
        timelineController.DangerLevelChanged += OnDangerLevelChanged;
        currentDangerLevel = timelineController.CurrentDangerLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateFadeLevels();
    }

    private void UpdateFadeLevels()
    {
        var timeNow = DateTime.UtcNow;
        calmFire.SetFloat("Fade", calmFade.GetFade(timeNow));
        cautionFire.SetFloat("Fade", cautionFade.GetFade(timeNow));
        dangerFire.SetFloat("Fade", dangerFade.GetFade(timeNow));
    }

    private void OnDangerLevelChanged(TimelineController.DangerLevel dangerLevel)
    {
        var timeNow = DateTime.UtcNow;
        switch (currentDangerLevel)
        {
            case TimelineController.DangerLevel.Calm: calmFade.fadeOutTime = timeNow; break;
            case TimelineController.DangerLevel.Caution: cautionFade.fadeOutTime = timeNow; break;
            case TimelineController.DangerLevel.Danger: dangerFade.fadeOutTime = timeNow; break;
        }
        switch (dangerLevel)
        {
            case TimelineController.DangerLevel.Calm: calmFade.fadeInTime = timeNow; break;
            case TimelineController.DangerLevel.Caution: cautionFade.fadeInTime = timeNow; break;
            case TimelineController.DangerLevel.Danger: dangerFade.fadeInTime = timeNow; break;
        }
        currentDangerLevel = dangerLevel;
    }
}
