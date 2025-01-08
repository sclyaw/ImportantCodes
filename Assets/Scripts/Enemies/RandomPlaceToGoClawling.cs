using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaceToGoClawling : IEnemyState
{

    Vector3 randomPointHere;
    public float escapeRange = 4f;

    public void EnterState(EnemyMain enemy)
    {

        float randomX = Random.Range(enemy.transform.position.x - 4f, enemy.transform.position.x + 4f);

        float randomY = Random.Range(enemy.transform.position.y - 4f, enemy.transform.position.y + 4f);
        randomPointHere = new Vector3(randomX, randomY,0f);

        enemy.animator.ResetTrigger("AttackTrigger");
        enemy.animator.SetTrigger("RunTrigger");



    }

    public void UpdateState(EnemyMain enemy)
    {
        if (enemy.IsWalkable(randomPointHere) && enemy.IsObstacleAtPoint(randomPointHere) && enemy.IsPointReachable(randomPointHere))
        {
            enemy.MoveTo(randomPointHere);
        }
        else
        {
            enemy.SwitchState(new IdleClawling());
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("RunTrigger");
    }


}