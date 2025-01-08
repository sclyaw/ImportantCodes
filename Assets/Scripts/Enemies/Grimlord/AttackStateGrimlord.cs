using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateGrimlord : IEnemyState
{
    public void EnterState(EnemyMain enemy)
    {
        enemy.specialAttackNumber = 1;
        enemy.SpecialAbility();


    }

    public void UpdateState(EnemyMain enemy)
    {





    }

    public void ExitState(EnemyMain enemy)
    {

    }
}

