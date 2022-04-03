using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameVictory : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    // Start is called before the first frame update
    void Start()
    {
        pickup.Disappeared += OnDisappeared;
    }

    private void OnDisappeared(bool pickedUp)
    {
        if (pickedUp)
        {
            Gameover.Instance.Victory();
        }
    }
}
