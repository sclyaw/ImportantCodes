using UnityEngine;
using System.Collections;



public class clickChecker : MonoBehaviour
{

    [SerializeField] private GameObject shooterPrefab;

    private Vector3 target;



    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            Shooter shooter = Instantiate(shooterPrefab, target, Quaternion.identity).GetComponent<Shooter>();
        }


        

        
    }
}