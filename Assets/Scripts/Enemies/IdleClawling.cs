using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleClawling : IEnemyState
{
    private float timer;

    public void EnterState(EnemyMain enemy)
    {
        float multiplier = (enemy.myEnemyHealthbar.hp / enemy.myEnemyHealthbar.GetMaxHp());
        timer = Mathf.Max(1f, (enemy.idleTimer * multiplier));
        enemy.animator.speed = enemy.animator.speed * multiplier;
        Debug.Log(timer);
        Debug.Log(enemy.animator.speed);
        enemy.animator.SetTrigger("IdleTrigger");
    }

    public void UpdateState(EnemyMain enemy)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(enemy.transform.position, 1000, enemy.unitLayer);
            if (unitsInRange.Length > 0)
            {
                Transform closestEnemy = FindLowestHealthTarget(unitsInRange);
                enemy.transformTarget = closestEnemy;

                enemy.SwitchState(new ChaseStateClawling());
            }
            else
            {
                enemy.SwitchState(new IdleStateToLowestHealth());
            }
        }



    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("IdleTrigger");
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
