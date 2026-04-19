using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIManager : ManagerBase
{
    private Dictionary<RootType, UIRoot> uiRoots = new Dictionary<RootType, UIRoot>();
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// UI 루트를 추가하는 메서드
    /// </summary>
    /// <param name="rootType"></param>
    /// <param name="uiRoot"></param>

    public void AddUIRoot(RootType rootType, UIRoot uiRoot)
    {
        if (!uiRoots.ContainsKey(rootType))
        {
            uiRoots.Add(rootType, uiRoot);
        }
    }

    /// <summary>
    /// UI 루트를 삭제하는 메서드
    /// </summary>
    /// <param name="rootType"></param>

    public void DeleteUIRoot(RootType rootType)
    {
        if (uiRoots.ContainsKey(rootType))
        {
            uiRoots.Remove(rootType);
        }
    }

    /// <summary>
    /// 특정 UI 루트를 보여주는 메서드
    /// </summary>
    /// <param name="rootType"></param>

    public void ShowUI(RootType rootType)
    {
        if (uiRoots.ContainsKey(rootType))
        {
            uiRoots[rootType].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 특정 UI 루트를 숨기는 메서드
    /// </summary>
    /// <param name="rootType"></param>

    public void HideUI(RootType rootType)
    {
        if (uiRoots.ContainsKey(rootType))
        {
            uiRoots[rootType].gameObject.SetActive(false);
        }
    }
}
