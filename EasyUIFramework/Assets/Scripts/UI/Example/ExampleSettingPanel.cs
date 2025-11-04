using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFramework
{
    /// <summary>
    /// 使用DI的设置面板示例
    /// </summary>
    public class ExampleSettingPanel : BasePanel
    {
        private Text ExplanationText;
        private Button BtnExit;
        public ExampleSettingPanel(IPanelManager panelManager, IUITool uiTool) : base("ExampleSettingPanel", panelManager, uiTool){}

        public override void ViewInit()
        {
            ExplanationText = UITool.UITypeGetChildComponent<Text>(UIType, "ExplanationText");
            BtnExit = UITool.UITypeGetChildComponent<Button>(UIType, "BtnExit");
        }

        public override void ControllerInit()
        {
            BtnExit.onClick.AddListener(() => { PanelManager.RemovePanel(UIType.Name); });
        }

        public override void UpdateView()
        {
            ExplanationText.text = "设置面板示例";
        }

    }
}