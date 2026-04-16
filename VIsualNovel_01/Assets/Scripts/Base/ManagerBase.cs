using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    public virtual void Init()
    {
        Debug.Log($"{gameObject.name}이 초기화되었습니다");
    }
}
