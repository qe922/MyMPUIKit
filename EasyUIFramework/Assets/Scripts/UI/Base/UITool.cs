using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EasyUIFramework
{
    public class UITool : BaseSingleton<UITool>
    {
        public T GetorAddComponent<T>(GameObject go) where T : Component
        {
            go.TryGetComponent<T>(out T t);
            if (t == null)
            {
                t = go.AddComponent<T>();
            }
            return t;
        }
        public GameObject FindChildGameObject(Transform parent, string childName)
        {
            Transform child = parent.Find(childName);
            if (child != null)
                return child.gameObject;
            return null;
        }
        public T GetorAddChildComponent<T>(Transform parent, string childName) where T : Component
        {
            GameObject go = FindChildGameObject(parent, childName);
            if (go == null)
                return null;
            return GetorAddComponent<T>(go);
        }
        public T UITypeGetChildComponent<T>(UIType uiType, string childName) where T : Component
        {
            GameObject go = PanelManager.Instance.GetDictPanel(uiType);
            if (go == null)
                return null;
            return GetorAddChildComponent<T>(go.transform, childName);
        }
        public GameObject UITypeGetChildGameObject(UIType uiType, string childName)
        {
            GameObject go = PanelManager.Instance.GetDictPanel(uiType);
            if (go == null)
                return null;
            return FindChildGameObject(go.transform, childName);
        }
        public T GetorAddPanelComponent<T>(UIType uiType) where T : Component
        {
            GameObject go = PanelManager.Instance.GetDictPanel(uiType);
            if (go == null)
                return null;
            return GetorAddComponent<T>(go);
        }

    }
}