using System.Collections;
using System.Collections.Generic;
using EasyUIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : SceneState
{
    readonly string sceneName = "StartScene";

    IPanelManager panelManager;
    IUITool uiTool;
    public override void OnEnter()
    {
        ServiceInitializer.InitializeServices();
        panelManager = ServiceInitializer.GetPanelManager();
        uiTool = ServiceInitializer.GetUITool();


        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            Debug.LogError("StartScene已经加载");
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    //场景加载完毕后使用的方法
    private void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        panelManager.AddPanel(new ExamplePanel(panelManager, uiTool));
            
        Debug.Log("DI示例已启动");
    }
}

