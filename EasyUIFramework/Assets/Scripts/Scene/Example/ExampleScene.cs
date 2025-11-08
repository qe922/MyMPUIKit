using System.Collections;
using System.Collections.Generic;
using EasyUIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExampleScene : SceneState
{
    readonly string sceneName = "ExampleScene";

    IPanelManager panelManager;
    IUITool uITool;
    public override void OnEnter()
    {
        panelManager = ServiceInitializer.GetPanelManager();
        uITool = ServiceInitializer.GetUITool();


        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            Debug.LogError("ExampleScene已经加载");
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    //场景加载完毕后使用的方法
    private void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        Debug.Log("ExampleScene加载完毕");

    }
}
