using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;

    [SerializeField] private float shootRate;
    
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private float trajectoryMaxHeight;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;


    private float shootTimer;

    public float detectionRange = 2f;
    public float currentDetectionRange;
    public float permanentDetectionRange;

    public LayerMask enemyLayer;

    private List<Effect> activeEffects = new List<Effect>();

    private void Start()
    {
        currentDetectionRange = detectionRange;

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
                currentDetectionRange += effect.valueChange;
                break;
            
        }

        
        yield return new WaitForSeconds(effect.duration);

        
        switch (effect.effectType)
        {
            
            case EffectType.AttackRange:
                currentDetectionRange -= effect.valueChange;
                break;
        }

        
        activeEffects.Remove(effect);
    }

    public float GetCurrentDetectionRange() => currentDetectionRange;
    


    
    private void Update()
    {

        



        DetectEnemies();

        if (Input.GetKeyDown(KeyCode.M))
        {
            this.AddEffect(new Effect(5f, 100f, EffectType.AttackRange));

        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            this.AddEffect(new Effect(2f, -10f, EffectType.AttackRange));

        }




    }

    private IEnumerator ResetAttackRangeAfterDelay(float delay, float permanent)   // kullanilmadi!! sil
    {
        yield return new WaitForSeconds(delay); 
        

        detectionRange = permanent; 
        
    }
    void DetectEnemies()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, currentDetectionRange, enemyLayer);

        if (enemiesInRange.Length > 0)
        {

            Collider2D lowestHealthEnemy = null;
            float lowestHealth = float.MaxValue;

            foreach (Collider2D enemy in enemiesInRange)
            {               

                float health = enemy.GetComponentInChildren<Healthbar>().hp;
                if (health != 0)
                {
                    if (health < lowestHealth)
                    {
                        lowestHealth = health;
                        lowestHealthEnemy = enemy;
                    }
                }
                


               

                if (lowestHealthEnemy != null)
                {
                    Transform enemyPosition = lowestHealthEnemy.transform;
                    shootTimer -= Time.deltaTime;
                    


                    if (shootTimer <= 0)
                    {
                        shootTimer = shootRate;
                        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                        projectile.InitializeProjectile(enemyPosition, projectileMaxMoveSpeed, trajectoryMaxHeight);
                        projectile.InitializeAnimationCurves(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
                    }
                }

            }

           
            
        }
        else
        {
            //Debug.Log("Düþman yok.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Görselleþtirme için bir daire çizer
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}





