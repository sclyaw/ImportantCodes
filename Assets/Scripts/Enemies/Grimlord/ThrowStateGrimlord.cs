using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStateGrimlord : IEnemyState
{
    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("ThrowStateGrimlord");
        enemy.ClearPath();
        enemy.animator.SetTrigger("ThrowTrigger");
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.specialAttackNumber = 3;
        enemy.SpecialAbility();


    }

    public void UpdateState(EnemyMain enemy)
    {





    }

    public void ExitState(EnemyMain enemy)
    {

    }
}

