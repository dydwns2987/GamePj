using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Skill : MonoBehaviour
{
    public SkillData data;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.skillName;
    }

    void Update()
    {

    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            case SkillData.SkillType.Character1:
            case SkillData.SkillType.Character2:
                break;
        }
    }
}
