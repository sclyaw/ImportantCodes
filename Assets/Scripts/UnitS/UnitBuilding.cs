using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnitBuilding : MonoBehaviour
{
  

    public float myStartingSpeedChangerUnit;
    public abstract void Attack();

    public abstract void UseSkill(int skillID);
    public abstract void ChangeCastBar();

    public float attackRange = 7f;
    public float attackDamage = 10f;
    public float attackTimer;
    public float defaultAttackTimer;
    public Transform target;
    public bool enemyBool = false;
    public bool isMoving = false;
    public bool isCasting = false;
    public bool haveActiveSkill = false;
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
    [SerializeField] public GameObject hitEffectPrefab;
    public UnitSkills unitSkills;
    public unitControl unitControlSystem;
    public float totalDamageDealt;
    public Healthbar myUnitHealthbar;
    private List<Effect> activeEffects = new List<Effect>();

    private void Awake()
    {
        currentRange = attackRange;
        enemyBool = false;
        idleCheckSituationBool = false;
        transformGround = Vector3.zero;
        attackTimer = defaultAttackTimer;
        unitControlSystem = GameObject.Find("systemManager").GetComponent<unitControl>();
        totalDamageDealt = 0;

    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();


        myUnitHealthbar = GetComponentInChildren<Healthbar>();
        attackRangeIndicator.SetActive(false);


        SetAttackRangeIndicatorSize();
        myStartingSpeedChangerUnit = animator.GetFloat("SpeedChanger");

    }

    private void Update()
    {
        if (transformTarget != null )
        {
            if (Vector3.Distance(transform.position, transformTarget.position) <= attackRange - 1 && transformTarget.gameObject.tag != "Dead")
            {
                attackTimer -= Time.deltaTime;
                animator.ResetTrigger("IdleTrigger");
                animator.SetTrigger("AttackTrigger");
                if (attackTimer <= 0)
                {
                    Attack();
                    totalDamageDealt += attackDamage;
                    attackTimer = defaultAttackTimer;
                }              
            }
            else if (Vector3.Distance(transform.position, transformTarget.position) > attackRange - 1 || transformTarget.gameObject.tag == "Dead")
            {

                transformTarget = null;
                idleCheckSituationBool = true;
                animator.SetTrigger("IdleTrigger");
                animator.ResetTrigger("AttackTrigger");

            }
            else if (transformTarget == null)
            {
                idleCheckSituationBool = true;
                animator.SetTrigger("IdleTrigger");
                animator.ResetTrigger("AttackTrigger");
            }
        }
        else
        {
            ClearTarget();
            idleCheckSituationBool = true;
        }

        if (idleCheckSituationBool == true)
        {
            idleCheckSituation();
        }
        
    }



    public void SetTarget(Transform transformTarget)
    {
        this.transformTarget = transformTarget;
        attackTimer = defaultAttackTimer;
        enemyBool = true;
        unitControlSystem.OpenSelectedEnemyMarker(transformTarget);
    }

    public void ClearTarget()
    {
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
        if (enemiesInRange.Length > 0)
        {
            Collider2D lowestHealthEnemy = null;
            float lowestHealth = float.MaxValue;
            foreach (Collider2D enemy in enemiesInRange)
            {
                float health = enemy.GetComponentInChildren<Healthbar>().hp;
                if (health != 0)
                {
                    if (health < lowestHealth)
                    {
                        lowestHealth = health;
                        lowestHealthEnemy = enemy;
                    }
                }
                if (lowestHealthEnemy != null)
                {
                    transformTarget = lowestHealthEnemy.transform;
                }

            }
        }
        else
        {
            //Debug.Log("Düþman yok.");
        }
    }



    public void TakeDamage(float damage)
    {
        myUnitHealthbar.hp -= damage;

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
