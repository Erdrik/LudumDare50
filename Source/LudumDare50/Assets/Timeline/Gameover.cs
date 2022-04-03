using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Gameover : MonoBehaviour
{
    public static Gameover Instance { get; private set; }

    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private AudioSource narrator;

    [SerializeField]
    private AudioClip gameoverSound;

    [SerializeField]
    private AudioClip victorySound;

    [SerializeField]
    private float gameoverDelay;

    [SerializeField]
    private float victoryDelay;

    [SerializeField]
    private Image gameoverImage;

    [SerializeField]
    private Color gameoverColor;

    [SerializeField]
    private Color victoryColor;

    [SerializeField]
    private TextMeshProUGUI subtitles;

    [SerializeField]
    private string gameoverSubtitles;

    [SerializeField]
    private string victorySubtitles;

    private PlayerControls playerControls;

    private bool ending = false;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        Instance = this;
        if (timelineController != null) {
            timelineController.TimelineEnded += OnTimelineEnded;
        }
        playerControls.Menu.Quit.Enable();
        playerControls.Menu.Quit.performed += OnQuit;
    }

    public void Victory()
    {
        if (!ending)
        {
            StartCoroutine(GameoverCoroutine(victorySound, victoryDelay, victorySubtitles, victoryColor, 1));
        }
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnQuit(InputAction.CallbackContext obj)
    {
        Quit();
    }

    private void OnTimelineEnded()
    {
        if (!ending)
        {
            StartCoroutine(GameoverCoroutine(gameoverSound, gameoverDelay, gameoverSubtitles, gameoverColor, 0));
        }
    }

    private IEnumerator GameoverCoroutine(AudioClip sound, float delay, string subtitlesText, Color imageColor, int scene)
    {
        ending = true;
        timelineController.gameObject.SetActive(false);
        narrator.PlayOneShot(sound);
        subtitles.text = subtitlesText;
        gameoverImage.color = imageColor;
        gameoverImage.enabled = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
