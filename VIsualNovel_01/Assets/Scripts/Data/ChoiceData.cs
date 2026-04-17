using UnityEngine;

[System.Serializable]
public struct ChoiceData
{
    [TextArea(1, 3)]
    public string choiceText; // 선택지 텍스트
    public DialogueNode nextNode; // 선택지 선택 시 이동할 다음 대사 노드
}
