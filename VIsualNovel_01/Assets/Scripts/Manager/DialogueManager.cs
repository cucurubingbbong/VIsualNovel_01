using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : ManagerBase
{
    [Header("설정")]
    [SerializeField] private GameOptionSettings gameOptionSettings;

    [Header("노드 설정")]
    public DialogueNode currentNode;
    [SerializeField] private DialogueNode startNode;

    [Header("UI 설정")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;

    [Header("캐릭터 데이터 배열")]
    [SerializeField] private CharacterData[] characterDataArray;

    [Header("대사 관련 변수들")]
    public int index = 0;
    public bool isSkip = false;
    public bool isAutoPlay = false;

    [SerializeField] private bool typeisEnd = false;

    private Coroutine typingCoroutine;

    public override void Init()
    {
        base.Init();

        foreach (CharacterData characterData in characterDataArray)
        {
            characterData.Init();
        }

        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    /// <summary>
    /// 대화 시작
    /// </summary>
    public void StartDialogue()
    {
        currentNode = startNode;
        index = 0;
        typeisEnd = true;

        if (currentNode != null && currentNode.dialogueArray.Length > 0)
        {
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            Debug.LogWarning("시작 노드가 설정되지 않았거나 대사 배열이 비어 있습니다.");
        }
    }

    /// <summary>
    /// 다음 대사로 이동
    /// </summary>
    public void NextDialogue()
    {
        if (currentNode == null || currentNode.dialogueArray == null || currentNode.dialogueArray.Length == 0)
            return;

        // 1. 타이핑이 끝나지 않았으면 현재 대사를 즉시 완성
        if (!typeisEnd)
        {
            Debug.Log("타이핑이 끝나지 않았습니다.");

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }

            dialogueText.text = currentNode.dialogueArray[index].dialogueText;
            typeisEnd = true;
            return;
        }

        // 2. 타이핑이 끝난 상태면 다음 대사로 이동
        if (index < currentNode.dialogueArray.Length - 1)
        {
            index++;
            Debug.Log("다음 대사로 이동합니다.");
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            // 3. 현재 노드의 마지막 대사까지 끝났으면 다음 노드로 이동
            NextNode();
        }
    }

    /// <summary>
    /// 다음 노드로 이동
    /// </summary>
    public void NextNode()
    {
        DialogueNode nextNode = currentNode.GetNextNode();

        if (nextNode == null)
        {
            Debug.LogWarning("다음 노드가 없습니다.");
            return;
        }

        currentNode = nextNode;
        index = 0;
        typeisEnd = true;

        if (currentNode.dialogueArray != null && currentNode.dialogueArray.Length > 0)
        {
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            Debug.LogWarning("다음 노드의 대사 배열이 비어 있습니다.");
        }
    }

    /// <summary>
    /// 대사 데이터를 UI에 표시
    /// </summary>
    public void DisplayDialogue(DialogueData data)
    {
        nameText.text = data.characterData.characterName;
        characterImage.sprite = data.characterData.GetCharImg(data.emotionType);
        backgroundImage.sprite = data.backgroundImage;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(data.dialogueText));
    }

    /// <summary>
    /// 타이핑 효과
    /// </summary>
    IEnumerator TypeText(string text)
    {
        typeisEnd = false;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(gameOptionSettings.textSpeed);
        }

        typeisEnd = true;
        typingCoroutine = null;
    }
}