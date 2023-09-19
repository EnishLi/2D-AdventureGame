using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Ê±¼ä¼àÌý")]

    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    private GameSceneSO sceneToLoad;
    private GameSceneSO currentLoadedScene;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private float fadeDuration;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference,LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }
    private void OnEnable()
    {
        loadEventSO.LoadREquestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadREquestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO gameSceneToLoad, Vector3 posToGo, bool fadeScreen)
    {
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        sceneToLoad = gameSceneToLoad;
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviosScene());
        }
    }

    private IEnumerator UnLoadPreviosScene()
    {
        if (fadeScreen)
        { }
        yield return new WaitForSeconds(fadeDuration);
        
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
    }
}
