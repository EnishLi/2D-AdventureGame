using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;

    //�жϳ�������״̬
    public bool isLoading;

    //��ɫ��λ��
    public Vector3 firstPosition;

    //��ɫ�������ص�λ��
    private Vector3 positionToGo;

    //���������״̬
    private bool fadeScreen;

    //��������ȴ���ʱ��
    public float fadeDuration;

    [Header("�¼�����")]

    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    private GameSceneSO sceneToLoad;
    private GameSceneSO currentLoadedScene;
    
    [Header("�㲥�¼� ")]
    public VoidEventSO AfterSceneEvent;
    public FadeEventSO fadeEvent;

    //TODO:��Ҫ����
    private void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadREquestEvent += OnLoadRequestEvent;
        
    }
    private void OnDisable()
    {
        loadEventSO.LoadREquestEvent -= OnLoadRequestEvent;
    }
    /// <summary>
    /// ���������¼�����
    /// </summary>
    /// <param name="gameSceneToLoad"></param>
    /// <param name="posToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO gameSceneToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
            return;
        isLoading = true;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        sceneToLoad = gameSceneToLoad;
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviosScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    private IEnumerator UnLoadPreviosScene()
    {
        if (fadeScreen)
        {
            //ж�س����𽥱��
            fadeEvent.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);
        
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        //�ر�����
        playerTrans.gameObject.SetActive(false);

        //�����³���
        LoadNewScene();
    }
    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad,firstPosition,true);
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad,firstPosition,true);
    }
    private void LoadNewScene()
    {
       var loadingOption =  sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompeted;
    }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="handle"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompeted(AsyncOperationHandle<SceneInstance> handle)
    {
        playerTrans.position = positionToGo;
        currentLoadedScene = sceneToLoad;
        if (fadeScreen)
        { 
            //TODO�����س����𽥱�͸��
            fadeEvent.FadeOut(fadeDuration);
        }
        playerTrans.gameObject.SetActive(true);
        isLoading = false;
        //����������
        if(currentLoadedScene.SceneType == SceneType.Location)
            AfterSceneEvent.RaiseEvent();
    }
}
