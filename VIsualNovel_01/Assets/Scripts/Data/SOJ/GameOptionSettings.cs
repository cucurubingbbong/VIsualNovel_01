using UnityEngine;

[CreateAssetMenu(fileName = "GameOptionSettings", menuName = "Settings/GameOptionSettings")]
public class GameOptionSettings : ScriptableObject
{
    [Range(0.01f, 1f)]
    public float textSpeed = 0.05f; // 텍스트가 한 글자씩 나타나는 속도

    [Range(0.5f, 25f)]
    public float autoPlayDelay = 2f; // 자동 재생 모드에서 다음 대사로 넘어가는 시간 간격

    [Range(0.0f, 1.0f)]
    public float bgmVolume = 0.5f; // 배경 음악 볼륨 (0.0 ~ 1.0)    

    [Range(0.0f, 1.0f)]
    public float sfxVolume = 0.5f; // 효과음 볼륨 (0.0 ~ 1.0)
}
