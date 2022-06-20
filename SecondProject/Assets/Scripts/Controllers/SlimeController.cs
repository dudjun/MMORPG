using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonsterController
{
    protected override void UpdateMoving()
    {
        if (_stat.isDead())
        {
            State = Define.State.Die;
            return;
        }

        if (_lockTarget != null)
        {
            destPos = _lockTarget.transform.position;
            float distance = (destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                if (QuickSlot.go_HandWeapon == null)
                {
                    NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                    nma.SetDestination(transform.position);
                    State = Define.State.Skill;
                }
                return;
            }
        }

        Vector3 dir = destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            if(QuickSlot.go_HandWeapon != null)
            {
                destPos = new Vector3(transform.position.x - _lockTarget.transform.position.x, 
                                      0f, 
                                      transform.position.z - _lockTarget.transform.position.z);
                dir = new Vector3(dir.x, dir.y, dir.z + 180f);
            }
            nma.SetDestination(destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }
}
