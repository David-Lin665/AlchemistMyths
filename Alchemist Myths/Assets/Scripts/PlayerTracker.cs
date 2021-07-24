using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static Vector3 playerPosition;
    void Update()
    {
        playerPosition = transform.position;
    }
}
