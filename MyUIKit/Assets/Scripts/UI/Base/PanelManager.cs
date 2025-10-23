using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : BaseSingleton<PanelManager>
{
    private Dictionary<UIType, GameObject> panelDict = new Dictionary<UIType, GameObject>();
    private Dictionary<UIType, BasePanel> panelInstanceDict = new Dictionary<UIType, BasePanel>();
    
    public void AddPanel(BasePanel basePanel)
    {
        GameObject panel = Resources.Load<GameObject>(basePanel.uIType.Path);
        GameObject panelGo = Instantiate(panel,GameObject.Find("Canvas").transform);
        panelDict.Add(basePanel.uIType, panelGo);
        panelInstanceDict.Add(basePanel.uIType, basePanel);
        basePanel.Init();
    }

    public void AddPanel(BasePanel basePanel, BasePanel parentPanel)
    {
        AddPanel(basePanel);
        
        // 如果指定了父Panel，建立父子关系
        if (parentPanel != null)
        {
            parentPanel.OpenChildPanel(basePanel);
        }
    }

    public GameObject GetDictPanel(UIType uiType)
    {
        panelDict.TryGetValue(uiType, out GameObject panel);
        if (panel != null)
            return panel;
        else
            return null;
    }

    public BasePanel GetPanelInstance(UIType uiType)
    {
        panelInstanceDict.TryGetValue(uiType, out BasePanel panel);
        return panel;
    }

    public void RemovePanel(UIType uiType)
    {
        if (panelDict.ContainsKey(uiType))
        {
            var panelInstance = GetPanelInstance(uiType);
            if (panelInstance != null && panelInstance.ParentPanel != null)
            {
                panelInstance.ParentPanel.RemoveChildPanel(panelInstance);
            }
            
            panelDict.Remove(uiType);
            panelInstanceDict.Remove(uiType);
        }
    }
}
