using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Telyssa : Unit
{
    public Coroutine spellOneRoutine;
    public Coroutine spellTwoRoutine;
    public Coroutine spellThreeRoutine;
    string animationName;
    public SkillData skillData;
    private float animationDuration;
    public bool spellOnePressed;
   
    private float animationDurationSkillOne;
    private float animationDurationSkillTwo;
    private float defaultAttackTimerSkillOne;
    private float defaultAttackTimerSkillTwo;
    private float attackTimerSkillOne;
    private float attackTimerSkillTwo;

    

    [SerializeField] public GameObject SkillTwoEffectPrefab;
    [SerializeField] public GameObject SkillTwoShieldEffectPrefab;
    [SerializeField] public GameObject SkillOneEffectPrefab;





    float elapsedTime = 0f;

    float rate;


    [SerializeField] private GameObject skillPrefab;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    public Coroutine cooldownTwoRoutine;

    

    protected override void Start()
    {
        

        base.Start();
        animationName = "TelyssaAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            Debug.Log(clip.name);
            Debug.Log(clip.name);
            if (clip.name == animationName)
            {
                animationDuration = (clip.length / 14) * 10;
                defaultAttackTimer = animationDuration;
                attackTimer = defaultAttackTimer;
                
            }
            if (clip.name == "TelyssaSkillOne")
            {
                animationDurationSkillOne = (clip.length / 14) * 10;
                Debug.Log(animationDurationSkillOne);
                defaultAttackTimerSkillOne = animationDurationSkillOne;
                Debug.Log(defaultAttackTimerSkillOne);
                attackTimerSkillOne = defaultAttackTimerSkillOne;
                Debug.Log(attackTimerSkillOne);

            }
            if(clip.name == "TelyssaSkillTwo")
            {
                animationDurationSkillTwo = (clip.length / 14) * 10;
                defaultAttackTimerSkillTwo = animationDurationSkillTwo;
                attackTimerSkillTwo = defaultAttackTimerSkillTwo;


            }
        }

        foreach (SkillData skill in unitSkills.skills)
        {
            skill.OnCooldown = false;
            skill.remainingCooldown = 0;
        }

        healthRegenRate = 1;
        strength = 140;


        myUnitHealthbar.StrenghtCalculator(strength);


        myUnitHealthbar.AddShield(100);



        

        


        StartCoroutine(ContinuousHealthRegen());




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



    private IEnumerator TelyssaSkill1()
    {
        haveActiveSkill = true;
        isCasting = true;
        animator.SetTrigger("SkillOneTrigger");
        animator.ResetTrigger("AttackTrigger");
        animator.ResetTrigger("IdleTrigger");
        animator.ResetTrigger("RunTrigger");
        Debug.Log(myStartingSpeedChangerUnit);
        Debug.Log(attackTimerSkillOne);
        float waitTime = (1/myStartingSpeedChangerUnit) * attackTimerSkillOne;
        Debug.Log("waittime:" + waitTime);
        unitControlSystem.OpenCastingBar();
        while (elapsedTime < waitTime)
        {
            
            elapsedTime += Time.deltaTime;
            rate = elapsedTime / waitTime;
            if (unitControlSystem.selectedUnit == this)
            {
                skillUIManager.spellNameCasting.text = this.unitSkills.skills[0].skillName;
                skillUIManager.castingBar.fillAmount = Mathf.Lerp(0, 1, rate);
            }
            else
            {
                unitControlSystem.CloseCastingBar();
            }

            yield return null;
        }

        if (transformTarget != null)
        {
            Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y, -2); // z degerini duzelttim 
            GameObject hitEffect = Instantiate(SkillOneEffectPrefab, attackHitEffectPos, Quaternion.identity);
            if (hitEffect != null)
            {

                Animator hitAnimator = hitEffect.GetComponent<Animator>();

                if (hitAnimator != null)
                {
                    Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }
            }

        }

        EnemyMain transformTargetEnemy = transformTarget.GetComponent<EnemyMain>();
        transformTargetEnemy.TakeDamage(100);
        strength += 5;


        myUnitHealthbar.StrenghtCalculator(strength);

        ResetSkill();

    }
    private IEnumerator TelyssaSkill2()
    {
        haveActiveSkill = true;
        isCasting = true;
        animator.SetTrigger("SkillTwoTrigger");
        animator.ResetTrigger("AttackTrigger");
        animator.ResetTrigger("IdleTrigger");
        animator.ResetTrigger("RunTrigger");
        float waitTime = (1/myStartingSpeedChangerUnit) * attackTimerSkillTwo;
        Debug.Log(waitTime);
        myUnitHealthbar.AddShield(-999);
        unitControlSystem.OpenCastingBar();
        while (elapsedTime < waitTime)
        {
            
            elapsedTime += Time.deltaTime;
            rate = elapsedTime / waitTime;
            if (unitControlSystem.selectedUnit == this)
            {
                skillUIManager.spellNameCasting.text = this.unitSkills.skills[1].skillName;
                skillUIManager.castingBar.fillAmount = Mathf.Lerp(0, 1, rate);
            }
            else
            {
                unitControlSystem.CloseCastingBar();
            }

            yield return null;
        }


        if (transformTarget != null)
        {
            Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y, -2); // z degerini duzelttim 
            GameObject hitEffect = Instantiate(SkillTwoEffectPrefab, attackHitEffectPos, Quaternion.identity);
            if (hitEffect != null)
            {

                Animator hitAnimator = hitEffect.GetComponent<Animator>();

                if (hitAnimator != null)
                {
                    Destroy(hitEffect, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }
            }

        }

        EnemyMain transformTargetEnemy = transformTarget.GetComponent<EnemyMain>();
        transformTargetEnemy.TakeDamage(100);

        cooldownTwoRoutine = StartCoroutine(CastCooldownTwo(unitSkills.skills[1]));



        ResetSkill();



    }


    private IEnumerator TelyssaSkill3()
    {


        haveActiveSkill = true;
        //Debug.Log(spellOnePressed);      
        //Debug.Log(elapsedTime);
        isCasting = true;
        animator.SetTrigger("SkillThreeStartTrigger");
        animator.ResetTrigger("AttackTrigger");
        animator.ResetTrigger("IdleTrigger");
        animator.ResetTrigger("RunTrigger");
        spellOnePressed = true;
        //yield return new WaitForSeconds(4);    
        float waitTime = 4f;
        unitControlSystem.OpenCastingBar();

        while (elapsedTime < waitTime)
        {
            
            elapsedTime += Time.deltaTime;
            rate = elapsedTime / waitTime;
            if (unitControlSystem.selectedUnit == this)
            {
                skillUIManager.spellNameCasting.text = this.unitSkills.skills[2].skillName;
                skillUIManager.castingBar.fillAmount = Mathf.Lerp(0, 1, rate);
            }
            else
            {
                unitControlSystem.CloseCastingBar();
            }

            yield return null;
        }

        StartCoroutine(CastProjectile(elapsedTime));

        ResetSkill3();


    }
    private void EarlyCastProjectile()
    {
        // Erken gönderim sýrasýnda yapýlacak iþlemler
        StartCoroutine(CastProjectile(elapsedTime));
        ResetSkill3();
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


        EnemyMain enemySkill = transformTarget.GetComponent<EnemyMain>(); //!!!!!!!!!!!! burda transformtarget deme baska ozel bisi tanýmla
        enemySkill.AddEffect(new Effect(3f, 0f, EffectType.Stun));

        Vector3 attackHitEffectPos = new Vector3(transformTarget.position.x, transformTarget.position.y + 2f, -2); // z degerini duzelttim 
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
    private void ResetSkill3()
    {
        elapsedTime = 0f;
        spellOnePressed = false;
        isCasting = false;
        if (transformTarget == null)
        {
            idleCheckSituationBool = true;
        }
        attackTimer = defaultAttackTimer;
        haveActiveSkill = false;
        unitControlSystem.CloseCastingBar();

    }

    private void ResetSkill()
    {
        elapsedTime = 0f;
        isCasting = false;
        if (transformTarget == null)
        {
            idleCheckSituationBool = true;
        }
        attackTimer = defaultAttackTimer;
        haveActiveSkill = false;
        unitControlSystem.CloseCastingBar();
    }


    public override void UseSkill(int skillID)
    {
        if (skillID == 0)
        {
            return;
        }



        if (skillID == 1)
        {
            transformGround = Vector3.zero;
            if(transformTarget != null)
            {
                StartCoroutine(TelyssaSkill1());
            }

            
        }

        if (skillID == 2) {
            transformGround = Vector3.zero;
            if (transformTarget != null)
            {
                StartCoroutine(TelyssaSkill2());
            }
        }

        if (skillID == 3) {
            transformGround = Vector3.zero;
            if (transformTarget != null && !spellOnePressed)
            {
                //Debug.Log("we start");
                spellOneRoutine = StartCoroutine(TelyssaSkill3());
            }
            else if (spellOnePressed && spellOneRoutine != null)
            {
                // Tuþa tekrar   projectile'ý erken gönder
                StopCoroutine(spellOneRoutine);
                EarlyCastProjectile();
            }
        }
    }

    public override void ChangeCastBar()
    {

    }




    private IEnumerator CastCooldownTwo(SkillData skill)
    {
        skill.OnCooldown = true;
        skill.remainingCooldown = skill.cooldownTime;

        while (skill.remainingCooldown > 0)
        {
            skill.remainingCooldown -= Time.deltaTime;
            if (unitControlSystem.selectedUnit == this)
            {
                
                skillUIManager.cooldownTextTwo.text = Mathf.Ceil(skill.remainingCooldown).ToString();
                skillUIManager.cooldownImageTwo.fillAmount = skill.remainingCooldown / skill.cooldownTime;

            }
            yield return null;
        }

        skill.OnCooldown = false;
        skillUIManager.cooldownImageTwo.fillAmount = 0;
        skillUIManager.cooldownTextTwo.text = "";


        

        Vector3 shieldEffectPos = new Vector3(transform.position.x, transform.position.y, -2); // z degerini duzelttim 
        GameObject shieldEffect = Instantiate(SkillTwoShieldEffectPrefab, shieldEffectPos, Quaternion.identity);
        if (shieldEffect != null)
        {
            Animator shieldAnimator = shieldEffect.GetComponent<Animator>();

            if (shieldAnimator != null)
            {
                Destroy(shieldEffect, shieldAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            myUnitHealthbar.AddShield(1000);
        }




        


    }


    private IEnumerator ContinuousHealthRegen()
    {
        while (true)
        {
            if (myUnitHealthbar.hp < myUnitHealthbar.GetMaxHp())
            {
                myUnitHealthbar.hp += healthRegenRate;
                //if (stats.currentHealth > stats.maxHealth)
                //{
                //    stats.currentHealth = stats.maxHealth;
                //}
               
            }
            yield return new WaitForSeconds(0.2f); // 0.1 saniye bekle
        }
    }




}