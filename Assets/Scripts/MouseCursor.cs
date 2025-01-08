using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{      
    public RectTransform cursorIcon;
    public SpriteAtlas spriteAtlas;
    public Image uiImage;
    private Sprite attackCursor;
    private Sprite defaultCursor;
    

    void Awake()
    {
        defaultCursor = spriteAtlas.GetSprite("defaultCursorImage"); 
        attackCursor = spriteAtlas.GetSprite("attackCursorImage");   
        Cursor.visible = false;
        
    }  

    void Update()
    {
        cursorIcon.position = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        
        if (hit.collider != null)
        {
            
            if (hit.collider.CompareTag("enemyBasic"))
            {
                ChangeSpriteAttackBasic();
            }
            //else
            //{
               
            //}
        }
        else
        {
            ChangeSpriteDefault();
        }
    }

    public void ChangeSprite(Sprite anotherSprite)
    {
        uiImage.sprite = anotherSprite;
    }
    public void ChangeSpriteAttackBasic()
    {
        uiImage.sprite = attackCursor;
    }

    public void ChangeSpriteDefault()
    {
        uiImage.sprite = defaultCursor;
    }

}