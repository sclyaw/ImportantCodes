using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.UI.Image;

public class unitControl : MonoBehaviour
{
    public Unit selectedUnit;
    public Healthbar healthbar;
    public bool unitSelected = false;
    private Vector3 targetPosition;
    public SkillUIManager skillUIManager;
    public bool castingBarActive;

    public GameObject selectedGroundMarkerPrefab;
    public GameObject selectedGroundMarker;

    public GameObject selectedUnitOutline;
    public GameObject selectedEnemyOutlinePrefab;

    public GameObject selectedTargetMarkerPrefab;
    public GameObject selectedTargetMarker;

    private Transform selectedTargetTransform;



    public GameObject selectedUnitMarkerPrefab;
    public GameObject selectedUnitMarker;

    public bool refreshStatsPanelBool;




    public void Start()
    {
        refreshStatsPanelBool = false;
        selectedGroundMarker = Instantiate(selectedGroundMarkerPrefab, Vector3.zero, Quaternion.identity);
        selectedGroundMarker.SetActive(false);

        selectedTargetMarker= Instantiate(selectedTargetMarkerPrefab, Vector3.zero,Quaternion.identity);
        selectedTargetMarker.SetActive(false);

        selectedUnitMarker = Instantiate(selectedUnitMarkerPrefab, Vector3.zero, Quaternion.identity);
        selectedUnitMarker.SetActive(false);
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // Sol týklama
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("unitBasic"))
                {
                    selectedUnit = hit.collider.GetComponent<Unit>();
                    Debug.Log("selected");

                }
                else if (hit.collider.CompareTag("enemyBasic") && selectedUnit != null)
                {
                    Healthbar target = hit.collider.GetComponent<Healthbar>();

                    if (target != null)
                    {
                        target.GetComponentInChildren<Healthbar>().hp -= 10f;
                        Debug.Log("attack");

                    }


                }
                else
                {
                    selectedUnit = null;
                    unitSelected = false;
                }

                Debug.Log("TIKLADIM");
                unitSelected = true;

                //UnitSkills unitSkills = selectedUnit.GetComponent<Unit>().unitSkills;
                skillUIManager.UpdateSkillPanel(selectedUnit);

                if (selectedUnit != null && selectedUnit.haveActiveSkill)
                {
                    OpenCastingBar();
                }
                else
                {
                    CloseCastingBar();
                }
                
                if(selectedUnit != null && selectedUnit.transformGround != Vector3.zero)
                {
                    
                    selectedGroundMarker.SetActive(true);
                    selectedGroundMarker.transform.position = selectedUnit.transformGround;
                }
                else if(selectedUnit != null && selectedUnit.transformGround == Vector3.zero)
                {
                    selectedGroundMarker.SetActive(false);
                }

                if(selectedUnit != null && selectedUnit.transformTarget != null)
                {
                    OpenSelectedEnemyMarker(selectedUnit.transformTarget);
                }
                else if(selectedUnit != null && selectedUnit.transformTarget == null)
                {
                    CloseSelectedEnemyMarker();
                }
            }
            else if(hit.collider == null)
            {
                selectedUnit=null;  
                unitSelected = false;
            }

            
        }


        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {

            int layerMask = LayerMask.GetMask("enemyLayer"); // Düþman katmaný

            Vector3 targetHit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPositionGround = new Vector3(targetHit.x, targetHit.y, transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(targetHit, Vector2.zero);//layerMask

            
            if (hit.collider != null && unitSelected == true) {
                Transform transformTarget = hit.collider.transform;               
                float distanceToTarget   = Vector2.Distance(selectedUnit.transform.position, transformTarget.position);
                //Debug.Log(distanceToTarget);
                if (hit.collider.CompareTag("enemyBasic") && distanceToTarget <= selectedUnit.attackRange) {
                    selectedUnit.transformGround = Vector3.zero;                                     
                    selectedUnit.SetTarget(transformTarget);
                    
                    if(selectedUnit.transformTarget != null) {
                        selectedUnit.transformGround = Vector3.zero;
                    }                   
                } else if (hit.collider.CompareTag("enemyBasic") && distanceToTarget > selectedUnit.attackRange) {
                    selectedUnit.transformGround = Vector3.zero;
                    selectedUnit.SetMoveTowardsLock(transformTarget, distanceToTarget);
                } else if (hit.collider.CompareTag("Ground")) {
                    selectedUnit.SetTransformGround(targetPositionGround);

                    if (selectedUnit != null && selectedUnit.transformGround != Vector3.zero)
                    {

                        selectedGroundMarker.SetActive(true);
                        selectedGroundMarker.transform.position = selectedUnit.transformGround;
                    }
                    else
                    {
                        selectedGroundMarker.SetActive(false);
                    }

                    //Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGG");
                }

            } else if (hit.collider == null)//&& unitSelected != null
            {
                if(unitSelected != false){
                    selectedUnit.ClearTarget();
                    //Debug.Log("ClearTarget11111111111111111111");
                }                      
            }
        }



        if(selectedTargetMarker.activeSelf && selectedTargetTransform != null)
        {
            selectedTargetMarker.transform.position = selectedTargetTransform.position;
        }


        if(selectedUnit != null)
        {
            selectedUnitMarker.SetActive(true);
            selectedUnitMarker.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitMarker.SetActive(false);
        }



        if(selectedUnit != null && selectedUnit.strength > 0 && !refreshStatsPanelBool)
        {
            skillUIManager.strenghtText.text = selectedUnit.strength.ToString();

            refreshStatsPanelBool = true;
            Debug.Log(31313131);

        }



    }

    public void OpenCastingBar()
    {
        Debug.Log("hep görürsen düzelt");
        skillUIManager.castingBar.gameObject.SetActive(true);
        skillUIManager.emptyCastingBar.gameObject.SetActive(true);
        skillUIManager.spellNameCasting.gameObject.SetActive(true);
    }

    public void CloseCastingBar()
    {
        Debug.Log("burayi düzelticez unit bak git çaðrý");
        skillUIManager.castingBar.gameObject.SetActive(false);
        skillUIManager.emptyCastingBar.gameObject.SetActive(false);
        skillUIManager.spellNameCasting.gameObject.SetActive(false);

    }

    public void OpenSelectedGroundMarker(Vector3 targetPositionGround)
    {
        selectedGroundMarker.SetActive(true);
        selectedGroundMarker.transform.position = targetPositionGround;
    }
    public void ClearSelectedGroundMarker()
    {
        selectedGroundMarker.SetActive(false);
    }

    public void OpenSelectedEnemyMarker(Transform enemyPosition)
    {
        selectedTargetMarker.SetActive(true);
        selectedTargetTransform = enemyPosition;
    }
    public void CloseSelectedEnemyMarker()
    {
        selectedTargetMarker.SetActive(false);
    }
}