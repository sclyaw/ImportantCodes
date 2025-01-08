using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Image hpImage;
    public Image hpEffectImage;


    [HideInInspector]
    public float hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;


    

    public bool deathBool;

    private bool IsEnemy;


    public float shieldHp;
    public Image shieldHpImage;
    public Image shieldEffectImage;
    public Image emptyShieldImage;
    public float maxShieldHp;

    public bool shieldActiveBool;
    private float previousMaxHp;


    void Awake()
    {
        hp = maxHp;
        deathBool = false;
    }

    void Start()
    {
        if(transform.parent.gameObject.tag == "enemyBasic")
        {
            IsEnemy = true;
        }
        else
        {
            IsEnemy = false;
        }

        
        
    }

    
    void Update()
    {
        hpImage.fillAmount = hp / maxHp;
        if (hpEffectImage.fillAmount  > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }

        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }




        shieldHpImage.fillAmount = shieldHp / maxShieldHp;
        if(shieldEffectImage.fillAmount > shieldHpImage.fillAmount)
        {
            shieldEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            shieldEffectImage.fillAmount = shieldHpImage.fillAmount;
        }




        if(hp <= 0 && !deathBool)
        {

            int deadLayer = LayerMask.NameToLayer("Default");
            transform.parent.gameObject.tag = "Dead";
            transform.parent.gameObject.layer = deadLayer;
            if (IsEnemy)
            {
                EnemyMain myEnemyMain = transform.parent.gameObject.GetComponent<EnemyMain>();
                myEnemyMain.DeathStateFunc();

            }
            else
            {
                Destroy(transform.parent.gameObject);   
            }
            
            
            
            deathBool = true;

        }

        if (hp > maxHp)
        {
            hp = maxHp;
        }


        

    }

    public float GetMaxHp()
    {
        return maxHp;
    }

    public void AddShield(float additionalShieldHp)
    {
        shieldHp += additionalShieldHp;
        if(shieldHp > 0)
        {
            if(shieldActiveBool != true)
            {
                SetActiveTrueShield();
                maxShieldHp = shieldHp;
            }
            maxShieldHp = shieldHp;


            if (shieldHp > maxShieldHp)
            {
                shieldHp = maxShieldHp;
            } 


        }
        else
        {
            SetActiveFalseShield();
            shieldActiveBool = false;
            shieldHp = 0;
        }






        
    }

    public void SetActiveFalseShield()
    {
        shieldHpImage.gameObject.SetActive(false);
        shieldEffectImage.gameObject.SetActive(false);
        emptyShieldImage.gameObject.SetActive(false);

    }


    public void  SetActiveTrueShield() {     
        shieldHpImage.gameObject.SetActive(true);
        shieldEffectImage.gameObject.SetActive(true);
        emptyShieldImage.gameObject.SetActive(true);
    }

    public void StrenghtCalculator(int strenght)
    {
        previousMaxHp = maxHp;
        maxHp = strenght * 20;
        hp = (hp * maxHp) / previousMaxHp;
    }

    
}
