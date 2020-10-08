using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public static Action onFinishTile;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            Destroy(other.gameObject);
            onFinishTile?.Invoke();
        }
    }
}
