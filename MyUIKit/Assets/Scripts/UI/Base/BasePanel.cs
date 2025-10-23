using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasePanel
{
    public UIType uIType { get; private set; }
    public BasePanel ParentPanel { get; private set; }
    private List<BasePanel> childPanels = new List<BasePanel>();

    public virtual void ModelInit()
    {

    }
    public virtual void ViewInit()
    {
        
    }
    public virtual void ControllerInit()
    {

    }
    public virtual void UpdataView()
    {
        
    }
    //UI暂停时执行的操作
    public virtual void OnPause()
    {
        UITool.Instance.GetorAddPanelComponent<CanvasGroup>(uIType).blocksRaycasts = false;
    }
    //UI继续时执行的操作
    public virtual void OnResume()
    {
        UITool.Instance.GetorAddPanelComponent<CanvasGroup>(uIType).blocksRaycasts = true;
    }
    public virtual void Init()
    {
        ModelInit();
        ViewInit();
        ControllerInit();
    }
    public virtual void OnExit()
    {
        
    }

    // 添加子Panel
    public void AddChildPanel(BasePanel childPanel)
    {
        if (childPanel != null && !childPanels.Contains(childPanel))
        {
            childPanel.ParentPanel = this;
            childPanels.Add(childPanel);
        }
    }

    // 移除子Panel
    public void RemoveChildPanel(BasePanel childPanel)
    {
        if (childPanels.Contains(childPanel))
        {
            childPanel.ParentPanel = null;
            childPanels.Remove(childPanel);
        }
    }

    // 打开子Panel时自动暂停父Panel
    public void OpenChildPanel(BasePanel childPanel)
    {
        AddChildPanel(childPanel);
        PanelManager.Instance.AddPanel(childPanel);
        // 暂停当前Panel
        this.OnPause();
    }

    // 关闭子Panel时恢复父Panel
    public void CloseChildPanel(BasePanel childPanel)
    {
        RemoveChildPanel(childPanel);
        PanelManager.Instance.RemovePanel(childPanel.uIType);
        
        // 如果没有其他子Panel，恢复当前Panel
        if (childPanels.Count == 0)
        {
            this.OnResume();
        }
    }

    // 关闭所有子Panel
    public void CloseAllChildPanels()
    {
        foreach (var child in childPanels.ToArray())
        {
            CloseChildPanel(child);
        }
    }
}
