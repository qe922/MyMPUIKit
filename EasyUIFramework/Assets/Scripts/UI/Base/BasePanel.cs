using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace EasyUIFramework
{
    public abstract class BasePanel
    {
        public UIType UIType { get; private set; }
        public BasePanel ParentPanel { get; private set; }
        protected List<BasePanel> childPanels = new List<BasePanel>();
        
        // 获取子面板数量
        public int ChildPanelCount => childPanels.Count;
        
        // DI服务引用
        protected IPanelManager PanelManager { get; private set; }
        protected IUITool UITool { get; private set; }
        
        public BasePanel(string name, IPanelManager panelManager, IUITool uiTool)
        {
            UIType = new UIType(name);
            PanelManager = panelManager;
            UITool = uiTool;
        }

        public virtual void ModelInit(){}
    
        public virtual void ViewInit(){}
       
        public virtual void ControllerInit(){}
        
        public virtual void UpdateView(){}
       
        
        //UI暂停时执行的操作
        public virtual void OnPause()
        {
            UITool.GetorAddPanelComponent<CanvasGroup>(UIType).blocksRaycasts = false;
        }
        
        //UI继续时执行的操作
        public virtual void OnResume()
        {
            UITool.GetorAddPanelComponent<CanvasGroup>(UIType).blocksRaycasts = true;
        }
        
        public virtual void Init()
        {
            ModelInit();
            ViewInit();
            ControllerInit();
        }
        
        public virtual void OnExit()
        {
            GameObject.Destroy(PanelManager.GetPanelInstanceGameObject(UIType.Name));
        }

        // 添加子Panel
        public BasePanel AddChildPanel(BasePanel childPanel)
        {
            if (childPanel != null && !childPanels.Contains(childPanel))
            {
                childPanel.ParentPanel = this;
                childPanels.Add(childPanel);
                return childPanel;
            }
            return null;
        }

        // 移除子Panel
        public void RemoveChildPanel(BasePanel childPanel)
        {
            if (childPanels.Remove(childPanel))
            {
                childPanel.ParentPanel = null;
            }
        }
        
        public void SetParentPanel(BasePanel parentPanel)
        {
            this.ParentPanel = parentPanel;
        }

        // 打开子Panel时自动暂停父Panel
        public void OpenChildPanel(BasePanel childPanel)
        {
            AddChildPanel(childPanel);
            PanelManager.AddPanel(childPanel, this);
            // 注意：PanelManager.AddPanel(BasePanel, BasePanel) 中已经处理了父面板暂停
            // 这里不再重复调用 OnPause()
        }

        // 关闭子Panel时恢复父Panel
        public void CloseChildPanel(BasePanel childPanel)
        {
            if (childPanel == null || !childPanels.Contains(childPanel))
                return;
            
            // 通过PanelManager统一处理面板移除
            PanelManager.RemovePanel(childPanel.UIType.Name);
            
            // PanelManager.RemovePanel 中已经处理了父子关系移除
            // 这里检查是否需要恢复父面板状态
            if (childPanels.Count == 0 && this.ParentPanel == null)
            {
                // 只有当没有其他子面板且当前面板不是子面板时才恢复
                this.OnResume();
            }
        }
        
        // 关闭所有子Panel
        public void CloseAllChildPanels()
        {
            // 从后往前遍历避免索引问题
            for(int i = childPanels.Count - 1; i >= 0; i--)
            {
                CloseChildPanel(childPanels[i]);
            }
        }
    }
}