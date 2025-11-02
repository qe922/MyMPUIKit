using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFramework
{
    /// <summary>
    /// 使用DI的示例面板
    /// </summary>
    public class DIExamplePanel : BasePanel
    {
        private Button BtnAdd;
        private Button BtnSub;
        private Text NumText;
        public int Num;

        public DIExamplePanel(IPanelManager panelManager, IUITool uiTool) : base("ExamplePanel", panelManager, uiTool) { }


        public override void ViewInit()
        {
            // 使用DI服务获取UI组件
            BtnAdd = UITool.UITypeGetChildComponent<Button>(UIType, "BtnAdd");
            BtnSub = UITool.UITypeGetChildComponent<Button>(UIType, "BtnSub");
            NumText = UITool.UITypeGetChildComponent<Text>(UIType, "NumText");


        }
        public override void ControllerInit()
        {
            BtnAdd?.onClick.AddListener(() => { Num++; UpdateView(); });
            BtnSub?.onClick.AddListener(() => { Num--; UpdateView(); });

        }
        public override void UpdateView()
        {
            NumText.text = Num.ToString();
        }

        private void OnButtonClick()
        {
            // 使用DI服务添加新面板
            var settingPanel = new DIExampleSettingPanel(PanelManager, UITool);
            PanelManager.AddPanel(settingPanel);

            // 暂停当前面板
            OnPause();
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}