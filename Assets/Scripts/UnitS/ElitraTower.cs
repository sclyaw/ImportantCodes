using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElitraTower : Unit
{

    public SkillData skillData;

    public GameObject objectToSpawn;

    public Image[] ammoSlots;
    public Sprite fullAmmoSprite; // Dolu mermi ikonu
    public Sprite emptyAmmoSprite;
    private int currentAmmo;


    protected override void Start()
    {
        base.Start();
        currentAmmo = ammoSlots.Length;
        UpdateAmmoUI();
    }
    protected override void Update()
    {
        if (transformTarget != null)
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

        if (currentAmmo == 0)
        {
            idleCheckSituationBool = false;
            transformTarget = null;
        }

    }
    public override void Attack()
    {
        
        UseAmmo();
        idleCheckSituationBool = false;

        if (transformTarget != null)
        {
            
            GameObject groundSkill = Instantiate(objectToSpawn, transformTarget.transform.position, Quaternion.identity);
            if (groundSkill != null)
            {

                Animator hitAnimator = groundSkill.GetComponent<Animator>();

                if (hitAnimator != null)
                {
                    Destroy(groundSkill, hitAnimator.GetCurrentAnimatorStateInfo(0).length);
                }
            }

        }
    }

    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
        }
    }

    public void Reload()
    {
        currentAmmo = ammoSlots.Length;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        for (int i = 0; i < ammoSlots.Length; i++)
        {
            if (i < currentAmmo)
            {
                ammoSlots[i].sprite = fullAmmoSprite;
            }
            else
            {
                ammoSlots[i].sprite = emptyAmmoSprite;
            }
        }
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

