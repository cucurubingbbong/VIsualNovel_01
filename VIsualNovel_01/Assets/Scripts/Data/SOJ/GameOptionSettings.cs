using UnityEngine;

[CreateAssetMenu(fileName = "GameOptionSettings", menuName = "Settings/GameOptionSettings")]
public class GameOptionSettings : ScriptableObject
{
    public float textSpeed = 0.05f; // 텍스트가 한 글자씩 나타나는 속도

    public float autoPlayDelay = 2f; // 자동 재생 모드에서 다음 대사로 넘어가는 시간 간격
}
