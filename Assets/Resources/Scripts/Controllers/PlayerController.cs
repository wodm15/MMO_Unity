using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

	// bool _moveToDest = false;
	Vector3 _destPos;

    void Start()
    {
		// Managers.Input.KeyAction -= OnKeyboard;
		// Managers.Input.KeyAction += OnKeyboard;
		Managers.Input.MouseAction -= OnMouseClicked;
		Managers.Input.MouseAction += OnMouseClicked;
	}
	float wait_run_ratio = 0;

	public enum PlayerState
	{
		Die,
		Moving,
		Idle,
	}

	PlayerState _state = PlayerState.Idle;

	void UpdateDie()
	{

	}

	void UpdateMoving()
	{
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.0001f)
		{
			_state = PlayerState.Idle;
		}
		else
		{
			float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}

		//애니메이션 처리
		wait_run_ratio = Mathf.Lerp(wait_run_ratio , 1 ,10.0f * Time.deltaTime);
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("wait_run_ratio", wait_run_ratio);
	}

	void UpdateIdle()
    {
		//애니메이션
		wait_run_ratio = Mathf.Lerp(wait_run_ratio , 0 ,10.0f * Time.deltaTime);
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("wait_run_ratio", wait_run_ratio);

    }
    void Update()
    {
		switch (_state)
		{
			case PlayerState.Die:
			    UpdateDie();
				break;
			
			case PlayerState.Moving:
				UpdateMoving();
				break;

			case PlayerState.Idle:
				UpdateIdle();
				break;
		}
    }

    // void OnKeyboard()
    // {
	// 	if (Input.GetKey(KeyCode.W))
	// 	{
	// 		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
	// 		transform.position += Vector3.forward * Time.deltaTime * _speed;
	// 	}

	// 	if (Input.GetKey(KeyCode.S))
	// 	{
	// 		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
	// 		transform.position += Vector3.back * Time.deltaTime * _speed;
	// 	}

	// 	if (Input.GetKey(KeyCode.A))
	// 	{
	// 		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
	// 		transform.position += Vector3.left * Time.deltaTime * _speed;
	// 	}

	// 	if (Input.GetKey(KeyCode.D))
	// 	{
	// 		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
	// 		transform.position += Vector3.right * Time.deltaTime * _speed;
	// 	}

	// 	_moveToDest = false;
	// }

	void OnMouseClicked(Define.MouseEvent evt)
	{
		if(_state == PlayerState.Die)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
		{
			_destPos = hit.point;
			_state = PlayerState.Moving;
		}
	}
}
