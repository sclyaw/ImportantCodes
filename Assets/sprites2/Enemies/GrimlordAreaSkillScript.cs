using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimlordAreaSkillScript : MonoBehaviour
{

    public float damageInterval = 0.5f; // Periyodik hasar verme aralýðý
    public int damageAmount = 10; // Periyodik hasar miktarý
    public int bonusDamageOnFirstEnter = 60; // Ýlk giriþte uygulanan ekstra hasar

    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>(); // Trigger içindeki nesneler
    private HashSet<GameObject> firstEntryObjects = new HashSet<GameObject>(); // Ýlk kez giren nesneler

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectsInTrigger.Contains(collision.gameObject))
        {
            objectsInTrigger.Add(collision.gameObject);

            // Ýlk giriþte ekstra hasar uygula
            if (!firstEntryObjects.Contains(collision.gameObject))
            {

                

                ApplyDamage(collision.gameObject, bonusDamageOnFirstEnter);
                firstEntryObjects.Add(collision.gameObject);
            }
            else
            {
                ApplyDamage(collision.gameObject, damageAmount);
            }

            // Coroutine baþlat
            StartCoroutine(ApplyDamageOverTime(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsInTrigger.Contains(collision.gameObject))
        {
            objectsInTrigger.Remove(collision.gameObject);
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject obj)
    {
        while (objectsInTrigger.Contains(obj))
        {
            yield return new WaitForSeconds(damageInterval);

            // Periyodik hasar uygula
            ApplyDamage(obj, damageAmount);
        }
    }


    private void ApplyDamage(GameObject obj, int damage)
    {
        if(obj != null)
        {
            EnemyMain enemy = obj.GetComponent<EnemyMain>();
            if (enemy != null)
            {
                enemy.TakeHeal(damage);
            }
            else
            {
                Unit unit = obj.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.TakeDamage(damage);
                }
            }

        }
         
    }
}


