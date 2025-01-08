using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateSkullCrasher : IEnemyState
{
    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("ChaseStateSkulLCrusher");


        enemy.animator.ResetTrigger("IdleTrigger");
        enemy.animator.SetTrigger("RunTrigger");
    }

    public void UpdateState(EnemyMain enemy)
    {
        if (enemy.transformTarget == null)
        {
            enemy.SwitchState(new IdleStateToLowestHealth());
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.transformTarget.position);

        if (distance <= enemy.attackRange)
        {
            enemy.SwitchState(new AttackStateSkullCrasher());
            return;
        }
        else
        {
            enemy.MoveTo(enemy.transformTarget.position);
        }
    }

    public void ExitState(EnemyMain enemy)
    {

    }
}