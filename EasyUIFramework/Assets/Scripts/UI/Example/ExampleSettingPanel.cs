using System;
using System.Collections;
using System.Collections.Generic;
using EasyUIFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExampleSettingPanel : BasePanel
{
    public static readonly string Name = "ExampleSettingPanel";
    public string ExplanationText;
    public Action action;
    public ExampleSettingPanel() : base(Name) { }
    public ExampleSettingPanel(string ExplanationText, Action action) : this()
    {
        
        this.ExplanationText = ExplanationText;
        this.action = action;
    }
    public override void ViewInit()
    {
        UITool.Instance.UITypeGetChildComponent<Text>(UIType, "ExplanationText").text = ExplanationText;
    }
    public override void ControllerInit()
    {
        UITool.Instance.UITypeGetChildComponent<Button>(UIType, "BtnExit").onClick.AddListener(() =>
        {
            action();
        });
    }
}
