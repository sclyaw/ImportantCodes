using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningStateGrimlord : IEnemyState
{
    private Vector3 positioningHere;
    private float tempOffset;

    public void EnterState(EnemyMain enemy)
    {
        enemy.animator.SetTrigger("RunTrigger");
        enemy.animator.ResetTrigger("ThrowTrigger");
        enemy.animator.ResetTrigger("IdleTrigger");
        

    }

    public void UpdateState(EnemyMain enemy)
    {
        if (enemy.transformTargetOne != null)
        {
            if (enemy.transformTargetOne.position.x - enemy.transform.position.x > 0)
            {
                enemy.transform.localScale = new Vector3(1, 1, 1);
                tempOffset = 2f;
            }
            else if (enemy.transformTargetOne.position.x - enemy.transform.position.x <= 0)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1);
                tempOffset = - 2f;
            }
            positioningHere = new Vector3(enemy.transformTargetOne.position.x - tempOffset, enemy.transformTargetOne.position.y -0.4f, 0);
            enemy.MoveTo(positioningHere);

        }
        else
        {
            enemy.SwitchState(new IdleStateGrimlord());

        }

        if (Vector3.Distance(enemy.transform.position, positioningHere) > 7f && enemy.bullet > 0)
        {
            enemy.SwitchState(new ThrowStateGrimlord());
            enemy.ClearPath();
            
        }

        
        if (Vector3.Distance(enemy.transform.position, positioningHere) < 0.2f)
        {
            enemy.ClearPath();
            Debug.Log("distanceOk");
            enemy.SwitchState(new AttackStateGrimlord());
            

        }




    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("RunTrigger");
    }
}


    