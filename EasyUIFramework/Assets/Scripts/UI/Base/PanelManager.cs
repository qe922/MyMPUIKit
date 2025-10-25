using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace EasyUIFramework
{
    public class PanelManager : BaseSingleton<PanelManager>
    {
        private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();
        private Dictionary<string, BasePanel> panelInstanceDict = new Dictionary<string, BasePanel>();

        public BasePanel AddPanel(BasePanel basePanel)
        {
            GameObject panel = Resources.Load<GameObject>(basePanel.UIType.Path);
            if (panel == null)
            {
                Debug.Log(basePanel.UIType.Path+"为空");
                return null;
            }
                
            GameObject Canvas = GameObject.Find("Canvas");
            if (Canvas == null)
            {
                Debug.Log("Canvas为空");
                return null;
            }
            GameObject panelGo = Instantiate(panel, Canvas.transform);
            panelDict.Add(basePanel.UIType.Name, panelGo);
            panelInstanceDict.Add(basePanel.UIType.Name, basePanel);
            basePanel.Init();
            return basePanel;
        }

        public BasePanel AddPanel(BasePanel basePanel, BasePanel parentPanel)
        {
            AddPanel(basePanel);
            basePanel.SetParentPanel(parentPanel);
            return basePanel;
        }
        public BasePanel AsyncAddPanel(BasePanel basePanel)
        {
            StartCoroutine(LoadPanelAsync(basePanel));
            return basePanel;
        }
        public BasePanel AsyncAddPanel(BasePanel basePanel, BasePanel parentPanel)
        {
            AsyncAddPanel(basePanel);
            basePanel.SetParentPanel(parentPanel);
            return basePanel;
        }
        private IEnumerator LoadPanelAsync(BasePanel basePanel)
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(basePanel.UIType.Path);
            if (resourceRequest == null)
                yield break;
            yield return resourceRequest;
            GameObject Canvas = GameObject.Find("Canvas");
            if (Canvas == null)
            {
                Debug.Log("Canvas为空");
                yield break;
            }
            GameObject panel = resourceRequest.asset as GameObject;
            GameObject panelGo = Instantiate(panel, Canvas.transform);
            panelDict.Add(basePanel.UIType.Name, panelGo);
            panelInstanceDict.Add(basePanel.UIType.Name, basePanel);
            basePanel.Init();
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
        public GameObject GetPanelInstanceGameObject(string Name)
        {
            if (panelDict.TryGetValue(Name, out GameObject panel))
                return panel;
            else
                return null;
        }

        public void RemovePanel(string Name)
        {
            if (panelDict.ContainsKey(Name))
            {
                var panelInstance = GetPanelInstance(Name);
                if(panelInstance != null&& panelInstance.ParentPanel != null)
                {
                    panelInstance.ParentPanel.CloseChildPanel(panelInstance);
                }
                panelInstance.OnExit();
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