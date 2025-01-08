using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public GameObject skillButtonPrefab; // Skill butonunun prefab'i
    public Transform skillPanel; // Skillerin ekleneceði panel
    private Unit selectedUnit; // Þu anda seçili olan unit

    
    public Image castingBar;
    public Image emptyCastingBar;

      
    public TMP_Text spellNameCasting;



    private Button[] skillButtonPrefabs;

    public Canvas skillCanvasOne;
    public Canvas skillCanvasTwo;
    public Canvas skillCanvasThree;

    public Sprite defaultSkillIcon;
    public Sprite defaultPassiveIcon;


    public TMP_Text cooldownTextOne;
    public TMP_Text cooldownTextTwo;
    public TMP_Text cooldownTextThree;

    public Image cooldownImageTwo;

    public TMP_Text strenghtText;

    




    public void Start()
    {



    }
    public void Update()
    {

        if (selectedUnit == null)
        {
            int btnIndex = 0;
            
            foreach (Transform child in skillPanel)
            {
                int currentIndex = btnIndex;
                if(currentIndex == 0)
                {
                    child.GetComponent<Image>().sprite = defaultPassiveIcon;

                }
                else
                {
                    child.GetComponent<Image>().sprite = defaultSkillIcon;

                }
                Button btn = child.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btnIndex++;

            }
        }



        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //UseSkill(selectedUnit.unitSkills.skills[0]);

                LikeButtonOnClick(0);
            }
            else if (Input.GetKeyDown(KeyCode.W) && selectedUnit.unitSkills.skills.Length > 1)
            {
                //UseSkill(selectedUnit.unitSkills.skills[1]);

                LikeButtonOnClick(1);
            }
            else if (Input.GetKeyDown(KeyCode.E) && selectedUnit.unitSkills.skills.Length > 2)
            {
                //UseSkill(selectedUnit.unitSkills.skills[2]);

                LikeButtonOnClick(2);
            }else if(Input.GetKeyDown(KeyCode.R) && selectedUnit.unitSkills.skills.Length > 3)
            {
                LikeButtonOnClick(3);
            }
        }

        



        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (skillCanvasTwo.gameObject.activeSelf)
                {
                    skillCanvasTwo.gameObject.SetActive(false);
                }
                if (skillCanvasThree.gameObject.activeSelf)
                {
                    skillCanvasThree.gameObject.SetActive(false);
                }
                skillCanvasOne.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (skillCanvasOne.gameObject.activeSelf)
                {
                    skillCanvasOne.gameObject.SetActive(false);
                }
                if (skillCanvasThree.gameObject.activeSelf)
                {
                    skillCanvasThree.gameObject.SetActive(false);
                }
                skillCanvasTwo.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (skillCanvasTwo.gameObject.activeSelf)
                {
                    skillCanvasTwo.gameObject.SetActive(false);
                }
                if (skillCanvasOne.gameObject.activeSelf)
                {
                    skillCanvasOne.gameObject.SetActive(false);
                }
                skillCanvasThree.gameObject.SetActive(true);
            }
        }

        if(selectedUnit != null)
        {
            if (selectedUnit.unitSkills.skills[1].OnCooldown && !cooldownTextTwo.gameObject.activeSelf)
            {
                cooldownTextTwo.gameObject.SetActive(true);
                cooldownImageTwo.gameObject.SetActive(true);
            }
            if (selectedUnit.unitSkills.skills[2].OnCooldown && !cooldownTextThree.gameObject.activeSelf)
            {
                cooldownTextThree.gameObject.SetActive(true);
            }
            //if (selectedUnit.unitSkills.skills[3].OnCooldown && !cooldownTextThree.gameObject.activeSelf)
            //{
             //   cooldownTextThree.gameObject.SetActive(true);
            //}

        }
        




    }
    public void UpdateSkillPanel(Unit unit)
    {

        if (selectedUnit != unit) { selectedUnit = unit; }


       
        


        int btnIndex = 0;
        foreach(Transform child in skillPanel)
        {
            Debug.Log(btnIndex);
            int currentIndex = btnIndex;
            child.GetComponent<Image>().sprite = selectedUnit.unitSkills.skills[currentIndex].skillIcon;
            Button btn = child.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => UseSkill(currentIndex));
            btnIndex++;
            
        }

        skillButtonPrefabs = this.GetComponentsInChildren<Button>();

        
    }

    // Yeteneði kullan ve cooldown baþlat
    private void UseSkill(int btnIndex) {
        selectedUnit.UseSkill(btnIndex);
        //Debug.Log("BASTIKKKK");
       

        
    }

    private IEnumerator CastCooldown(SkillData skill) {
        //skill.OnCastDone();
        skill.OnCooldown = true;
        yield return new WaitForSeconds(skill.cooldownTime);
        skill.OnCooldown = false;
    }
    private void LikeButtonOnClick(int btnIndex) {
        skillButtonPrefabs[btnIndex].onClick.Invoke();
    }

    public void UpdateSkillTree(SkillData skill)
    {
            

    }




}