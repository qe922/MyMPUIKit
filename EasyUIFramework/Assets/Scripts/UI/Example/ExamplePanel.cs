using UnityEngine;
using UnityEngine.UI;

namespace EasyUIFramework
{
    /// <summary>
    /// 使用DI的示例面板
    /// </summary>
    public class ExamplePanel : BasePanel
    {
        private Button BtnAdd;
        private Button BtnSub;
        private Text NumText;
        private Button BtnOpenSettingPanel;
        public int Num;

        public ExamplePanel(IPanelManager panelManager, IUITool uiTool) : base("ExamplePanel", panelManager, uiTool) { }


        public override void ViewInit()
        {
            //订阅事件
            EventBus.Instance.RegisterListener("AmendNum", UpdateView);

            // 使用DI服务获取UI组件
            BtnAdd = UITool.UITypeGetChildComponent<Button>(UIType, "BtnAdd");
            BtnSub = UITool.UITypeGetChildComponent<Button>(UIType, "BtnSub");
            NumText = UITool.UITypeGetChildComponent<Text>(UIType, "NumText");
            BtnOpenSettingPanel = UITool.UITypeGetChildComponent<Button>(UIType, "BtnOpenSettingUI");
        }
        public override void ControllerInit()
        {
            BtnAdd?.onClick.AddListener(() => { AddNum(); });
            BtnSub?.onClick.AddListener(() => { SubNum(); });
            
            BtnOpenSettingPanel?.onClick.AddListener(OnButtonClick);
        }
        public override void UpdateView()
        {
            NumText.text = Num.ToString();
        }

        private void OnButtonClick()
        {
            // 使用DI服务添加新面板，并建立父子关系
            var settingPanel = new ExampleSettingPanel(PanelManager, UITool);
            PanelManager.AddPanel(settingPanel, this);
            // 通过面板管理器来管理状态，而不是直接调用OnPause
            // 父面板的暂停状态由面板管理器在添加子面板时自动处理
        }
        private void AddNum()
        {
            Num++;
            EventBus.Instance.Publish("AmendNum");
        }

        private void SubNum()
        {
            Num--;
            EventBus.Instance.Publish("AmendNum");
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}