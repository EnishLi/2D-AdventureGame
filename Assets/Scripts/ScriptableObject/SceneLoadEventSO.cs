using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadREquestEvent;

    /// <summary>
    /// ��������Ҫ��
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳ���</param>
    /// <param name="posToGo">Player��Ŀ������</param>
    /// <param name="fadeScreen">�Ƿ��뽥��</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad,Vector3 posToGo,bool fadeScreen) 
    {
        LoadREquestEvent?.Invoke(locationToLoad,posToGo,fadeScreen);
    }
}