using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateGrimlord : IEnemyState
{

   

    public float timer;
    private bool isSetActiveFalseBool = false;
    private bool healthbarSetActiveFalseBool = false;
    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("DeathStateGrimlord");
        timer = 8f;
        
        enemy.animator.SetTrigger("DeathTrigger");
        enemy.animator.ResetTrigger("StunTrigger");
        enemy.animator.ResetTrigger("ThrowTrigger");
        enemy.animator.ResetTrigger("IdleTrigger");
        enemy.animator.ResetTrigger("TripleAttackTrigger");
        enemy.animator.ResetTrigger("RunTrigger");
        enemy.animator.ResetTrigger("SpecialTrigger");
        enemy.animator.ResetTrigger("AttackTrigger");
    }

    public void UpdateState(EnemyMain enemy)
    {
        timer -= Time.deltaTime;
       
        if (timer <= 7f && !healthbarSetActiveFalseBool){
            enemy.myEnemyHealthbar.gameObject.SetActive(false);
            healthbarSetActiveFalseBool = true;

        }
        else if (timer <= 4f && !isSetActiveFalseBool)
        {
            isSetActiveFalseBool = true;
            enemy.SetActiveFalseFunc();
            


        }else if(timer <= 0)
        {
            enemy.DestroySelf();
            Debug.Log("ffffffffffffffffffffffff");
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        //enemy.animator.ResetTrigger("IdleTrigger");
    }
}



