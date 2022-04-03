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
    [SerializeField]
    private TimelineController timelineController;

    [SerializeField]
    private AudioSource narrator;

    [SerializeField]
    private AudioClip gameoverSound;

    [SerializeField]
    private float gameoverDelay;

    [SerializeField]
    private Image gameoverImage;

    [SerializeField]
    private TextMeshProUGUI subtitles;

    [SerializeField]
    private string gameoverSubtitles;

    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        timelineController.TimelineEnded += OnTimelineEnded;
        playerControls.Menu.Quit.Enable();
        playerControls.Menu.Quit.performed += OnQuit;
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
        StartCoroutine(GameoverCoroutine());
    }

    private IEnumerator GameoverCoroutine()
    {
        narrator.PlayOneShot(gameoverSound);
        subtitles.text = gameoverSubtitles;
        gameoverImage.enabled = true;
        yield return new WaitForSeconds(gameoverDelay);
        SceneManager.LoadScene(0);
    }
}
