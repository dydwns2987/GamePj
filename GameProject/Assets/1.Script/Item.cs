using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[Mathf.Min(level, data.damages.Length - 1)] * 100, data.counts[Mathf.Min(level, data.counts.Length - 1)]);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[Mathf.Min(level, data.damages.Length - 1)] * 100);
                break;

            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClick()
    {
        // 아이템 타입에 따른 처리 로직

        if (level >= data.damages.Length)
        {
            // 더 이상 레벨업이 불가능할 경우
            GetComponent<Button>().interactable = false;
            return;
        }

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    // 배열 크기 검사 후 값을 적용
                    nextDamage += data.baseDamage * data.damages[Mathf.Min(level, data.damages.Length - 1)];
                    nextCount += data.counts[Mathf.Min(level, data.counts.Length - 1)];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[Mathf.Min(level, data.damages.Length - 1)];
                    gear.LevelUp(nextRate);
                }

                level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.instance.playerHealth = GameManager.instance.maxPlayerHealth;
                break;
        }

        if (level >= data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
