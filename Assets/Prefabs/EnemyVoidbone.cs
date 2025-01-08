using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoidbone : EnemyMain
{

    //public float attackTimerSkullCrusher;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    public bool boolShot;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackRange = 1.5f; // Yakýn dövüþ mesafesi
        idleTimer = 2f;
        attackDamage = 1f;
        detectionRange = 1000f;
        startingSpeedChangerSpeed = animator.GetFloat("SpeedChanger");
        string animationVoidboneAttack = "VoidboneAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationVoidboneAttack)
            {
                Debug.Log(clip.length);
                Debug.Log(startingSpeedChangerSpeed);
                enemyStartingAttackTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
        }
        SwitchState(new IdleStateVoidbone());
    }
    public void SpecialAttackSkullCrusher()
    {

    }

    public override void SpecialAbility()
    {
        
        if (!boolShot && transformTargetOne != null)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.damageProjectileSet(50);
            projectile.InitializeProjectile(transformTargetOne, projectileMaxMoveSpeed, trajectoryMaxHeight);
            projectile.InitializeAnimationCurves(trajectoryAnimationCurve,axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
            boolShot = true;
        }else if (boolShot && transformTargetOne != null)
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.damageProjectileSet(50);
            projectile.InitializeProjectile(transformTargetOne, projectileMaxMoveSpeed, trajectoryMaxHeight);
            projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
            boolShot = false;

        }
    }
    public override void StartStunState()
    {
        throw new System.NotImplementedException(); 
    }

    public override void DeathStateFunc()
    {
        throw new System.NotImplementedException();
    }
}
