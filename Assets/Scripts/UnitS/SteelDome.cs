using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelDome : Unit
{

    public LayerMask unitLayer;
    public float detectionRange = 18f;
    public Coroutine spellOneRoutine;
    string animationName;
    public SkillData skillData;
    private float animationDuration;
    public bool spellOnePressed;

   


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;



    protected override void Start()
    {
        base.Start();
        animationName = "SteelDomeAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                animationDuration = (clip.length / 16) * 10;
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


    }

    public void SteelDomeSkill1()
    {
        Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, unitLayer);        
        if (unitsInRange.Length > 0) {
            Collider2D lowestHealthUnit = null;
            float lowestHealth = float.MaxValue;
            foreach (Collider2D unit in unitsInRange) {
                float health = unit.GetComponentInChildren<Healthbar>().hp;
                if (health != 0) {

                    if (health < lowestHealth) {
                        lowestHealth = health;
                        lowestHealthUnit = unit;
                    }
                }
                if (lowestHealthUnit != null) {

                    CastProjectile(lowestHealthUnit.transform);
                }
            }

            
        } else {
            //Debug.Log("Düþman yok.");
        }

    }
    private void CastProjectile(Transform transformUnit)
    {
        if (transformUnit != null) {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.damageProjectileSet(10000);
            projectile.InitializeProjectile(transformUnit, projectileMaxMoveSpeed, trajectoryMaxHeight);
            projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
        }

    }

    public override void UseSkill(int skillID)
    {
        if(skillID == 1) { SteelDomeSkill1(); }
        if (skillID == 0) { return; }
        if (skillID == 2) { return; }
    }

    public override void ChangeCastBar()
    {

    }


}
