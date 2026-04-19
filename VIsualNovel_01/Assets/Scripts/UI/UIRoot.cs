using UnityEngine;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private RootType rootType;

    private void Awake() {
        GManager.Instance.uiManager.AddUIRoot(rootType, this);
    }

    private void OnDestroy() {
        GManager.Instance.uiManager.DeleteUIRoot(rootType);
    }
}
