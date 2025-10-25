using System.Collections;
using System.Collections.Generic;
using EasyUIFramework;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        PanelManager.Instance.AsyncAddPanel(new ExamplePanel());
    }
}
