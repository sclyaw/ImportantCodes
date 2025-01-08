using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackRange : MonoBehaviour
{

    
    public GameObject attackRangeIndicator; 

    private float currentRange;

    public float defaultSize = 1f;

    public float fadeDuration = 0.5f;
    

    


    void Start()
    {
        
        attackRangeIndicator.SetActive(false);

        
        SetAttackRangeIndicatorSize();
    }

    

    void OnMouseEnter()
    {
        

        currentRange = this.GetComponent<Shooter>().GetCurrentDetectionRange();
        //Debug.Log(currentRange);
        //Debug.Log(defaultSize);

        
        attackRangeIndicator.SetActive(true);
        attackRangeIndicator.transform.localScale = new Vector3(currentRange * defaultSize, currentRange * defaultSize, 1);
        StartCoroutine(FadeIn());


        //Debug.Log("IM STUPID");

        
    }

    void OnMouseExit()
    {
        
        attackRangeIndicator.SetActive(false);
        StartCoroutine(FadeOut());
    }

    void SetAttackRangeIndicatorSize()
    {
        
        
        float size = this.GetComponent<Shooter>().detectionRange;
        Debug.Log(size);
        attackRangeIndicator.transform.localScale = new Vector3(size, size, 1); 
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer spriteRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        UnityEngine.Color color = spriteRenderer.color;
        float elapsedTime = 0f;


        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            spriteRenderer.color = color;
            yield return null;  
        }



       
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        UnityEngine.Color color = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            spriteRenderer.color = color; // alpha degeri
            yield return null;
        }
    }

    


    

  

   
}
