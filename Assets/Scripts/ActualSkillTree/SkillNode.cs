using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillNode
{
    public string skillName;       // Yetenek ismi
    public bool isUnlocked;        // Bu yetenek a��ld� m�?
    public SkillNode leftNode;     // Sol d���m (�rn. Savunma)
    public SkillNode rightNode;    // Sa� d���m (�rn. Sald�r�)
    public bool notPrefered;
    public float nodeNumber;

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log(skillName + " unlocked!");
        }
    }

}

