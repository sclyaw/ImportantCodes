using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullCrusher : EnemyMain
{

    //public float attackTimerSkullCrusher;




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackRange = 1.5f; // Yakýn dövüþ mesafesi
        idleTimer = 2f;
        attackDamage = 10f;

        startingSpeedChangerSpeed = animator.GetFloat("SpeedChanger");
        string animationSkullCrasherAttack = "skullCrusherAttack";
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationSkullCrasherAttack)
            {

                Debug.Log(45434);
                Debug.Log(clip.length);
                Debug.Log(startingSpeedChangerSpeed);
                attackTimer = (clip.length * 1/startingSpeedChangerSpeed);
            }
        }



        SwitchState(new IdleStateToLowestHealth());
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
