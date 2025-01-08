using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrimlord : EnemyMain
{

    //public float attackTimerSkullCrusher;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    public bool boolShot;
    public float throwTimer;
    public Vector3 finishPoint;
    public Vector3 startingPoint;
    public float grimlordThrowTimer;
    public float grimlordTripleTimer;
    public float grimlordSpecialTimer;

    public GameObject objectToSpawn;
    private bool tripleAttack = false;
    private bool specialAttackGrimlord =false;
    public float specialChance = 0.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bullet = 1f;
        attackRange = 3.5f; // Yakýn dövüþ mesafesi
        idleTimer = 2f;
        attackDamage = 50f;
        detectionRange = 1000f;
        startingSpeedChangerSpeed = animator.GetFloat("SpeedChanger");
        string animationName = "GrimlordAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                Debug.Log(clip.length);
                Debug.Log(startingSpeedChangerSpeed);
                enemyStartingAttackTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
            if (clip.name == "GrimlordThrow")
            {
                Debug.Log(clip.length);
                Debug.Log(startingSpeedChangerSpeed);
                grimlordThrowTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
            if (clip.name == "GrimlordTripleAttack")
            {
                grimlordTripleTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
            if(clip.name == "GrimlordSpecialAttack")
            {
                grimlordSpecialTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
        }
        SwitchState(new IdleStateGrimlord());


        myEnemyHealthbar.AddShield(0);
    }

    public override void SpecialAbility()
    {
        //if (boolShot)
        //{
        //    Instantiate(objectToSpawn, finishPoint, Quaternion.identity);
        //}
        //else
        //{
        //    if (specialAttackNumber == 1) // tek vurus
        //    {


        //    }
        //    else if (specialAttackNumber == 2) //3lu vurus
        //    {


        //    }
        //    else if (specialAttackNumber == 3)
        //    { //throw

        //        StartCoroutine(ThrowRoutine());
        //        bullet -= 1f;
        //    }
        //}

        if (specialAttackNumber == 1) // tek vurus
        {
            
            Debug.Log("AttackState");
            if(!tripleAttack)
            {
                float healthPercentage = myEnemyHealthbar.hp / myEnemyHealthbar.GetMaxHp();
                Debug.Log(healthPercentage);    
                if (healthPercentage < 0.5)
                {
                    animator.ResetTrigger("IdleTrigger");
                    animator.ResetTrigger("AttackTrigger");
                    animator.ResetTrigger("RunTrigger");
                    animator.SetTrigger("TripleAttackTrigger");
                    
                    StartCoroutine(TripleAttackRoutine());
                }
                else
                {
                    animator.ResetTrigger("IdleTrigger");
                    animator.ResetTrigger("RunTrigger");
                    animator.SetTrigger("AttackTrigger");
                    StartCoroutine(AttackStateRoutine());
                }
                
            }
            else
            {

                animator.ResetTrigger("IdleTrigger");
                animator.ResetTrigger("RunTrigger");
                animator.SetTrigger("AttackTrigger");
                StartCoroutine(AttackStateRoutine());
            }
           
            

        }
        else if (specialAttackNumber == 2) //3lu vurus
        {


        }
        else if (specialAttackNumber == 3)
        { //throw

            StartCoroutine(ThrowRoutine());
            bullet -= 1f;
        }






    }

    IEnumerator ThrowRoutine()
    {
        Debug.Log("ThrowRoutine");
        yield return new WaitForSeconds(grimlordThrowTimer - 0.2f);

        if (transformTargetOne != null)
        {
            startingPoint = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
            Projectile projectile = Instantiate(projectilePrefab, startingPoint, Quaternion.identity).GetComponent<Projectile>();
            projectile.damageProjectileSet(50);
            if (transform.localScale.x < 0)
            {
                finishPoint = new Vector3(transformTargetOne.position.x + 1f, transformTargetOne.position.y, 0);  
            }
            else
            {

                finishPoint = new Vector3(transformTargetOne.position.x - 1f, transformTargetOne.position.y, 0);
            }

            GameObject invisibleObject = new GameObject("InvisibleTarget");
            invisibleObject.transform.position = finishPoint;


            //projectile.SetSkillEffect();
            //projectile.FromEnemy();
            projectile.InitializeProjectile(invisibleObject.transform, projectileMaxMoveSpeed, trajectoryMaxHeight);
            projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
            boolShot = true;

            SwitchState(new PositioningStateGrimlord());

            while (projectile != null) {
                yield return null;
            }

            GameObject groundSkill = Instantiate(objectToSpawn, finishPoint, Quaternion.identity);
            yield return new WaitForSeconds(3f);
            Destroy(invisibleObject);
            yield return new WaitForSeconds(10f);
            Destroy(groundSkill);
        }
        
    }

    IEnumerator AttackStateRoutine() {

        Debug.Log(enemyStartingAttackTimer);
    
        yield return new WaitForSeconds(enemyStartingAttackTimer-0.15f);

        Debug.Log(Vector2.Distance(transform.position, transformTargetOne.position));
        Debug.Log(attackRange);

        if(Vector2.Distance(transform.position, transformTargetOne.position) <= attackRange)
        {

            Unit unitTransform = transformTargetOne.GetComponent<Unit>();
            unitTransform.TakeDamage(100);
        }

       
        if (Random.value < specialChance)
        {
           

            animator.ResetTrigger("AttackTrigger");
            animator.SetTrigger("SpecialTrigger");
            yield return new WaitForSeconds(grimlordSpecialTimer);
            myEnemyHealthbar.hp += 100;

            if (Vector2.Distance(transform.position, transformTargetOne.position) <= attackRange)
            {

                Unit unitTransform = transformTargetOne.GetComponent<Unit>();
                unitTransform.TakeDamage(100);
            }
        }
        



        SwitchState(new IdleStateGrimlord());



    }

    IEnumerator TripleAttackRoutine()
    {
        Debug.Log("Triple");
        yield return new WaitForSeconds(grimlordTripleTimer/3);
        if (Vector2.Distance(transform.position, transformTargetOne.position) <= attackRange)
        {

            Unit unitTransform = transformTargetOne.GetComponent<Unit>();
            unitTransform.TakeDamage(20);
        }
        yield return new WaitForSeconds(grimlordTripleTimer / 3);
        if (Vector2.Distance(transform.position, transformTargetOne.position) <= attackRange)
        {

            Unit unitTransform = transformTargetOne.GetComponent<Unit>();
            unitTransform.TakeDamage(20);
        }


        yield return new WaitForSeconds(grimlordTripleTimer / 3);
        if (Vector2.Distance(transform.position, transformTargetOne.position) <= attackRange)
        {

            Unit unitTransform = transformTargetOne.GetComponent<Unit>();
            unitTransform.TakeDamage(20);
        }

        tripleAttack = true;
        SwitchState(new IdleStateGrimlord());



    }


    public override void StartStunState()
    {

        if (!stunnedBool) 
        {
            SwitchState(new StunnedStateGrimlord());
            stunnedBool = true;
        }
        else
        {
            SwitchState(new IdleStateGrimlord());
            stunnedBool = false;
        }
      
    }

    public override void DeathStateFunc()
    {
        SwitchState(new DeathStateGrimlord());
    }
}



