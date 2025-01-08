using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interractableObject : MonoBehaviour
{


    [SerializeField] private MouseCursor mouseIcon;
    

    private void OnMouseEnter()
    {
        mouseIcon.ChangeSpriteAttackBasic();


    }
    private void OnMouseExit()
    {
        mouseIcon.ChangeSpriteDefault();
    }



}
