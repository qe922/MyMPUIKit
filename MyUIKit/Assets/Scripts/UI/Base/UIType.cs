using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType 
{
    public string Name { get; private set; }
    public string Path { get; private set; }

    public UIType(string name)
    {
        Name = name;
        Path = "UI/Panels/" + name;
    }
}
