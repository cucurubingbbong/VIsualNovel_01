using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Data/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    /// <summary>
    /// 대사 데이터 배열
    /// </summary>
    public DialogueData[] dialogueArray = new DialogueData[0];

    /// <summary>
    /// 다음 대사 노드
    /// </summary>
    [SerializeField] private DialogueNode nextNode;

    /// <summary>
    /// 다음 대사 노드를 반환하는 메서드
    /// </summary>
    /// <returns>다음 대사 노드</returns>

    public DialogueNode GetNextNode()
    {
        return nextNode;
    }
}
