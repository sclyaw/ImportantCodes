using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateSkullCrasher : IEnemyState
{
    

    float attackStateSkullCrasherTimer;

    public Vector3 rayOrigin; // Raycast'in baþlangýç noktasý
    //public float radius = 0.5f; // Çemberin yarýçapý
    public LayerMask targetLayer;

    public void EnterState(EnemyMain enemy)
    {
        Debug.Log("AttackStateSkullCrasher");
        enemy.animator.SetTrigger("AttackTrigger");


        attackStateSkullCrasherTimer = enemy.attackTimer * (1/enemy.updatingSpeedChangerAnim);




        //tek 1 kere de yapýlýrr


    }

    public void UpdateState(EnemyMain enemy)
    {
        attackStateSkullCrasherTimer -= Time.deltaTime;


        Debug.Log(attackStateSkullCrasherTimer);

        if (0.3 < attackStateSkullCrasherTimer && attackStateSkullCrasherTimer <= 0.5)
        {
            enemy.animator.ResetTrigger("AttackTrigger");
            enemy.animator.SetTrigger("IdleTrigger");
            
            if (enemy.transform.localScale.x > 0)
            {  rayOrigin = new Vector3(enemy.transform.position.x+0.5f, enemy.transform.position.y, enemy.transform.position.z); }
            else if (enemy.transform.localScale.x < 0)
            {  rayOrigin = new Vector3(enemy.transform.position.x-0.5f, enemy.transform.position.y, enemy.transform.position.z); }

           

            Collider2D[] hits = Physics2D.OverlapCircleAll(rayOrigin, 0.5f, enemy.unitLayer);
            foreach (var hit in hits)
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(rayOrigin, (hit.transform.position - rayOrigin ).normalized, 0.5f, enemy.unitLayer);


                if (raycastHit.collider != null)
                {
                    var unit = raycastHit.collider.GetComponent<Unit>();
                    unit.TakeDamage(enemy.attackDamage);
                }
            }
        }else if(attackStateSkullCrasherTimer <= 0.3)
        {
            enemy.SwitchState(new IdleStateToLowestHealth());
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        
       
    }

    

    
}