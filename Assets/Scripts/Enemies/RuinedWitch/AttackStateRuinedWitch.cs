using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateRuinedWitch : IEnemyState
{
    bool stateShotOne = false;
    bool stateShotTwo = false;

    float attackStateRuinedWitch;


    public void EnterState(EnemyMain enemy)
    {

        enemy.ClearPath();
        attackStateRuinedWitch = enemy.attackTimer;
        enemy.animator.SetTrigger("AttackTrigger");

    }

    public void UpdateState(EnemyMain enemy)
    {
        attackStateRuinedWitch -= Time.deltaTime;
        //Debug.Log(attackStateVoidboneTimer);

        if (!stateShotOne && attackStateRuinedWitch <= (((attackStateRuinedWitch / 3) * 2) + 0.1))
        {
            Debug.Log(attackStateRuinedWitch);
            enemy.SpecialAbility();
            stateShotOne = true;


        }
        else if (!stateShotTwo && attackStateRuinedWitch <= ((attackStateRuinedWitch / 3) + 0.1))
        {
            Debug.Log(attackStateRuinedWitch);
            enemy.SpecialAbility();
            stateShotTwo = true;
        }
        else if (attackStateRuinedWitch < 0)
        {
            enemy.SwitchState(new IdleStateRuinedWitch());
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("AttackTrigger");
    }
}
