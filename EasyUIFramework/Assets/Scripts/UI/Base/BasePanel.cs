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
        private List<BasePanel> childPanels = new List<BasePanel>();
        
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
                //childPanel.OnExit();
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
            PanelManager.AddPanel(childPanel);
            // 暂停当前Panel
            this.OnPause();
        }

        // 关闭子Panel时恢复父Panel
        public void CloseChildPanel(BasePanel childPanel)
        {
            RemoveChildPanel(childPanel);
            PanelManager.RemovePanel(childPanel.UIType.Name);

            // 如果没有其他子Panel，恢复当前Panel
            if (childPanels.Count == 0)
            {
                this.OnResume();
            }
        }

        // 关闭所有子Panel
        public void CloseAllChildPanels()
        {
            for(int i = childPanels.Count - 1; i >= 0; i--)
            {
                CloseChildPanel(childPanels[i]);
            }
        }
    }
}