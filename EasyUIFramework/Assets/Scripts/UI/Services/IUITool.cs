using UnityEngine;

namespace EasyUIFramework
{
    /// <summary>
    /// UI工具接口
    /// </summary>
    public interface IUITool
    {
        /// <summary>
        /// 获取或添加组件
        /// </summary>
        T GetorAddComponent<T>(GameObject go) where T : Component;

        /// <summary>
        /// 查找子物体
        /// </summary>
        GameObject FindChildGameObject(Transform parent, string childName);

        /// <summary>
        /// 获取或添加子物体组件
        /// </summary>
        T GetorAddChildComponent<T>(Transform parent, string childName) where T : Component;

        /// <summary>
        /// 根据UIType获取子物体组件
        /// </summary>
        T UITypeGetChildComponent<T>(UIType uiType, string childName) where T : Component;

        /// <summary>
        /// 根据UIType获取子物体
        /// </summary>
        GameObject UITypeGetChildGameObject(UIType uiType, string childName);

        /// <summary>
        /// 获取或添加面板组件
        /// </summary>
        T GetorAddPanelComponent<T>(UIType uiType) where T : Component;
    }
}