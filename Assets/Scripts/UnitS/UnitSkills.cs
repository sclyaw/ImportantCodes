using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Skills", menuName = "Skill Tree/Unit Skills")]
public class UnitSkills : ScriptableObject
{
    public SkillData[] skills;
}