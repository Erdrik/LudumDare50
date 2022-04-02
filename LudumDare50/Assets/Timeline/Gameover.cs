using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Gameover : MonoBehaviour
{
    [SerializeField]
    private TimelineController timelineController;

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
        Quit();
    }
}
