using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyMain enemy)
    {

       
       
        enemy.animator.SetTrigger("RunTrigger");
    }

    public void UpdateState(EnemyMain enemy)
    {
        if (!enemy.transformTarget.gameObject.activeSelf)
        {
            enemy.SwitchState(new IdleStateToLowestHealth());
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.transformTarget.position);

        if (distance <= enemy.attackRange)
        {
            enemy.SwitchState(new AttackStateSkullCrasher());
        }
        else
        {
            enemy.MoveTo(enemy.transformTarget.position);
        }
    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.ClearPath();

    }
}