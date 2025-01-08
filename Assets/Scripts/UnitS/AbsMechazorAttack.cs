using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsMechazorAttack : Unit
{



    public SkillData skillData;



    


    // Start is called before the first frame update
    public override void Attack()
    {

        Debug.Log($"Current Time: {Time.deltaTime}, Last Attack Time: {attackTimer}, Attack Cooldown: {attackCooldown}");
        GameObject enemyToAttack = transformTarget.gameObject;
        EnemyMain enemyToAttackScript = enemyToAttack.GetComponent<EnemyMain>();
        if (enemyToAttackScript != null)
        {
            enemyToAttackScript.TakeDamage(attackDamage);
        }





        idleCheckSituationBool = false;

        if (transformTarget != null)
        {
            Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y, -2); // z degerini duzelttim 
            GameObject hitEffect = Instantiate(hitEffectPrefab, attackHitEffectPos, Quaternion.identity);   
            if (hitEffect != null)
            {

                Animator hitAnimator = hitEffect.GetComponent<Animator>();

                if (hitAnimator != null)
                {
                    Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }
            }

        }











        //hitAnimator.Play("hitEffectMechazorAnim");




        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!








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
