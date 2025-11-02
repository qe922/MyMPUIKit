using UnityEngine;

namespace EasyUIFramework
{
    /// <summary>
    /// 面板管理器接口
    /// </summary>
    public interface IPanelManager
    {
        /// <summary>
        /// 添加面板
        /// </summary>
        BasePanel AddPanel(BasePanel basePanel);
        
        /// <summary>
        /// 添加面板并设置父面板
        /// </summary>
        BasePanel AddPanel(BasePanel basePanel, BasePanel parentPanel);
        
        /// <summary>
        /// 异步添加面板
        /// </summary>
        BasePanel AsyncAddPanel(BasePanel basePanel);
        
        /// <summary>
        /// 异步添加面板并设置父面板
        /// </summary>
        BasePanel AsyncAddPanel(BasePanel basePanel, BasePanel parentPanel);
        
        /// <summary>
        /// 获取面板GameObject
        /// </summary>
        GameObject GetDictPanel(UIType uiType);
        
        /// <summary>
        /// 获取面板实例
        /// </summary>
        BasePanel GetPanelInstance(string name);
        
        /// <summary>
        /// 获取面板GameObject
        /// </summary>
        GameObject GetPanelInstanceGameObject(string name);
        
        /// <summary>
        /// 移除面板
        /// </summary>
        void RemovePanel(string name);
        
        /// <summary>
        /// 获取指定类型的面板
        /// </summary>
        T GetPanel<T>(string name) where T : BasePanel;
    }
}