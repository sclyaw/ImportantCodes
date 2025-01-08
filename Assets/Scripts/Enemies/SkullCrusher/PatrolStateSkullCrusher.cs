using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateSkullCrusher : IEnemyState
{

    private float timer = 2f;


    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("PatrolStateSkullCrusher");

        enemy.animator.ResetTrigger("IdleTrigger");
        enemy.animator.SetTrigger("RunTrigger");
    }

    public void UpdateState(EnemyMain enemy)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(enemy.transform.position, 100f, enemy.unitLayer);
            if (unitsInRange.Length > 0)
            {
                Transform closestEnemy = FindLowestHealthTarget(unitsInRange);
                enemy.transformTarget = closestEnemy;

                enemy.SwitchState(new ChaseStateSkullCrasher());
            }
            else
            {
                enemy.SwitchState(new IdleStateToLowestHealth());
            }
        }
    }

    public void ExitState(EnemyMain enemy)
    {
        //Debug.Log("Exiting Chase State");
    }


    private Transform FindLowestHealthTarget(Collider2D[] units)
    {
        float lowestHealth = float.MaxValue;
        Transform lowestHealthEnemy = null;

        foreach (Collider2D unit in units)
        {
            Healthbar healthbar = unit.GetComponentInChildren<Healthbar>();
            if (healthbar != null && healthbar.hp > 0 && healthbar.hp < lowestHealth)
            {
                lowestHealth = healthbar.hp;
                lowestHealthEnemy = unit.transform;
            }
        }

        return lowestHealthEnemy;
    }
}