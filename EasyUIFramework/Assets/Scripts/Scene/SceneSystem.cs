using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSystem 
{
    SceneState sceneState;
    
    public void SetScene(SceneState state)
    {
        if (sceneState != null)
        {
            sceneState.OnExit();
        }
        sceneState = state;
        if (sceneState != null)
        {
            sceneState.OnEnter();
        }
        
    }
}
