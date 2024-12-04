using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { Character1, Character2 }

    [Header("# Main Info")]
    public SkillType skillType;
    public int skillId;
    public string skillName;
    [TextArea]
    public string skillDesc;
    public Sprite skillIcon;

    [Header("# Skill Data")]
    public float baseDamage;
    public int baseCount;
}
