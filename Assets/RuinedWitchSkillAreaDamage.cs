using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinedWitchSkillAreaDamage : MonoBehaviour
{
    public int damageAmount =100; // Verilecek hasar miktarý



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fgfds");
        Unit unit = collision.GetComponent<Unit>();
        if (unit != null)
        {
            unit.TakeDamage(damageAmount); // Hasar uygula
            //Debug.Log($"{collision.gameObject.name} hasar aldý!");
        }
    }
}
