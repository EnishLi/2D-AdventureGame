using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //AudioÊÂ¼þ
    public PlayerAudioEventSO playerAudioEvent;

    //ÒôÆµ
    public AudioClip audioClip;

    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
            PlayAudioClip();
    }

    public void PlayAudioClip()
    {
        playerAudioEvent.RaisedEvent(audioClip);
    }


}
