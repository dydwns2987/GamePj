using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Wave, TimeSlide, TimeText, PlayerHealth, TowerHealth }
    public InfoType type;
    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                myText.text = string.Format("Soul.{0:F0}", GameManager.instance.soul);
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;

            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;

            case InfoType.Wave:
                myText.text = string.Format("Wave:{0:F0}", GameManager.instance.wave);
                break;

            case InfoType.TimeSlide:
                mySlider.value = GameManager.instance.gameTime / GameManager.instance.maxGameTime;
                break;

            case InfoType.TimeText:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

            case InfoType.PlayerHealth:
                float curPlayerHealth = GameManager.instance.playerHealth;
                float maxPlayerHealth = GameManager.instance.maxPlayerHealth;
                mySlider.value = curPlayerHealth / maxPlayerHealth;
                break;

            case InfoType.TowerHealth:
                float curTowerHealth = GameManager.instance.towerHealth;
                float maxTowerHealth = GameManager.instance.maxTowerHealth;
                myText.text = string.Format("Tower HP:{0:F0}", curTowerHealth + 1);
                // mySlider.value = curTowerHealth / maxTowerHealth;
                break;
        }
    }
}
