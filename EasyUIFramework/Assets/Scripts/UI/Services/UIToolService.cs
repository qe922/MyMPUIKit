using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUIFramework
{
    /// <summary>
    /// UI工具服务实现
    /// </summary>
    public class UIToolService : IUITool
    {
        private IPanelManager _panelManager;

        public UIToolService(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public T GetorAddComponent<T>(GameObject go) where T : Component
        {
            if (go == null)
            {
                Debug.LogError("GetorAddComponent: GameObject is null");
                return null;
            }
            
            T t = null;
            go.TryGetComponent<T>(out t);
            if (t == null)
            {
                t = go.AddComponent<T>();
            }
            return t;
        }

        public GameObject FindChildGameObject(Transform parent, string childName)
        {
            Transform[] transforms = parent.GetComponentsInChildren<Transform>();
            foreach (var item in transforms)
            {
                if (item.name == childName)
                {
                    return item.gameObject;
                }
            }
            return null;
        }

        public T GetorAddChildComponent<T>(Transform parent, string childName) where T : Component
        {
            GameObject childGo = FindChildGameObject(parent, childName);
            if (childGo == null)
            {
                Debug.LogError($"GetorAddChildComponent: Cannot find child '{childName}' under parent '{parent?.name}'");
                return null;
            }
            return GetorAddComponent<T>(childGo);
        }

        public T UITypeGetChildComponent<T>(UIType uiType, string childName) where T : Component
        {
            GameObject go = _panelManager.GetDictPanel(uiType);
            if (go == null)
            {
                Debug.LogError($"UITypeGetChildComponent: Cannot find panel for UIType '{uiType}'");
                return null;
            }

            if (go.transform == null)
            {
                Debug.LogError($"UITypeGetChildComponent: Panel GameObject '{go.name}' has null transform");
                return null;
            }

            return GetorAddChildComponent<T>(go.transform, childName);
        }

        public GameObject UITypeGetChildGameObject(UIType uiType, string childName)
        {
            GameObject go = _panelManager.GetDictPanel(uiType);
            if (go == null)
                return null;
            return FindChildGameObject(go.transform, childName);
        }

        public T GetorAddPanelComponent<T>(UIType uiType) where T : Component
        {
            GameObject go = _panelManager.GetDictPanel(uiType);
            if (go == null)
            {
                Debug.LogError($"GetorAddPanelComponent: Cannot find panel for UIType '{uiType}'");
                return null;
            }
            return GetorAddComponent<T>(go);
        }
    }
}