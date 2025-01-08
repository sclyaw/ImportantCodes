using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKnightUnit : Unit
{

    string animationName;
    public SkillData skillData;
    private float animationDuration;


    protected override void Start()
    {
        base.Start();

        animationName = $"{GetType().Name}Attack";
        Debug.Log(animationName);

       
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            Debug.Log(clip.name);  
            if (clip.name == animationName)
            {
                animationDuration = (clip.length / 14)*10;
                Debug.Log($"Animasyon: {clip.name}, Süre: {animationDuration} saniye");
                defaultAttackTimer = animationDuration;
                attackTimer = defaultAttackTimer;
                break;
            }
        }


    }

    
    public override void Attack()
    {
        GameObject enemyToAttack = transformTarget.gameObject;
        EnemyMain enemyToAttackScript = enemyToAttack.GetComponent<EnemyMain>();
        if (enemyToAttackScript != null) {
            enemyToAttackScript.TakeDamage(attackDamage);
            this.totalDamageDealt += attackDamage;
        }
        idleCheckSituationBool = false;
        if (transformTarget != null)
        {
            Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y, -2); // z degerini duzelttim 
            GameObject hitEffect = Instantiate(hitEffectPrefab, attackHitEffectPos, Quaternion.identity);
            if (hitEffect != null){
                Animator hitAnimator = hitEffect.GetComponent<Animator>();
                if (hitAnimator != null){
                    Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }
            }
        }

       
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
