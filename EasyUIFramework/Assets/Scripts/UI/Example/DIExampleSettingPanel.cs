using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFramework
{
    /// <summary>
    /// 使用DI的设置面板示例
    /// </summary>
    public class DIExampleSettingPanel : BasePanel
    {
        private Slider _volumeSlider;
        private Toggle _musicToggle;

        public DIExampleSettingPanel(IPanelManager panelManager, IUITool uiTool) : base("ExampleSettingPanel", panelManager, uiTool)
        {
        }

        public override void ViewInit()
        {
            // 使用DI服务获取UI组件
            _volumeSlider = UITool.UITypeGetChildComponent<Slider>(UIType, "VolumeSlider");
            _musicToggle = UITool.UITypeGetChildComponent<Toggle>(UIType, "MusicToggle");
        }
    }
}