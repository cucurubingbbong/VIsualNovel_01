using UnityEngine;

[System.Serializable]
public struct DialogueData
{
    /// <summary>
    /// 대사 텍스트
    /// </summary>
    [TextArea(1, 5)]
    public string dialogueText;

    /// <summary>
    /// 대사에 해당하는 캐릭터 데이터
    /// </summary>
    public CharacterData characterData;

    /// <summary>
    /// 대사에 해당하는 배경 이미지
    /// </summary>
    public Sprite backgroundImage;

    /// <summary>
    /// 대사에 해당하는 캐릭터의 감정 유형
    /// </summary>
    public EmotionType emotionType;
}
