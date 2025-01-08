using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




[CreateAssetMenu(fileName = "NewNode", menuName = "BinaryTree/Node")]
public class Node : ScriptableObject
{
    public Button associatedButton;

    public string skillName;
    public string skillDescription;
    public bool isSelected;
    public bool isUnlocked;

    public bool notPrefered;
    public int value;
    public Node left;
    public Node right;
   

}