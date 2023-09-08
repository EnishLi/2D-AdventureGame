using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    private Character currentCharacter;
    //����ֵ
    public Image healthImage;
    //����ֵ�ӳ�
    public Image healthDelayImage;
    //����
    public Image powerImage;
    //�ظ���״̬
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
    /// ����Health�ı���ٷֱ�
    /// </summary>
    /// <param name="persentage">�ٷֱȣ�Current/Max</param>
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
