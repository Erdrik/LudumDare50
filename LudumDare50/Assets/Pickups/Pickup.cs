using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public event Action<bool> Disappeared;

    [SerializeField]
    private Transform self;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool destroyOnPickup = true;

    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            target = PlayerMovement.Instance;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (self == null || target == null)
        {
            return;
        }
        var previousColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(self.position, target.position);
        Gizmos.color = previousColor;
    }

    private void FixedUpdate()
    {
        Check();
    }

    public void Disappear()
    {
        Disappeared?.Invoke(false);
        Destroy(this.gameObject);
    }

    private void Check()
    {
        offset = target.position - self.position;
        var offsetSqrMagnitude = offset.sqrMagnitude;
        if (offsetSqrMagnitude < 1.0f)
        {
            Disappeared?.Invoke(true);
            if (destroyOnPickup)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
