using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clawling : EnemyMain
{
    public float clawlingAttackTimer;
    public float clawlingAttackTimerDefault;
    //public float attackTimerSkullCrusher;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackRange = 1.5f; // Yakýn dövüþ mesafesi
        idleTimer = 2f;
        attackDamage = 80f;
        SwitchState(new IdleClawling());


        string animationSkullCrasherAttack = "ClawlingAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationSkullCrasherAttack)
            {
                float AnimationDuration = (clip.length / 14) * 10;
                clawlingAttackTimerDefault = AnimationDuration;
                clawlingAttackTimer = clawlingAttackTimerDefault;
                break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

    }

    public void SpecialAttackSkullCrusher()
    {

    }

    public override void SpecialAbility()
    {
        Debug.Log("Melee Enemy: Special attack (e.g., heavy slash)!");
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
