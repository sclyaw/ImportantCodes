using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateVoidbone : IEnemyState
{

    bool stateShotOne= false;
    bool stateShotTwo = false;

    float attackStateVoidboneTimer;


    public void EnterState(EnemyMain enemy)
    {
        
        enemy.ClearPath();
        attackStateVoidboneTimer = enemy.attackTimer;
        enemy.animator.SetTrigger("AttackTrigger");
        
    }

    public void UpdateState(EnemyMain enemy)
    {
        attackStateVoidboneTimer -= Time.deltaTime;
        //Debug.Log(attackStateVoidboneTimer);

        if (!stateShotOne && attackStateVoidboneTimer <= (((attackStateVoidboneTimer/3)*2)+0.1))
        {
            enemy.SpecialAbility();
            stateShotOne = true;


        }
        else if (!stateShotTwo && attackStateVoidboneTimer <= ((attackStateVoidboneTimer / 3) + 0.1))
        {
            enemy.SpecialAbility();
            stateShotTwo = true;
        }
        else if(attackStateVoidboneTimer < 0)
        {
            enemy.SwitchState(new IdleStateVoidbone());
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("AttackTrigger");
    }

    

}
