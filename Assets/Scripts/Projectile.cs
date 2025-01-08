using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private ProjectileVisual projectileVisual;



    private float damageProjectile = 10;

    private Transform target;
    private float moveSpeed;
    private float maxMoveSpeed;
    private float trajectoryMaxRelativeHeight;
    private float distanceToTargetToDestroyProjectile = 1f;

    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve projectileSpeedAnimationCurve;

    private Vector3 trajectoryStartPoint;

    private Vector3 projectileMoveDir;

    private Vector3 trajectoryRange;

    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;

    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;
    //private bool setSkillEffectBool = false;
    //private bool fromUnitBool = false;
    //private bool fromEnemyBool = false;



    private void Start() {
    
        trajectoryStartPoint = transform.position;
        
    }
    private void Update()
    {
        UpdateProjectilePosition();


        if(Vector3.Distance(transform.position, target.position) < distanceToTargetToDestroyProjectile)
        {
            if (target.CompareTag("enemyBasic"))
            {
                EnemyMain enemyComponent = target.GetComponent<EnemyMain>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(damageProjectile);
                }
            }
            else if (target.CompareTag("unitBasic"))
            {
                Unit unit = target.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.TakeDamage(damageProjectile);
                }
            }
            



            Destroy(gameObject);



        }

        
    }

    private void UpdateProjectilePosition()
    {
        

        
        trajectoryRange = target.position - trajectoryStartPoint;

        if(Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            if (trajectoryRange.y < 0) { moveSpeed = -moveSpeed; }

            UpdatePositionWithXCurve();


        }
        else
        {
            if (trajectoryRange.x < 0) { moveSpeed = -moveSpeed; }

            UpdatePositionWithYCurve();
        }
        
    }

    private void UpdatePositionWithYCurve()
    {
        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);

        nextYTrajectoryPosition = nextPositionYNormalized * trajectoryMaxRelativeHeight;




        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;

        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionXNormalized);
        projectileMoveDir = newPosition - transform.position;
        transform.position = newPosition;

    }

    private void UpdatePositionWithXCurve()
    {
        float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;

        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);

        nextXTrajectoryPosition = nextPositionXNormalized * trajectoryMaxRelativeHeight;




        float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * trajectoryRange.x;


        if(trajectoryRange.x > 0 &&  trajectoryRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextPositionX  = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionYNormalized);
        projectileMoveDir = newPosition - transform.position;
        transform.position = newPosition;

    }

    private void CalculateNextProjectileSpeed(float nextPositionXNormalized)
    {
        float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
    }


    public void InitializeProjectile(Transform target, float maxMoveSpeed, float trajectoryMaxHeight)
    {
        this.target = target;
        this.maxMoveSpeed = maxMoveSpeed;

        float xDistanceToTarget = target.position.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget)* trajectoryMaxHeight;   
        projectileVisual.SetTarget(target);

    }

    public void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
        this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;   
        this.projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;
    }

    public Vector3 GetProjectileMoveDir()
    {
        return projectileMoveDir;
    }

    public float GetNextYTrajectoryPosition()
    {
        return nextYTrajectoryPosition;

    }

    public float GetNextYPositionCorrectionAbsolute()
    {
        return nextPositionYCorrectionAbsolute;
    }

    public float GetNextXTrajectoryPosition()
    {
        return nextXTrajectoryPosition;

    }

    public float GetNextXPositionCorrectionAbsolute()
    {
        return nextPositionXCorrectionAbsolute;
    }

    public void damageProjectileSet(float a)
    {
        this.damageProjectile = a;
    }
    
    //public void SetSkillEffect()
    //{
    //    this.setSkillEffectBool = true;
    //}

    //public void FromUnit()
    //{
    //    this.fromUnitBool = true;
    //}

    //public void FromEnemy()
    //{
    //    this.fromEnemyBool = true;
    //}




}

