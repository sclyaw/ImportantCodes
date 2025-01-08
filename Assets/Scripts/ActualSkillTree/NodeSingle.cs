using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeSingle : MonoBehaviour
{
    public Node node;
    //public Node nodeRight;
    //public Node nodeLeft;
    //public int value;
    public Image buttonImage;

    public Color selectedColor = new Color(1f, 1f, 1f, 1f); // Açýk renk
    public Color lockedColor = new Color(0.5f, 0.5f, 0.5f, 1f); // Koyu renk
    public Color selectableColor = new Color(0.85f, 0.85f, 0.85f, 1f);
    public Color notPreferedColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    //private SkillTreeManager skillTreeManager;
    void Start()
    {


        
       

    
        //node.value = this.value;
        //node.right = this.nodeRight;
        //node.left = this.nodeLeft;

        node.isUnlocked = false;
        node.isSelected = false;
        node.notPrefered = false;
        buttonImage = this.GetComponent<Image>();
        buttonImage.color = lockedColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockedFuncButton()
    {
        buttonImage.color = selectableColor;
    }

    public void SelectedFuncButton()
    {
        buttonImage.color = selectedColor;
    }

    public void NotPreferedFuncButton()
    {
        buttonImage.color = lockedColor;
    }

    
}
