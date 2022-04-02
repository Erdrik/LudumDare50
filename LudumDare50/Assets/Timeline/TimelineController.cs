using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimelineController : MonoBehaviour
{
    public event Action TimelineEnded;

    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private Color badColour;

    [SerializeField]
    private Color goodColour;

    [SerializeField]
    private Color greatColour;

    [SerializeField]
    private float startingTime;

    [SerializeField]
    private float timeRemaining;

    [SerializeField]
    private float goodTime = 30.0f;

    private VisualElement timeBackground;
    private Label timeLabel;

    void Start()
    {
        timeBackground = uiDocument.rootVisualElement.Q<VisualElement>("TimeBackground");
        timeLabel = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        timeRemaining = startingTime;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timeRemaining <= 0.0f)
        {
            timeBackground.style.backgroundColor = badColour;
            timeBackground.style.width = new StyleLength(Length.Percent(100.0f));
            EndTimeline();
        }
        else if (timeRemaining > goodTime)
        {
            timeBackground.style.backgroundColor = greatColour;
            timeBackground.style.width = new StyleLength(Length.Percent(100.0f));
        }
        else
        {
            var timeRemainingNormalized = timeRemaining / goodTime;
            timeBackground.style.backgroundColor = Color.Lerp(badColour, goodColour, timeRemainingNormalized);
            timeBackground.style.width = new StyleLength(Length.Percent(timeRemainingNormalized * 100.0f));
        }
        timeLabel.text = timeRemaining.ToString("0.00");
    }

    public void GainTime(float timeGained)
    {
        timeRemaining += timeGained;
    }

    private void EndTimeline()
    {
        TimelineEnded?.Invoke();
    }
}
