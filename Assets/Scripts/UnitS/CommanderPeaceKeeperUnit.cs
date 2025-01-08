using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderPeaceKeeperUnit : Unit


{

    

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

    public override void Attack()
    {

        Debug.Log($"Current Time: {Time.deltaTime}, Last Attack Time: {attackTimer}, Attack Cooldown: {attackCooldown}");
              
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.damageProjectileSet(attackDamage);
        projectile.InitializeProjectile(transformTarget, projectileMaxMoveSpeed, trajectoryMaxHeight);
        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);


        idleCheckSituationBool = false;

        





        //hitAnimator.Play("hitEffectMechazorAnim");




        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!





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
