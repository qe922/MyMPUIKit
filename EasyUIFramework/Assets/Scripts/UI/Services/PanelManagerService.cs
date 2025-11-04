using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUIFramework
{
    /// <summary>
    /// 面板管理器服务实现
    /// </summary>
    public class PanelManagerService : IPanelManager
    {
        private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();
        private Dictionary<string, BasePanel> panelInstanceDict = new Dictionary<string, BasePanel>();

        public BasePanel AddPanel(BasePanel basePanel)
        {
            GameObject panel = Resources.Load<GameObject>(basePanel.UIType.Path);
            if (panel == null)
            {
                Debug.Log(basePanel.UIType.Path + "为空");
                return null;
            }

            GameObject Canvas = GameObject.Find("Canvas");
            if (Canvas == null)
            {
                Debug.Log("Canvas为空");
                return null;
            }
            GameObject panelGo = GameObject.Instantiate(panel, Canvas.transform);
            panelDict.Add(basePanel.UIType.Name, panelGo);
            panelInstanceDict.Add(basePanel.UIType.Name, basePanel);
            basePanel.Init();
            return basePanel;
        }

        public BasePanel AddPanel(BasePanel basePanel, BasePanel parentPanel)
        {
            var panel = AddPanel(basePanel);
            basePanel.SetParentPanel(parentPanel);
            parentPanel.AddChildPanel(basePanel);
            
            // 暂停父面板
            parentPanel.OnPause();
            
            return panel;
        }

        public BasePanel AsyncAddPanel(BasePanel basePanel)
        {
            // 这里需要MonoBehaviour来启动协程，所以暂时不支持异步
            // 在实际项目中，可以通过依赖注入传递MonoBehaviour
            Debug.LogWarning("异步加载面板需要MonoBehaviour支持，使用同步加载代替");
            return AddPanel(basePanel);
        }

        public BasePanel AsyncAddPanel(BasePanel basePanel, BasePanel parentPanel)
        {
            var panel = AsyncAddPanel(basePanel);
            basePanel.SetParentPanel(parentPanel);
            parentPanel.AddChildPanel(basePanel);
            
            // 暂停父面板
            parentPanel.OnPause();
            
            return panel;
        }

        public GameObject GetDictPanel(UIType uiType)
        {
            if (panelDict.TryGetValue(uiType.Name, out GameObject panel))
                return panel;
            else
                return null;
        }

        public BasePanel GetPanelInstance(string name)
        {
            panelInstanceDict.TryGetValue(name, out BasePanel panel);
            return panel;
        }

        public GameObject GetPanelInstanceGameObject(string name)
        {
            if (panelDict.TryGetValue(name, out GameObject panel))
                return panel;
            else
                return null;
        }

        public void RemovePanel(string name)
        {
            if (panelDict.ContainsKey(name))
            {
                var panelInstance = GetPanelInstance(name);
                if (panelInstance != null)
                {
                    // 1. 先通知父面板移除子面板关系
                    var parentPanel = panelInstance.ParentPanel;
                    if (parentPanel != null)
                    {
                        parentPanel.RemoveChildPanel(panelInstance);
                        
                        // 检查父面板是否需要恢复
                        if (parentPanel.ChildPanelCount == 0 && parentPanel.ParentPanel == null)
                        {
                            parentPanel.OnResume();
                        }
                    }
                    
                    // 2. 执行子面板的退出逻辑
                    panelInstance.OnExit();
                    
                    // 3. 最后从字典中移除
                    panelDict.Remove(name);
                    panelInstanceDict.Remove(name);
                }
            }
        }

        public T GetPanel<T>(string name) where T : BasePanel
        {
            panelInstanceDict.TryGetValue(name, out BasePanel panel);
            T t = panel as T;
            if (t == null)
                Debug.LogError("获取Panel失败");
            return t;
        }
    }
}