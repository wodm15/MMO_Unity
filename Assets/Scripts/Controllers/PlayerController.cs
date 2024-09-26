﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
	Vector3 _destPos;

    void Start()
    {
		_stat = GetComponent<PlayerStat>();

		Managers.Input.MouseAction -= OnMouseClicked;
		Managers.Input.MouseAction += OnMouseClicked;		
	}

	public enum PlayerState
	{
		Die,
		Moving,
		Idle,
		Skill,
	}

	PlayerState _state = PlayerState.Idle;

	void UpdateDie()
	{
		// 아무것도 못함

	}

	void UpdateMoving()
	{
		Vector3 dir = _destPos - transform.position;
		if (dir.magnitude < 0.1f)
		{
			_state = PlayerState.Idle;
		}
		else
		{
			float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
			NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

			nma.Move(dir.normalized * moveDist);

			Debug.DrawRay(transform.position , dir.normalized , Color.green);
			if(Physics.Raycast(transform.position + Vector3.up * 0.5f , dir, 1.0f , LayerMask.GetMask("Block")))
			{
				_state = PlayerState.Idle;
				return;
			}
			
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}

		// 애니메이션
		Animator anim = GetComponent<Animator>();
		// 현재 게임 상태에 대한 정보를 넘겨준다
		anim.SetFloat("speed", _stat.MoveSpeed);
	}

	void UpdateIdle()
	{
		// 애니메이션
		Animator anim = GetComponent<Animator>();

		anim.SetFloat("speed", 0);
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

	int _mask = (1 << (int)Define.Layer.Ground) | ( 1<< (int)Define.Layer.Monster);

	void OnMouseClicked(Define.MouseEvent evt)
	{
		if (_state == PlayerState.Die)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0f, _mask))
		{
			_destPos = hit.point;
			_state = PlayerState.Moving;

			if(hit.collider.gameObject.layer == (int)Define.Layer.Monster)
			{
				Debug.Log("monster click");
			}
			else
			{
				Debug.Log("ground click");
			}
		}
	}
}
