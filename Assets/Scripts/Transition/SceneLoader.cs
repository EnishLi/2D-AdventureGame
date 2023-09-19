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

    public bool isLoading;

    public Vector3 firstPosition;

    private Vector3 positionToGo;
    private bool fadeScreen;
    public float fadeDuration;
    [Header("时间监听")]

    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    private GameSceneSO sceneToLoad;
    private GameSceneSO currentLoadedScene;
    
    [Header("广播事件 ")]
    public VoidEventSO AfterSceneEvent;
    public FadeEventSO fadeEvent;

    //TODO:需要更改
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
    /// 场景加载事件请求
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
            //卸载场景逐渐变黑
            fadeEvent.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);
        
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        //关闭人物
        playerTrans.gameObject.SetActive(false);

        //加载新场景
        LoadNewScene();
    }
    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad,firstPosition,true);
    }
    private void LoadNewScene()
    {
       var loadingOption =  sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompeted;
    }

    /// <summary>
    /// 场景加载完成
    /// </summary>
    /// <param name="handle"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadCompeted(AsyncOperationHandle<SceneInstance> handle)
    {
        playerTrans.position = positionToGo;
        currentLoadedScene = sceneToLoad;
        if (fadeScreen)
        { 
            //TODO：加载场景逐渐变透明
            fadeEvent.FadeOut(fadeDuration);
        }
        playerTrans.gameObject.SetActive(true);
        isLoading = false;
        //场景加载完
        AfterSceneEvent.RaiseEvent();
    }
}
