using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositioningRuinedWitch : IEnemyState
{
    private Vector3 positioningHere;

    public void EnterState(EnemyMain enemy)
    {

        Debug.Log("Positioning");


        if (enemy.transformTargetOne == null && enemy.transformTargetTwo == null)
        {
            Debug.Log(1);
            enemy.SwitchState(new IdleStateVoidbone());
        }
        else if (enemy.transformTargetOne != null && enemy.transformTargetTwo == null)
        {
            Debug.Log(2);
            float randomX = Random.Range(enemy.transformTargetOne.position.x - 3f, enemy.transformTargetOne.position.x + 3f);
            float randomY = Random.Range(enemy.transformTargetOne.position.y - 0.5f, enemy.transformTargetOne.position.y + 0.5f);
            positioningHere = new Vector3(randomX, randomY, 0f);
            enemy.animator.ResetTrigger("RunTrigger");
            enemy.animator.SetTrigger("RunTrigger");

        }
        else if (enemy.transformTargetOne != null && enemy.transformTargetTwo != null)
        {


            if (enemy.transformTargetOne.position.x - enemy.transformTargetTwo.position.y <= 0)
            {

                float distanceToOne = enemy.transform.position.x - enemy.transformTargetOne.position.x;
                float distanceToTwo = enemy.transform.position.x - enemy.transformTargetTwo.position.x;

                if (Mathf.Abs(distanceToOne) > Mathf.Abs(distanceToTwo))
                {

                    float middlePointY = ((enemy.transformTargetOne.position.y + enemy.transformTargetTwo.position.y) / 2);
                    float randomY = Random.Range(middlePointY + 1f, middlePointY - 1f);
                    positioningHere = new Vector3(enemy.transformTargetTwo.position.x + 3f, randomY, 0f);
                    enemy.MoveTo(positioningHere);
                }
                else
                {
                    float middlePointY = ((enemy.transformTargetOne.position.y + enemy.transformTargetTwo.position.y) / 2);
                    float randomY = Random.Range(middlePointY + 1f, middlePointY - 1f);
                    positioningHere = new Vector3(enemy.transformTargetOne.position.x - 3f, randomY, 0f);
                    enemy.MoveTo(positioningHere);
                }
            }
            else
            {
                float distanceToOne = enemy.transform.position.x - enemy.transformTargetOne.position.x;
                float distanceToTwo = enemy.transform.position.x - enemy.transformTargetTwo.position.x;

                if (Mathf.Abs(distanceToOne) > Mathf.Abs(distanceToTwo))
                {

                    float middlePointY = ((enemy.transformTargetOne.position.y + enemy.transformTargetTwo.position.y) / 2);
                    float randomY = Random.Range(middlePointY + 1f, middlePointY - 1f);
                    positioningHere = new Vector3(enemy.transformTargetTwo.position.x - 3f, randomY, 0f);
                    enemy.MoveTo(positioningHere);
                }
                else
                {
                    float middlePointY = ((enemy.transformTargetOne.position.y + enemy.transformTargetTwo.position.y) / 2);
                    float randomY = Random.Range(middlePointY + 1f, middlePointY - 1f);
                    positioningHere = new Vector3(enemy.transformTargetOne.position.x + 3f, randomY, 0f);
                    enemy.MoveTo(positioningHere);
                }
            }
            enemy.animator.ResetTrigger("IdleTrigger");
            enemy.animator.SetTrigger("RunTrigger");
        }
    }

    public void UpdateState(EnemyMain enemy)
    {

        if (Vector3.Distance(enemy.transform.position, positioningHere) < 0.1)
        {
            enemy.SwitchState(new AttackStateRuinedWitch());
        }


    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("RunTrigger");
    }
}
