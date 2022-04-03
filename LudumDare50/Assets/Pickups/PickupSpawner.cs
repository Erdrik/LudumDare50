using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> spawnPositions;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject pickupPrefab;

    [SerializeField]
    private float offsetMilliseconds;

    [SerializeField]
    private int maxSpawns = 4;

    private DateTime lastSpawn = DateTime.MinValue;

    private List<int> availablePositions;

    void Start()
    {
        availablePositions = new List<int>();
        for (var i = 0; i < spawnPositions.Count; ++i)
        {
            availablePositions.Add(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var timeNow = DateTime.UtcNow;
        SpawnPickup(timeNow);
    }

    void OnDrawGizmosSelected()
    {
        var previousColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        foreach (var spawnPosition in spawnPositions)
        {
            Gizmos.DrawCube(spawnPosition, new Vector3(0.25f, 0.25f, 0.25f));
        }
        Gizmos.color = previousColor;
    }

    private void SpawnPickup(DateTime currentTime)
    {
        if (availablePositions.Count == 0 || availablePositions.Count < (spawnPositions.Count - maxSpawns) )
        {
            return;
        }

        var nextTimeGainer = lastSpawn.AddMilliseconds(offsetMilliseconds);
        if (currentTime >= nextTimeGainer)
        {
            var randomPosition = GetRandomPosition(spawnPositions, availablePositions, out var randomIndex);
            var gameObject = Instantiate(pickupPrefab, randomPosition, Quaternion.identity);
            var pickup = gameObject.GetComponent<Pickup>();
            pickup.PickedUp += () => { PositionCleared(randomIndex); };
            lastSpawn = currentTime;
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
        availablePositions.Add(index);
    }
}
