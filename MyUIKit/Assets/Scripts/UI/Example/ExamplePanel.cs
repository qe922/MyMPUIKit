using System.Collections;
using System.Collections.Generic;
using EasyUIFramework;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePanel : BasePanel
{
    public static readonly string Name = "ExamplePanel";
    public int Num = 0;
    public ExamplePanel() : base(Name) { }

    override public void ModelInit()
    {
        Debug.Log("ModelInit");
        Num = PlayerPrefs.GetInt(nameof(Num), 0);
    }
    public override void ViewInit()
    {
        UpdateView();
    }
    public override void ControllerInit()
    {
        UITool.Instance.UITypeGetChildComponent<Button>(UIType, "BtnAdd").onClick.AddListener
        (
            () => { Num++; PlayerPrefs.Save(); UpdateView(); }
        );
        UITool.Instance.UITypeGetChildComponent<Button>(UIType, "BtnSub").onClick.AddListener
        (
            () => { Num--; PlayerPrefs.Save(); UpdateView(); }
        );
    }
    override public void UpdateView()
    {
        UITool.Instance.UITypeGetChildComponent<Text>(UIType, "NumText").text = Num.ToString();

    }
}
