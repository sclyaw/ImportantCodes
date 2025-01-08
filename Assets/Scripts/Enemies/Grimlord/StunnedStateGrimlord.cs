using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedStateGrimlord : IEnemyState
{
    private Vector3 positioningHere;
    private float tempOffset;

    public void EnterState(EnemyMain enemy)
    {

        enemy.ClearPath();
        enemy.animator.SetTrigger("StunTrigger");
        enemy.animator.ResetTrigger("ThrowTrigger");
        enemy.animator.ResetTrigger("IdleTrigger");
        enemy.animator.ResetTrigger("TripleAttackTrigger");
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.animator.ResetTrigger("SpecialTrigger");
        enemy.animator.ResetTrigger("AttackTrigger");
        
        enemy.StunIndicatorStart();




    }

    public void UpdateState(EnemyMain enemy)
    {




    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("StunTrigger");
        enemy.StunIndicatorEnd();
    }
}


