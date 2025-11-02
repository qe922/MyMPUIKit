using UnityEngine;

namespace EasyUIFramework
{
    /// <summary>
    /// DI示例控制器
    /// </summary>
    public class DIExampleController : MonoBehaviour
    {
        private void Start()
        {
            // 初始化DI服务
            ServiceInitializer.InitializeServices();
            
            // 获取服务
            var panelManager = ServiceInitializer.GetPanelManager();
            var uiTool = ServiceInitializer.GetUITool();

            // 创建并显示示例面板（通过依赖注入传递服务）
            var examplePanel = new DIExamplePanel(panelManager, uiTool);
            panelManager.AddPanel(examplePanel);
            
            Debug.Log("DI示例已启动");
        }
    }
}