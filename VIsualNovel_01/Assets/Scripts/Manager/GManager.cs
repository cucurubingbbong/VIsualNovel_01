using UnityEngine;

public class GManager : ManagerBase
{
    public static GManager Instance {get; private set;}

    /// <summary>
    /// 초기화할 매니저들
    /// </summary>
    [SerializeField] ManagerBase[] managers;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        Init();
    }

    public override void Init()
    {
        base.Init();
        foreach (ManagerBase mn in managers)
        {
            mn.Init();
        }
    }

}
