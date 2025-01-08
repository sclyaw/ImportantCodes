using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRuinedWitch : EnemyMain
{


    public bool boolShot;
    [SerializeField] public GameObject enemyRuinedWitchEffectPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackRange = 1.5f; // Yakýn dövüþ mesafesi
        idleTimer = 2f;
        attackDamage = 1f;
        detectionRange = 1000f;
        startingSpeedChangerSpeed = animator.GetFloat("SpeedChanger");
        string animationRuinedWitchAttack = "RuinedWitchAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationRuinedWitchAttack)
            {
                Debug.Log(clip.length);
                Debug.Log(startingSpeedChangerSpeed);
                enemyStartingAttackTimer = (clip.length * 1 / startingSpeedChangerSpeed);
            }
        }
        SwitchState(new IdleStateRuinedWitch());
    }


    public override void SpecialAbility()
    {

    //    if (!boolShot && transformTargetOne != null)
    //    {
    //        if (transformTargetOne != null)
    //        {
    //            Vector3 attackHitEffectPos = new Vector3(transformTargetOne.position.x, transformTargeOne.position.y, -2); // z degerini duzelttim 
    //            GameObject hitEffect = Instantiate(enemyRuinedWitchEffectPrefab, attackHitEffectPos, Quaternion.identity);
    //            if (hitEffect != null) {
    //                Animator effectAnimator = hitEffect.GetComponent<Animator>();
    //                if (effectAnimator != null) {
    //                    Destroy(hitEffect, effectAnimator.GetCurrentAnimatorStateInfo(0).length);
    //                }
    //            }

    //        }

    //        boolShot = true;
    //    }

    //    else if (boolShot && transformTargetOne != null)
    //    {
    //        if (transformTargetOne != null)
    //        {
    //            Vector3 attackHitEffectPos = new Vector3(transformTargetOne.position.x, transformTargeOne.position.y, -2); // z degerini duzelttim 
    //            GameObject hitEffect = Instantiate(enemyRuinedWitchEffectPrefab, attackHitEffectPos, Quaternion.identity);
    //            if (hitEffect != null)
    //            {
    //                Animator effectAnimator = hitEffect.GetComponent<Animator>();
    //                if (effectAnimator != null)
    //                {
    //                    Destroy(hitEffect, effectAnimator.GetCurrentAnimatorStateInfo(0).length);
    //                }
    //            }

    //        }
    //        boolShot = false;

    //    }
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
