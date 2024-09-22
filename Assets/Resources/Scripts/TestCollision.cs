using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.name != "Plane")
            Debug.Log($"Collision @ {collisionInfo.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"isTrigger @ {other.gameObject.name}");
    }
    
    void Update()
    {
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); //뷰 포인트
        //로컬에서 월드 방향으로 변환
        // Vector3 look = transform.TransformDirection(Vector3.forward);
        // Debug.DrawRay(transform.position+ Vector3.up, look * 10, Color.red);

        // RaycastHit hit;
        // if(Physics.Raycast(transform.position, look ,out hit, 10))
        // {
        //     Debug.Log($"Raycast {hit.collider.gameObject.name}");
        // }

        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f , Color.red , 1.0f);

            int mask = (1<< 8) | (1 << 7);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f , mask))
            {
                Debug.Log($"Raycast {hit.collider.gameObject.name}");
            }
        }
    }
}
