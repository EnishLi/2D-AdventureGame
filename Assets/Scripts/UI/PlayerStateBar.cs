using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    /// <summary>
    /// ����Health�ı���ٷֱ�
    /// </summary>
    /// <param name="persentage">�ٷֱȣ�Current/Max</param>
    public void OnHealthChange(float persentage)
    { 
        healthImage.fillAmount = persentage;
    
    }
}
