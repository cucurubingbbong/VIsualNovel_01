using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIManager : ManagerBase
{
    private Dictionary<RootType, UIRoot> uiRoots = new Dictionary<RootType, UIRoot>();

    public void AddUIRoot(RootType rootType, UIRoot uiRoot)
    {
        if (!uiRoots.ContainsKey(rootType))
        {
            uiRoots.Add(rootType, uiRoot);
        }
    }

    public void DeleteUIRoot(RootType rootType)
    {
        if (uiRoots.ContainsKey(rootType))
        {
            uiRoots.Remove(rootType);
        }
    }
}
