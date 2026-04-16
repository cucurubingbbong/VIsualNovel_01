using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    /// <summary>
    /// 캐릭터의 이름
    /// </summary>
    public string characterName;

    /// <summary>
    /// 캐릭터의 유형
    /// </summary>
    public CharacterType characterType;

    /// <summary>
    /// 캐릭터의 감정 유형과 이미지 데이터 배열
    /// </summary>
    public CharImgData[] charImgDataArray;

    private Dictionary<EmotionType, Sprite> charImgDictionary;

    public void Init()
    {
        charImgDictionary = new Dictionary<EmotionType, Sprite>();
        foreach (CharImgData charImgData in charImgDataArray)
        {
            charImgDictionary[charImgData.emotionType] = charImgData.charImg;
        }
    }

    /// <summary>
    /// 감정 유형에 따른 캐릭터 이미지를 반환하는 메서드
    /// </summary>
    /// <param name="emotionType">감정 유형</param>
    /// <returns></returns>

    public Sprite GetCharImg(EmotionType emotionType)
    {
        return charImgDictionary.TryGetValue(emotionType, out Sprite charImg) ? charImg : null;
    }
}
