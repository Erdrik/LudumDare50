using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimelineController : MonoBehaviour
{
    public enum DangerLevel
    {
        Calm,
        Caution,
        Danger
    }

    public static TimelineController Instance;

    public event Action TimelineEnded;

    public event Action<DangerLevel> DangerLevelChanged;

    [SerializeField]
    private UIDocument uiDocument;

    [SerializeField]
    private AudioSource pickupAudio;

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
    private float cautionTime = 30.0f;

    [SerializeField]
    private float dangerTime = 15.0f;

    public float countdownMultiplier = 1.0f;

    private VisualElement timeBackground;
    private Label timeLabel;

    private DangerLevel dangerLevel;

    public DangerLevel CurrentDangerLevel { get => dangerLevel; }

    public float TotalTime { get; private set; } = -1.0f;

    private void Start()
    {
        Instance = this;
    }

    void OnEnable()
    {
        timeBackground = uiDocument.rootVisualElement.Q<VisualElement>("TimeBackground");
        timeLabel = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        timeRemaining = startingTime;
    }

    private void Update()
    {
        var timeTaken = Time.deltaTime * countdownMultiplier;
        TotalTime += timeTaken;
        timeRemaining -= timeTaken;
    }

    private void FixedUpdate()
    {
        if (timeRemaining <= 0.0f)
        {
            timeBackground.style.backgroundColor = badColour;
            timeBackground.style.width = new StyleLength(Length.Percent(100.0f));
            EndTimeline();
        }
        else if (timeRemaining > cautionTime)
        {
            timeBackground.style.backgroundColor = greatColour;
            timeBackground.style.width = new StyleLength(Length.Percent(100.0f));
        }
        else
        {
            var timeRemainingNormalized = timeRemaining / cautionTime;
            timeBackground.style.backgroundColor = Color.Lerp(badColour, goodColour, timeRemainingNormalized);
            timeBackground.style.width = new StyleLength(Length.Percent(timeRemainingNormalized * 100.0f));
        }
        CheckDangerLevel();
        timeLabel.text = timeRemaining.ToString("0.00");
    }

    public void GainTime(float timeGained)
    {
        timeRemaining += timeGained;
        pickupAudio.Play();
    }

    private void EndTimeline()
    {
        TimelineEnded?.Invoke();
    }

    private void CheckDangerLevel()
    {
        if (timeRemaining < dangerTime)
        {
            ChangeDangerLevel(DangerLevel.Danger);
        }
        else if (timeRemaining < cautionTime)
        {
            ChangeDangerLevel(DangerLevel.Caution);
        }
        else
        {
            ChangeDangerLevel(DangerLevel.Calm);
        }
    }

    private void ChangeDangerLevel(DangerLevel newDangerLevel)
    {
        if (dangerLevel != newDangerLevel)
        {
            dangerLevel = newDangerLevel;
            DangerLevelChanged?.Invoke(dangerLevel);
        }
    }
}
