using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour,Iinteractable
{
    public Vector3 positionToGo;

    public GameSceneSO sceneToGo;

    public SceneLoadEventSO loadEventSO;

    public void TriggerAction()
    {
        Debug.Log("´«ËÍ");

        loadEventSO.RaiseLoadRequestEvent(sceneToGo,positionToGo,true);
    }
}
