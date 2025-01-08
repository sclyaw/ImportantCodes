using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archilaus : Unit
{


    public Coroutine spellOneRoutine;
    string animationName;
    public SkillData skillData;
    private float animationDuration;
    public bool spellOnePressed;

    float elapsedTime = 0f;

    float rate;


    [SerializeField] private GameObject skillPrefab;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;



    protected override void Start()
    {
        base.Start();
        animationName = "ArchilausUnitAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips) {
            Debug.Log(clip.name);
            if (clip.name == animationName) {
                animationDuration = (clip.length / 14) * 10;
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

    

    private IEnumerator ArchilausSkill1()
    {


        haveActiveSkill = true;
        //Debug.Log(spellOnePressed);      
        //Debug.Log(elapsedTime);
        isCasting = true;
        animator.SetTrigger("SkillOneCastTrigger");
        animator.ResetTrigger("AttackTrigger");
        animator.ResetTrigger("IdleTrigger");
        animator.ResetTrigger("RunTrigger");
        spellOnePressed = true;
        //yield return new WaitForSeconds(4);    
        float waitTime = 4f;
        unitControlSystem.OpenCastingBar();

        while (elapsedTime < waitTime) {
            if (!spellOnePressed) // Tuþa tekrar basýlma kontrolü??????
            {
                spellOnePressed = false;
                yield break; // Coroutine sonlandýrýlýr
                
            }
            elapsedTime += Time.deltaTime;
            rate = elapsedTime / waitTime;
            if (unitControlSystem.selectedUnit == this)
            {
                skillUIManager.spellNameCasting.text = this.unitSkills.skills[1].skillName;
                skillUIManager.castingBar.fillAmount = Mathf.Lerp(0, 1, rate);
            } else {
                unitControlSystem.CloseCastingBar();
            }
           
            yield return null;
        }
        StartCoroutine(CastProjectile(elapsedTime));
       
        ResetSkill1();


    }
    private void EarlyCastProjectile(){
        // Erken gönderim sýrasýnda yapýlacak iþlemler
        StartCoroutine(CastProjectile(elapsedTime));
        ResetSkill1();
    }
    private IEnumerator CastProjectile(float elapsedTime) 
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.damageProjectileSet(20 * elapsedTime);
        projectile.InitializeProjectile(transformTarget, projectileMaxMoveSpeed, trajectoryMaxHeight);
        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);

        while (projectile != null)
        {
            yield return null;
        }


        EnemyMain enemySkill= transformTarget.GetComponent<EnemyMain>(); //!!!!!!!!!!!! burda transformtarget deme baska ozel bisi tanýmla
        enemySkill.AddEffect(new Effect(3f, 0f, EffectType.Stun));

        Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y+2f, -2); // z degerini duzelttim 
        GameObject hitEffect = Instantiate(skillPrefab, attackHitEffectPos, Quaternion.identity);
        if (hitEffect != null)
        {
            Animator hitAnimator = hitEffect.GetComponent<Animator>();

            if (hitAnimator != null)
            {
                Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
        }

    }
    private void ResetSkill1() {     
        elapsedTime = 0f;
        spellOnePressed = false;
        isCasting = false;
        if (transformTarget == null){
            idleCheckSituationBool = true;
        }
        attackTimer = defaultAttackTimer;
        haveActiveSkill = false;
        unitControlSystem.CloseCastingBar();

        


    }

    public override void UseSkill(int skillID)
    {
        if (skillID == 0) {
            return;
        }



        if (skillID == 1) {
            
            transformGround = Vector3.zero;
            if (transformTarget != null && !spellOnePressed)
            {
                //Debug.Log("we start");
                spellOneRoutine = StartCoroutine(ArchilausSkill1());
            }
            else if (spellOnePressed && spellOneRoutine != null)
            {
                // Tuþa tekrar   projectile'ý erken gönder
                StopCoroutine(spellOneRoutine);
                EarlyCastProjectile();
            }
        }

        if (skillID == 2) { return; }
    }

    public override void ChangeCastBar()
    {

    }


}