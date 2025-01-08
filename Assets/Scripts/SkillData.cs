using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill Tree/Skill")]
public class SkillData : ScriptableObject
{
    public string skillType;
    public string selectionType;
    public float cooldownTime;
    public float remainingCooldown;
    [HideInInspector] public float lastUsedTime;


    public string skillName;
    public Sprite skillIcon;
    public string description;
    public int cost;

    public bool OnCooldown
    {
        get;
        set;
    }

    public float MyCooldown
    {
        get;
        set;
    }
}