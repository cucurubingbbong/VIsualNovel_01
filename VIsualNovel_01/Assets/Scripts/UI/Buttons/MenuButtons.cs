using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : UIRoot
{
    // 인게임 세팅 버튼
    [SerializeField] private Button settingBtn;

    // 인게임 제거 버튼 ( 대사 패널 , 설정 패널 제거 )
    [SerializeField] private Button clearBtn;

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        settingBtn.onClick.AddListener(OnSettingButtonClicked);
        clearBtn.onClick.AddListener(OnClearButtonClicked);
    }

    private void OnSettingButtonClicked()
    {
        // 설정 패널 열기
        GManager.Instance.uiManager.ShowUI(RootType.SettingPanel);
    }

    private void OnClearButtonClicked()
    {
        // 대사 패널과 설정 패널 제거
        GManager.Instance.uiManager.HideUI(RootType.DialoguePanel);
        GManager.Instance.uiManager.HideUI(RootType.ChoicePanel);
    }
}
