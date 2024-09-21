using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 10.0f;

    void Start()
    {
        //구독 신청 
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;

    }

    void Update()
    {

    }

    void OnKeyBoard()
    {
        if(Input.GetKey(KeyCode.W))
        {   
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward) , 0.2f);
            // transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back) , 0.2f);
            // transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left) , 0.2f);
            // transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right) , 0.2f);
            // transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
