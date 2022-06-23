using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : BaseController
{
	Stat _stat;
	int _mask = (1 << (int)Define.Layer.Ground);
	bool _stopSkill = false;

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;
		_stat = gameObject.GetComponent<Stat>();
		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;
	}

	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (State)
		{
			case Define.State.Idle:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Moving:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Skill:
				{
					if (evt == Define.MouseEvent.PointerUp)
						_stopSkill = true;
				}
				break;
		}
	}

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
				{
					if (raycastHit)
					{
						destPos = hit.point;
						State = Define.State.Moving;
						_stopSkill = false;
					}
				}
				break;
			case Define.MouseEvent.Press:
				{
					if (raycastHit)
						destPos = hit.point;
				}
				break;
			case Define.MouseEvent.PointerUp:
				_stopSkill = true;
				break;
		}
	}

	protected override void UpdateIdle()
    {
		if (_stat.isDead())
        {
			State = Define.State.Die;
			return;
        }

		// 우클릭으로 공격
		if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f, _mask))
            {
				destPos = hit.point;
            }

			State = Define.State.Skill;
			return;
		}
	}

	protected override void UpdateMoving()
	{
		if (_stat.isDead())
		{
			State = Define.State.Die;
			return;
		}

		// 우클릭으로 공격
		if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100.0f, _mask))
			{
				destPos = hit.point;
			}

			State = Define.State.Skill;
			return;
		}

		Vector3 dir = destPos - transform.position;
		dir.y = 0;         
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
			if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
			{
				if (Input.GetMouseButton(0) == false)
					State = Define.State.Idle;
				return;
			}

			float moveDist = Mathf.Clamp(5f * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}

	}

	protected override void UpdateSkill()
	{
		if (_stat.isDead())
		{
			State = Define.State.Die;
			return;
		}

		Vector3 dir = destPos - transform.position;
		Quaternion quat = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
	}

    void OnSkillFinishEvent()
    {
		State = Define.State.Idle;
    }
}
