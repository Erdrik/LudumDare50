using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> timeGainerSpawnPositions;

    [SerializeField]
    private GameObject timeGainerPickup;

    [SerializeField]
    private float timeGainerOffsetMilliseconds;

    private DateTime lastTimeGainer = DateTime.MinValue;

    private List<int> timeGainerAvailablePositions;

    private void Start()
    {
        timeGainerAvailablePositions = new List<int>();
        for (var i = 0; i < timeGainerSpawnPositions.Count; ++i)
        {
            timeGainerAvailablePositions.Add(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var timeNow = DateTime.UtcNow;
        SpawnTimeGainer(timeNow);
    }

    void OnDrawGizmosSelected()
    {
        var previousColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        foreach (var spawnPosition in timeGainerSpawnPositions)
        {
            Gizmos.DrawCube(spawnPosition, new Vector3(0.25f, 0.25f, 0.25f));
        }
        Gizmos.color = previousColor;
    }

    private void SpawnTimeGainer(DateTime currentTime)
    {
        var nextTimeGainer = lastTimeGainer.AddMilliseconds(timeGainerOffsetMilliseconds);
        if (currentTime >= nextTimeGainer)
        {
            var randomPosition = GetRandomPosition(timeGainerSpawnPositions, timeGainerAvailablePositions, out var randomIndex);
            var gameObject = Instantiate(timeGainerPickup, randomPosition, Quaternion.identity);
            var pickup = gameObject.GetComponent<Pickup>();
            pickup.PickedUp += () => { PositionCleared(randomIndex); };
            lastTimeGainer = currentTime;
        }
    }

    private Vector3 GetRandomPosition(List<Vector3> positions, List<int> available, out int index)
    {
        var randomIndex = UnityEngine.Random.Range(0, available.Count);
        index = available[randomIndex];
        available.RemoveAt(randomIndex);
        return positions[index];
    }

    private void PositionCleared(int index)
    {
        timeGainerAvailablePositions.Add(index);
    }
}
