using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateClawling : IEnemyState
{
    private float[] attackTimers = { 0.2f, 0.4f, 0.6f };
    private int currentAttackIndex = 0;

    public LayerMask targetLayer; // Hedef layer
    public Vector2 boxSize = new Vector2(3f, 0.5f); // Dikd�rtgenin geni�lik ve y�ksekli�i

    public Vector2 clawBoxSize = new Vector2(1f, 0.5f);

    public float attackTimerClawling;


    public void EnterState(EnemyMain enemy)
    {
        enemy.animator.SetTrigger("AttackTrigger");

        // Animasyon s�resini hesapla
        string attackAnimation = "clawlingAttack";
        AnimationClip[] clips = enemy.animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == attackAnimation)
            {
                float attackDuration = clip.length;
                attackTimerClawling = attackDuration;
                break;
            }
        }
    }

    public void UpdateState(EnemyMain enemy)
    {

        attackTimerClawling += Time.deltaTime;

        // S�radaki sald�r�y� tetikleme
        if (currentAttackIndex < attackTimers.Length && attackTimerClawling >= attackTimers[currentAttackIndex])
        {
            PerformClawAttack(enemy);
            currentAttackIndex++;
        }

        if (currentAttackIndex >= attackTimers.Length)
        {
            enemy.SwitchState(new ChaseStateClawling());
        }

    }

    public void ExitState(EnemyMain enemy)
    {
        enemy.animator.ResetTrigger("AttackTrigger");
    }

    private void PerformClawAttack(EnemyMain enemy)
    {
        // Dikd�rtgen alan� belirle
        Vector2 boxCenter = enemy.transform.position;
        if (enemy.transform.localScale.x < 0) // Sol tarafa bak�yorsa
            boxCenter.x -= clawBoxSize.x / 2f;
        else
            boxCenter.x += clawBoxSize.x / 2f;

        // �ak��malar� kontrol et
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, clawBoxSize, 0f, enemy.unitLayer);
        foreach (var hit in hits)
        {
            var unit = hit.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(enemy.attackDamage);
            }
        }

        // Pen�e sald�r�s�n� g�rselle�tirme (iste�e ba�l�)
        Debug.DrawLine(boxCenter + new Vector2(-clawBoxSize.x / 2, clawBoxSize.y / 2), boxCenter + new Vector2(clawBoxSize.x / 2, clawBoxSize.y / 2), Color.red, 0.5f);
        Debug.Log("Pen�e sald�r�s� yap�ld�!");
    }
}