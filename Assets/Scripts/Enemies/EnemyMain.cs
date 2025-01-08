using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMain : MonoBehaviour
{
    public abstract void SpecialAbility();
    public abstract void StartStunState();
    public abstract void DeathStateFunc();

    protected IEnemyState currentState;


    public float updatingSpeedChangerAnim;
    public float startingSpeedChangerSpeed;

    public Transform player;          
    public float detectionRange = 1000f; 
    public float stopRange = 2f;
    public float attackRange = 10f;
    public bool idleCheckSituationBool;
    NavMeshAgent agent;
    public Vector3 transformGround;
    public float distanceToTargetGround;
    public Animator animator;
    public bool MoveTowardsLockBool = false;
    public Transform transformTargetOne;
    public Transform transformTargetTwo;
    public Transform transformTarget;

    public Transform temporaryTransformTarget;
    public float distanceToTarget;
    public Healthbar myEnemyHealthbar;
    public Healthbar target;
    public float attackTimer;
    public LayerMask unitLayer;
    public float attackDamage = 50f;
    public bool targetBool = false;
    float tempOffset = 5f;
    public float startingHp;
    public float idleTimer;
    public float patrolTimer;

    public float enemyStartingAttackTimer;

    public float specialAttackNumber;

    public float bullet;

    public bool stunnedBool = false;

    private List<Effect> activeEffects = new List<Effect>();

    public GameObject stunIndicatorPrefab;
    private GameObject stunIndicatorInstance;







    public void Awake()
    {


    }

    protected virtual void Start()
    {  
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // 2D sahnelerde d�nme g�ncellemelerini kapat�yoruz
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        myEnemyHealthbar = GetComponentInChildren<Healthbar>();
        startingSpeedChangerSpeed = animator.GetFloat("SpeedChanger");
    }

    protected virtual void Update()
    {
        currentState?.UpdateState(this);

        Vector3 position = transform.position;
        position.z = 0; 
        transform.position = position;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }


    public void ClearPath()
    {
       agent.ResetPath();
    }

    public void TakeDamage(float damage)
    {       
        myEnemyHealthbar.hp -= damage;
           
    }

    public void TakeHeal(float heal) { 
        
        myEnemyHealthbar.hp += heal;
       
    }

    public bool IsPointReachable(Vector3 point)
    {
        NavMeshHit hit;
        // Raycast ile kontrol et
        bool reachable = !NavMesh.Raycast(transform.position, point, out hit, NavMesh.AllAreas);
        return reachable && NavMesh.SamplePosition(point, out hit, 0.1f, NavMesh.AllAreas);
    }

    public bool IsWalkable(Vector3 point)
    {
        NavMeshHit hit;
        float maxDistance = 1.0f; // Hedefe yakın bir NavMesh aralığı
        if (NavMesh.SamplePosition(point, out hit, maxDistance, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }

    public bool IsObstacleAtPoint(Vector3 point, float radius = 0.5f)
    {
        Collider2D collider = Physics2D.OverlapCircle(point, radius, unitLayer);
        return collider != null; // Eğer bir çakışan varsa, engel var demektir
    }


    public void SwitchState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
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
                detectionRange += effect.valueChange;
                break;

            case EffectType.Stun:
                StartStunState();
                break;
        }
        yield return new WaitForSeconds(effect.duration);
        switch (effect.effectType)
        {
           
            case EffectType.Stun:
                StartStunState();
                break;
            case EffectType.AttackRange:
                detectionRange -= effect.valueChange;
                break;
        }
        activeEffects.Remove(effect);
    }


    public void StunIndicatorStart()
    {
        Vector3 StunEffectPos = new Vector3(transform.position.x, transform.position.y + 3.5f, -2);
        stunIndicatorInstance = Instantiate(stunIndicatorPrefab, StunEffectPos, Quaternion.identity, transform);

    }

    public void StunIndicatorEnd()
    {
        Destroy(stunIndicatorInstance);
    }

    public void DestroySelf()
    {
        
        Destroy(this.gameObject);
    }


    public void SetActiveFalseFunc()
    {
        SpriteRenderer myEnemySpriteRenderer= this.GetComponent<SpriteRenderer>();
        myEnemySpriteRenderer.enabled = false;
    }









}

