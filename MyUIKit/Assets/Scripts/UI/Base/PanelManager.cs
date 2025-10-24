using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EasyUIFramework
{
    public class PanelManager : BaseSingleton<PanelManager>
    {
        private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();
        private Dictionary<string, BasePanel> panelInstanceDict = new Dictionary<string, BasePanel>();

        public void AddPanel(BasePanel basePanel)
        {
            GameObject panel = Resources.Load<GameObject>(basePanel.UIType.Path);
            GameObject panelGo = Instantiate(panel, GameObject.Find("Canvas").transform);
            panelDict.Add(basePanel.UIType.Name, panelGo);
            panelInstanceDict.Add(basePanel.UIType.Name, basePanel);
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
            if (panelDict.TryGetValue(uiType.Name, out GameObject panel))
                return panel;
            else
                return null;
        }

        public BasePanel GetPanelInstance(string Name)
        {
            panelInstanceDict.TryGetValue(Name, out BasePanel panel);
            return panel;
        }

        public void RemovePanel(string Name)
        {
            if (panelDict.ContainsKey(Name))
            {
                var panelInstance = GetPanelInstance(Name);
                if (panelInstance != null && panelInstance.ParentPanel != null)
                {
                    panelInstance.ParentPanel.RemoveChildPanel(panelInstance);
                }
                panelDict.Remove(Name);
                panelInstanceDict.Remove(Name);
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