
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HitOnceAreaEffect : MonoBehaviour
    {
        public int damageAmount = 100; // Periyodik hasar miktarý

        private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>(); // Trigger içindeki nesneler

        private void OnTriggerEnter2D(Collider2D collision)
        {
    
            if (collision.CompareTag("enemyBasic") && !objectsInTrigger.Contains(collision.gameObject))
            {
                objectsInTrigger.Add(collision.gameObject);
                ApplyDamage(collision.gameObject, damageAmount);
            }
        }


        private void ApplyDamage(GameObject obj, int damage)
        {
            if (obj != null)
            {
                EnemyMain enemy = obj.GetComponent<EnemyMain>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

            }

        }
    }

