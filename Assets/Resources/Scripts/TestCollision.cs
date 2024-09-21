using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log($"Collision @ {collisionInfo.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"isTrigger @ {other.gameObject.name}");
    }
}
