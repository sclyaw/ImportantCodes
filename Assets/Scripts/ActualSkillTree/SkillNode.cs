using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillNode
{
    public string skillName;       // Yetenek ismi
    public bool isUnlocked;        // Bu yetenek açýldý mý?
    public SkillNode leftNode;     // Sol düðüm (Örn. Savunma)
    public SkillNode rightNode;    // Sað düðüm (Örn. Saldýrý)
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

