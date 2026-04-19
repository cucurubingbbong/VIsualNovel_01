using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : UIRoot
{
    /// <summary>
    /// 게임 옵션 설정 ScriptableObject 참조
    /// </summary>
    [SerializeField] private GameOptionSettings gameOptionSettings;

    [Header("UI 설정")]
    [SerializeField] private Slider textSpeedSlider;

    [SerializeField] private Slider autoPlayDelaySlider;

    [SerializeField] private Slider bgmVolumeSlider;

    [SerializeField] private Slider sfxVolumeSlider;
    public void OpenPanel()
    {
        gameObject.SetActive(true);
        // 슬라이더 초기값을 게임 옵션 설정에서 가져온 값으로 설정
        textSpeedSlider.value = gameOptionSettings.textSpeed;
        autoPlayDelaySlider.value = gameOptionSettings.autoPlayDelay;
        bgmVolumeSlider.value = gameOptionSettings.bgmVolume;
        sfxVolumeSlider.value = gameOptionSettings.sfxVolume;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void ApplySettings()
    {
        // 슬라이더 값을 게임 옵션 설정에 적용
        gameOptionSettings.textSpeed = textSpeedSlider.value;
        gameOptionSettings.autoPlayDelay = autoPlayDelaySlider.value;
        gameOptionSettings.bgmVolume = bgmVolumeSlider.value;
        gameOptionSettings.sfxVolume = sfxVolumeSlider.value;

        // 설정이 적용된 후 패널 닫기
        ClosePanel();
    }
}
