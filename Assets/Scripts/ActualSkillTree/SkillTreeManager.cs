using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public GameObject nodePrefab;   
    public Vector2 offset;           
    public Transform treeParent;    
   
    //public List<Node> nodeListOne = new List<Node>();

    public List<Button> buttonListOne;



    //public void CreateNode(SkillNode node, Vector2 position)
    //{
    //    if (node == null) return;

    //    
    //    GameObject newNode = Instantiate(nodePrefab, treeParent);
    //    newNode.transform.localPosition = position;

    //    
    //    if (node.leftNode != null)
    //    {
    //        CreateNode(node.leftNode, position + new Vector2(-offset.x, -offset.y));
    //    }
    //    if (node.rightNode != null)
    //    {
    //        CreateNode(node.rightNode, position + new Vector2(offset.x, -offset.y));
    //    }
    //}



    public IEnumerator Start()
    {
        yield return null;
        foreach (var button in buttonListOne)
        {
            button.onClick.AddListener(() => SelectSkillTreeNode(button));

        }
    }





    public void SelectSkillTreeNode(Button button)
    {

        NodeSingle nodeSingle = button.GetComponent<NodeSingle>();
        Node node = nodeSingle.node;
        UnlockLeftRight(node.associatedButton);
       
        
        
        if(node != null)
        {
            node.isSelected = true;
            

            switch (node.value)
            {

                case 2:
                    
                    var targetButtonAA = FindNodeInList(buttonListOne, 2);
                    
                    var targetButtonA = FindNodeInList(buttonListOne, 3);
                    if (targetButtonA != null)
                    {
                        UnlockLeftRight(targetButtonAA);
                        NotPreferedFunc(targetButtonA);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 3 not found!");
                    }                  
                    break;
                case 3:
                   
                    var targetButtonBB = FindNodeInList(buttonListOne, 3);
                    var targetButtonB = FindNodeInList(buttonListOne, 2);
                    if (targetButtonB != null)
                    {
                        UnlockLeftRight(targetButtonBB);
                        NotPreferedFunc(targetButtonB);
                    }
                    else
                    {
                        UnlockLeftRight(targetButtonBB);
                        Debug.LogWarning("Button with value 3 not found!");
                    }
                    

                    break;
                case 4:
        
                    var targetButtonCC = FindNodeInList(buttonListOne, 4);
                    var targetButtonC= FindNodeInList(buttonListOne, 6);
                    if (targetButtonC != null)
                    {
                        UnlockLeftRight(targetButtonCC);
                        NotPreferedFunc(targetButtonC);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 3 not found!");
                    }

                    break;
                case 6:
          
                    var targetButtonDD = FindNodeInList(buttonListOne, 6);
                    var targetButtonD = FindNodeInList(buttonListOne, 4);
                    if (targetButtonD != null)
                    {
                        UnlockLeftRight(targetButtonDD);
                        NotPreferedFunc(targetButtonD);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 3 not found!");
                    }
                    break;
                case 5:
                 
                    var targetButtonEE = FindNodeInList(buttonListOne, 5);
                    var targetButtonE = FindNodeInList(buttonListOne, 7);
                    if (targetButtonE != null)
                    {
                        UnlockLeftRight(targetButtonEE);
                        NotPreferedFunc(targetButtonE);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 3 not found!");
                    }
                    break;
                case 7:
                 
                    var targetButtonFF = FindNodeInList(buttonListOne, 7);
                    var targetButtonF = FindNodeInList(buttonListOne, 5);
                    if (targetButtonF != null)
                    {
                        UnlockLeftRight(targetButtonFF);
                        NotPreferedFunc(targetButtonF);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 3 not found!");
                    }
                    break;
                case 8:
                   
                    var targetButtonGG = FindNodeInList(buttonListOne, 8);
                    var targetButtonG = FindNodeInList(buttonListOne, 10);
                    if (targetButtonG != null)
                    {
                        UnlockLeftRight(targetButtonGG);
                        NotPreferedFunc(targetButtonG);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 10 not found!");
                    }
                    break;
                case 10:
                  
                    var targetButtonHH = FindNodeInList(buttonListOne, 10);
                    var targetButtonH = FindNodeInList(buttonListOne, 8);
                    if (targetButtonH != null)
                    {
                        UnlockLeftRight(targetButtonHH);
                        NotPreferedFunc(targetButtonH);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 8 not found!");
                    }
                    break;
                case 9:
              
                    var targetButtonII = FindNodeInList(buttonListOne, 9);
                    var targetButtonI = FindNodeInList(buttonListOne, 11);
                    if (targetButtonI != null)
                    {
                        UnlockLeftRight(targetButtonII);
                        NotPreferedFunc(targetButtonI);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 8 not found!");
                    }
                    break;
                case 11:
           
                    var targetButtonJJ = FindNodeInList(buttonListOne, 11);
                    var targetButtonJ = FindNodeInList(buttonListOne, 9);
                    if (targetButtonJ != null)
                    {
                        UnlockLeftRight(targetButtonJJ);
                        NotPreferedFunc(targetButtonJ);
                    }
                    else
                    {
                        Debug.LogWarning("Button with value 8 not found!");
                    }
                    break;



            }
        }
    }



    public Button FindNodeInList(List<Button> buttonList, int value)
    {
        foreach (var button in buttonList)
        {
            NodeSingle nodeSingle = button.GetComponent<NodeSingle>();
            Node node = nodeSingle.node;
            if (node != null && node.value == value)
            {                 
                return button;
            }
        }

       
        return null;
    }

    public void NotPreferedFunc(Button button)
    {
        Debug.Log("one");
        if (button != null)
        {
            
            NodeSingle nodeSingle = button.GetComponent<NodeSingle>();
            Node node = nodeSingle.node;
            if (node != null)
            {
                node.notPrefered = true;
                button.onClick.RemoveAllListeners();
                nodeSingle.NotPreferedFuncButton();

             
                if (node.right != null) {
                    var targetButtonRight = FindNodeInList(buttonListOne, node.right.value);
                    NotPreferedFunc(targetButtonRight);

                }
                if (node.left != null) {
                    var targetButtonLeft = FindNodeInList(buttonListOne, node.left.value);
                    NotPreferedFunc(targetButtonLeft);
                   
                }
              

            }
            DeleteListener(button);
        }
           
    }

    public void DeleteListener(Button button)
    {
        button.onClick.RemoveAllListeners();
        
        Debug.Log ("one");
    }

    public void UnlockLeftRight(Button button)
    {
        if (button != null)
        {
            NodeSingle nodeSingle = button.GetComponent<NodeSingle>();
            Node node = nodeSingle.node;

            if (node != null)
            {
                node.isSelected = true;
                button.onClick.RemoveAllListeners();
                nodeSingle.SelectedFuncButton();


                if (node.right != null)
                {
                    node.right.isUnlocked = true;
                    var targetButtonRight = FindNodeInList(buttonListOne, node.right.value);
                    NodeSingle nodeSingleRight = button.GetComponent<NodeSingle>();
                    nodeSingleRight.UnlockedFuncButton();
                  

                }
                if (node.left != null)
                {
                    var targetButtonLeft = FindNodeInList(buttonListOne, node.left.value);
                    NodeSingle nodeSingleLeft = button.GetComponent<NodeSingle>();
                    nodeSingleLeft.UnlockedFuncButton();
                    node.left.isUnlocked = true;
                }


            }
        }

        



    }




}