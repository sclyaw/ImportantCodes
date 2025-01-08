using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateClawling : IEnemyState
{

    Vector3 newPoint;
    public float escapeRange = 4f;
    public float distance;

    public void EnterState(EnemyMain enemy)
    {
        
        float multiplier = (enemy.myEnemyHealthbar.hp / enemy.myEnemyHealthbar.GetMaxHp());
        enemy.animator.speed = enemy.animator.speed * multiplier;


        enemy.animator.ResetTrigger("IdleTrigger");
        enemy.animator.SetTrigger("RunTrigger");

        if(enemy.transformTarget != null)
        {
            distance = Vector3.Distance(enemy.transform.position, enemy.transformTarget.position);
        }
        else
        {
            return;
        }
       

        if (distance <= enemy.attackRange) // Eðer hedef çok yakýnsa
        {
            // Hedeften uzaklaþ
            Vector3 directionAway = (enemy.transform.position - enemy.transformTarget.position).normalized;
            newPoint = enemy.transform.position + directionAway * escapeRange;
        }
        else if (distance > enemy.attackRange) // Eðer hedef uzaksa
        {
            // Hedefe yaklaþ
            Vector3 directionTowards = (enemy.transformTarget.position - enemy.transform.position).normalized;
            newPoint = enemy.transform.position + directionTowards * (distance - enemy.attackRange);

        }



    }

    public void UpdateState(EnemyMain enemy)
    {
        if (enemy.transformTarget == null)
        {
            enemy.SwitchState(new RandomPlaceToGoClawling());
            return;
        }
        else
        {
            if(enemy.IsWalkable(newPoint)&& enemy.IsObstacleAtPoint(newPoint) && enemy.IsPointReachable(newPoint))
            {
                enemy.MoveTo(newPoint);
            }
            else
            {
                enemy.SwitchState(new ChaseStateClawling());
            }

    
        }


        float distance = Vector3.Distance(enemy.transform.position, newPoint);

        if (distance <= 0.2)
        {
            enemy.SwitchState(new AttackStateClawling());
        }
       
    }

    public void ExitState(EnemyMain enemy)
    {
        
    }

    
}