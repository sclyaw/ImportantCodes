using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public abstract class Unit : MonoBehaviour
{
    //[SerializeField] private float attackRange = 100f;

    public abstract void Attack();
    
    public float myStartingSpeedChangerUnit;

    public abstract void UseSkill(int skillID);
    public abstract void ChangeCastBar();

    public float attackRange = 7f;
    public float attackDamage = 10f;

    //private float lastAttackTime = 0f;
    public float attackTimer;

    public float defaultAttackTimer = 1.55f;
    public Transform target;
    public bool enemyBool = false;



    public float moveSpeed = 0.1f;
    public bool isMoving = false;



    public bool isCasting = false;


    public bool haveActiveSkill= false; 

    



    public GameObject attackRangeIndicator;

    public float currentRange;

    public float defaultSize = 1f;

    public float fadeDuration = 0.5f;



    [SerializeField] public float attackCooldown;

    public Animator animator;


    public bool MoveTowardsLockBool = false;




    public Transform transformTarget;
    public Transform temporaryTransformTarget;
    public float distanceToTarget;

    public bool idleCheckSituationBool;

    public LayerMask enemyLayer; 



    public Vector3 transformGround; 
    public float distanceToTargetGround;

    public GameObject markerPrefab;

    [SerializeField]public GameObject hitEffectPrefab;

    public UnitSkills unitSkills;


    public unitControl unitControlSystem;


    public float totalDamageDealt;

    public Healthbar myUnitHealthbar;

    private List<Effect> activeEffects = new List<Effect>();



    public SkillUIManager skillUIManager;

    public int strength; // Fiziksel güç
    public int agility;  // Hýz ve çeviklik
    public int intelligence; // Zeka

    public float healthRegenRate;


   



    private void Awake()
    {
        currentRange = attackRange;
        enemyBool = false;
        idleCheckSituationBool = false;
        transformGround = Vector3.zero;
        attackTimer = defaultAttackTimer;
        unitControlSystem = GameObject.Find("systemManager").GetComponent<unitControl>();
        totalDamageDealt = 0;
        skillUIManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillUIManager>();

        





    }

    protected virtual void Start()
    {

        animator = GetComponent<Animator>();


        myUnitHealthbar = GetComponentInChildren<Healthbar>();



        attackRangeIndicator.SetActive(false);

        
        SetAttackRangeIndicatorSize();
        myStartingSpeedChangerUnit = animator.GetFloat("SpeedChanger");

        


    }

    protected virtual void Update()
    {

        if (attackTimer < defaultAttackTimer)
        {
            //Debug.Log("attackTimerDefault");
        }
              

        if(transformGround != Vector3.zero && !isCasting)
        {
            transformTarget = null;

            animator.ResetTrigger("AttackTrigger");
            animator.ResetTrigger("IdleTrigger");
            animator.SetTrigger("RunTrigger");


            transformGround.z = transform.position.z;

            if (transformGround.x - transform.position.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transformGround.x - transform.position.x <= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            transform.position = Vector3.MoveTowards(transform.position, transformGround, moveSpeed * Time.deltaTime);
            
            

            idleCheckSituationBool = false;

            // Karakter hedefe ulaþtýysa hareketi durdur
            if (Vector3.Distance(transform.position, transformGround) < 0.1f)
            {               
                ClearTransformGround();
            }

        }
        if (transformTarget != null && !isCasting)
        {

            if (transformTarget.position.x - transform.position.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transformTarget.position.x - transform.position.x <= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }






            if (Vector3.Distance(transform.position, transformTarget.position) <= attackRange - 1)
            {
                
                

                if (transformTarget != null)
                {
                    
                    idleCheckSituationBool = false;

                }
                else
                {
                    ClearTarget();
                    if(transformGround != Vector3.zero)
                    {
                        idleCheckSituationBool = true;


                    }
                }



                attackTimer -= Time.deltaTime;

                

                animator.ResetTrigger("IdleTrigger");
                animator.ResetTrigger("RunTrigger");
                animator.SetTrigger("AttackTrigger");



                if (attackTimer <= 0)
                {

                    

                    //&& Vector2.Distance(transform.position, target.transform.position) <= attackRange
                    if (transformTarget != null)
                    {
                        Attack();
                        totalDamageDealt += attackDamage;
                        attackTimer = defaultAttackTimer;
                    }
                    
                    




                }


            }
            else if (Vector3.Distance(transform.position, transformTarget.position) > attackRange - 1)
            {
                idleCheckSituationBool = false;
                attackTimer = defaultAttackTimer;

                animator.ResetTrigger("AttackTrigger");
                animator.ResetTrigger("IdleTrigger");
                animator.SetTrigger("RunTrigger");


                if (transformTarget.position.x - transform.position.x > 0)
                {
                    //Debug.Log("fdfgfdgr");
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (transformTarget.position.x - transform.position.x >= 0)    
                {
                    //Debug.Log("elsefdsfggfd");
                    GetComponent<SpriteRenderer>().flipX = true; // Sola bak
                }

                Vector3 moveDirNormalized = (transformTarget.position - transform.position).normalized;
                transform.position += moveDirNormalized * moveSpeed * Time.deltaTime;
            }

        } else if (transformTarget == null&& transformGround == Vector3.zero) {
            animator.ResetTrigger("AttackTrigger");
            animator.SetTrigger("IdleTrigger");
            animator.ResetTrigger("RunTrigger");

            if (transformGround == Vector3.zero) {
                idleCheckSituationBool = true;
                
            }
        }

        if(idleCheckSituationBool == true) {
            idleCheckSituation();           
        }

        if (transformTarget != null)
        {
            if (transformTarget.gameObject.tag == "Dead") { transformTarget = null; }
        }



       
    }

    

    public void SetTarget(Transform transformTarget) {
        this.transformTarget = transformTarget;
        attackTimer = defaultAttackTimer;    
        enemyBool = true;
        unitControlSystem.OpenSelectedEnemyMarker(transformTarget);
    }

    public void ClearTarget() {     
        transformTarget = null;
        enemyBool = false;
        animator.ResetTrigger("AttackTrigger");
        animator.SetTrigger("IdleTrigger");
        animator.ResetTrigger("RunTrigger");
        attackTimer = defaultAttackTimer;
        unitControlSystem.CloseSelectedEnemyMarker();
    }

    void OnMouseEnter()
    {
        // Fare objenin üzerine geldiðinde menzil objesini göster

        currentRange = attackRange;
        //Debug.Log(currentRange);
        //Debug.Log(defaultSize);


        attackRangeIndicator.SetActive(true);
        attackRangeIndicator.transform.localScale = new Vector3(currentRange * defaultSize, currentRange * defaultSize, 1);
        StartCoroutine(FadeIn());


        //Debug.Log("IM STUPID");

    }

    void OnMouseExit()
    {
        // Fare objeden çýktýðýnda menzil objesini gizle
        attackRangeIndicator.SetActive(false);
        StartCoroutine(FadeOut());
    }


    void SetAttackRangeIndicatorSize()
    {
        // Menzil objesinin boyutunu karakterin menzil deðerine göre ayarla
        // Örneðin, menzil objesinin bir daire olduðunu varsayalým
        // Transform boyutunu deðiþtirmek için "localScale" kullanabilirsiniz

        float size = attackRange;
        Debug.Log(size);
        attackRangeIndicator.transform.localScale = new Vector3(size, size, 1); // 2D için z eksenini 1 olarak býrakýyoruz
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer spriteRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        UnityEngine.Color color = spriteRenderer.color;
        float elapsedTime = 0f;


        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }

        Debug.Log("düzelt");


    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        UnityEngine.Color color = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            spriteRenderer.color = color; // Alpha deðerini güncelle
            yield return null;
        }
    }

    public void SetMoveTowardsLock(Transform transformTarget, float distanceToTarget)
    {

        this.transformTarget = transformTarget;
        this.distanceToTarget = distanceToTarget;
        MoveTowardsLockBool = true;
    }

    public void SetTransformGround(Vector3 transformGround)
    {  
        this.transformGround = transformGround;
        attackTimer = defaultAttackTimer;
        unitControlSystem.OpenSelectedGroundMarker(transformGround);
        //GameObject marker = Instantiate(markerPrefab, transformGround, Quaternion.identity);
        //Destroy(marker, 1f);

    }

    public void ClearTransformGround()
    {
        //distanceToTargetGround = Vector3.Distance(transform.position, transformGround);
        transformGround = Vector3.zero;
        unitControlSystem.ClearSelectedGroundMarker();
    }


    public void idleCheckSituation()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, currentRange, enemyLayer);
        if (enemiesInRange.Length > 0) {
            Collider2D lowestHealthEnemy = null;
            float lowestHealth = float.MaxValue;
            foreach (Collider2D enemy in enemiesInRange) {        
                float health = enemy.GetComponentInChildren<Healthbar>().hp;
                if (health != 0) {
                    if (health < lowestHealth) {
                        lowestHealth = health;
                        lowestHealthEnemy = enemy;
                    }
                }
                if (lowestHealthEnemy != null) {                                     
                    transformTarget = lowestHealthEnemy.transform;
                }

            }
        } else {
            //Debug.Log("Düþman yok.");
        }
    }



    public void TakeDamage(float damage)
    {
        if(myUnitHealthbar.shieldHp > 0)
        {
            myUnitHealthbar.shieldHp -= damage;
            if(myUnitHealthbar.shieldHp <= 0)
            {
                myUnitHealthbar.shieldHp = 0;
                myUnitHealthbar.SetActiveFalseShield();
            }
        }
        else
        {
            myUnitHealthbar.hp -= damage;
        }
      

    }

    public void TakeHeal(float heal)
    {

        myUnitHealthbar.hp += heal;

    }


    public void AddEffect(Effect newEffect)
    {
        activeEffects.Add(newEffect);
        StartCoroutine(ApplyEffect(newEffect));
    }

    private IEnumerator ApplyEffect(Effect effect)
    {
        switch (effect.effectType)
        {
            case EffectType.AttackRange:
                if (effect.valueChange > 0)
                {
                    Debug.Log("Positive");
                }
                else
                {
                    Debug.Log("Negative");
                }
                currentRange += effect.valueChange;
                break;

        }
        yield return new WaitForSeconds(effect.duration);
        switch (effect.effectType)
        {

            case EffectType.AttackRange:
                currentRange -= effect.valueChange;
                break;
        }
        activeEffects.Remove(effect);
    }

    public bool IsNull(Transform transform)
    {
        if (transform.gameObject.tag == "Dead")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}













