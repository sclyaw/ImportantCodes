using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySoldierUnit : Unit
{



    public SkillData skillData;






    // Start is called before the first frame update
    public override void Attack()
    {

        Debug.Log($"Current Time: {Time.deltaTime}, Last Attack Time: {attackTimer}, Attack Cooldown: {attackCooldown}");

        //transformTarget


        Collider2D[] enemiesInRangeAttack = Physics2D.OverlapCircleAll(transform.position, currentRange, enemyLayer);

        foreach (Collider2D enemyColliderAttack in enemiesInRangeAttack)
        {
            Transform enemyTransformAttack = enemyColliderAttack.transform;
            GameObject enemyToAttack = enemyTransformAttack.gameObject;
            EnemyMain enemyToAttackScript = enemyToAttack.GetComponent<EnemyMain>();

            Vector3 attackHitEffectPos = new Vector3(enemyTransformAttack.position.x, enemyTransformAttack.position.y, -2); // z degerini duzelttim 
            GameObject hitEffect = Instantiate(hitEffectPrefab, attackHitEffectPos, Quaternion.identity);
            if (hitEffect != null)
            {
               

                Animator hitAnimator = hitEffect.GetComponent<Animator>();

                if (hitAnimator != null)
                {
                    Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }

                //if (isAttackCancelled) return;

                if (enemyToAttackScript != null) {
                    enemyToAttackScript.TakeDamage(attackDamage);
                }
            }          
        }
        idleCheckSituationBool = false;
    }


    public override void UseSkill(int skillID)
    {
        if (skillID == 0) { return; }
        if (skillID == 1) { return; }
        if (skillID == 2) { return; }
    }

    public override void ChangeCastBar()
    {

    }
}

