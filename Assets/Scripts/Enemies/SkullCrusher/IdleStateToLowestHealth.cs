using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateToLowestHealth : IEnemyState
{
    private float timer;

    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("IdleStateSkullCrusher");
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.animator.ResetTrigger("AttackTrigger");
        enemy.animator.SetTrigger("IdleTrigger");
        float healthPercentage = enemy.myEnemyHealthbar.hp / enemy.myEnemyHealthbar.GetMaxHp();
        float multiplier = Mathf.Lerp(1f, 2f, 1 - healthPercentage);
        //enemy.updatingSpeedChangerAnim = enemy.startingSpeedChangerSpeed * multiplier;
        //enemy.animator.SetFloat("SpeedChanger", enemy.updatingSpeedChangerAnim);
    }

    public void UpdateState(EnemyMain enemy)
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.detectionRange, enemy.unitLayer);
            if (unitsInRange.Length > 0)
            {
                Transform closestEnemy = FindLowestHealthTarget(unitsInRange);
                enemy.transformTarget = closestEnemy;

                enemy.SwitchState(new ChaseStateSkullCrasher());
            }
            else
            {
                enemy.SwitchState(new PatrolStateSkullCrusher());
            }
        }


       
    }
        
    public void ExitState(EnemyMain enemy)
    {
        //enemy.animator.ResetTrigger("IdleTrigger");
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
