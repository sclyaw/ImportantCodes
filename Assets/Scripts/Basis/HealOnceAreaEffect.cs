using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealOnceAreaEffect : MonoBehaviour
{
    public int damageAmount = 100; 
    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("unitBasic") && !objectsInTrigger.Contains(collision.gameObject))
        {
            objectsInTrigger.Add(collision.gameObject);
            ApplyDamage(collision.gameObject, damageAmount);
        }
    }


    private void ApplyDamage(GameObject obj, int damage)
    {
        if (obj != null)
        {
            Unit unit = obj.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeHeal(damage);
            }
        }
    }
}

