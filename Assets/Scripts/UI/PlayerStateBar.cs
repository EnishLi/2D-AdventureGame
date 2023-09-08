using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    private Character currentCharacter;
    //生命值
    public Image healthImage;
    //生命值延迟
    public Image healthDelayImage;
    //耐力
    public Image powerImage;
    //回复的状态
    private bool isRecoverying;
    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount)
            healthDelayImage.fillAmount -= Time.deltaTime * 0.5f;
        if (isRecoverying)
        {
            float persentage = currentCharacter.currentPower / currentCharacter.maxPower;
            powerImage.fillAmount = persentage;
            if (persentage >= 1)
            { 
                isRecoverying = false;
                return;
            }
        }
    }

    /// <summary>
    /// 接收Health的变更百分比
    /// </summary>
    /// <param name="persentage">百分比：Current/Max</param>
    public void OnHealthChange(float persentage)
    { 
        healthImage.fillAmount = persentage;
    
    }
    public void OnPowerChange(Character character)
    { 
        isRecoverying = true;
        currentCharacter = character;
    }
}
