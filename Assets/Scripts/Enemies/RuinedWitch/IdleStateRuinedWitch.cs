using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateRuinedWitch : IEnemyState
{
    private float timer = 2.5f;
    public void EnterState(EnemyMain enemy)
    {
        float startingTimer = timer;
        timer = 2.5f;
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.animator.ResetTrigger("AttackTrigger");
        enemy.animator.SetTrigger("IdleTrigger");
        float healthPercentage = enemy.myEnemyHealthbar.hp / enemy.myEnemyHealthbar.GetMaxHp();
        float multiplier = Mathf.Lerp(1f, 1.3f, 1 - healthPercentage);
        enemy.updatingSpeedChangerAnim = enemy.startingSpeedChangerSpeed * multiplier;
        timer = startingTimer * (1 / multiplier);
        //Debug.Log(timer);
        enemy.attackTimer = enemy.enemyStartingAttackTimer * (1 / multiplier);
        enemy.animator.SetFloat("SpeedChanger", enemy.updatingSpeedChangerAnim);
        //Debug.Log(enemy.updatingSpeedChangerAnim);
    }

    public void UpdateState(EnemyMain enemy)
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.detectionRange, enemy.unitLayer);
            if (unitsInRange.Length == 1)
            {
                Debug.Log(444);
                enemy.transformTargetOne = unitsInRange[0].transform;
                enemy.SwitchState(new PositioningRuinedWitch());
            }
            else if (unitsInRange.Length >= 2)
            {
                float secondLowestHealth = float.MaxValue;
                float lowestHealth = float.MaxValue;
                Transform lowestHealthEnemy = null;
                Transform secondLowestHealthEnemy = null;
                foreach (Collider2D unit in unitsInRange)
                {
                    Healthbar healthbar = unit.GetComponentInChildren<Healthbar>();
                    if (healthbar != null && healthbar.hp > 0f)
                    {
                        if (healthbar.hp < lowestHealth)
                        {
                            secondLowestHealth = lowestHealth;
                            secondLowestHealthEnemy = lowestHealthEnemy;
                            lowestHealth = healthbar.hp;
                            lowestHealthEnemy = unit.transform;
                        }
                        else if (healthbar.hp < secondLowestHealth)
                        {
                            secondLowestHealth = healthbar.hp;
                            secondLowestHealthEnemy = unit.transform;
                        }
                    }
                    // Position (check, if 1 TARGET1: do so if TARGET1 AND 2 do so -> Attack  -> TARGET2 NULL -> PatrolState ->
                    // AreaAttack (check if there is a TARGET1) -> TARGET1 NULL -> Idle (if 2 cast, cast twice)
                }

                if (Mathf.Abs(lowestHealthEnemy.position.x - secondLowestHealthEnemy.position.x) < 20f)
                {
                    enemy.transformTargetOne = lowestHealthEnemy;
                    enemy.transformTargetTwo = secondLowestHealthEnemy;
                    enemy.SwitchState(new PositioningRuinedWitch());

                }
                else
                {
                    enemy.transformTargetOne = lowestHealthEnemy;
                    enemy.SwitchState(new PositioningRuinedWitch());
                }


            }
            else
            {
                //enemy.SwitchState(new PatrolStateVoidbone()); // -> AreaAttack -> Idle 
            }
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        //enemy.animator.ResetTrigger("IdleTrigger");
    }
}
