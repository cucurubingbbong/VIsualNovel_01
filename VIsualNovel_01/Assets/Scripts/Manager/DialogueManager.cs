using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : ManagerBase
{
    [Header("설정")]
    [SerializeField] private GameOptionSettings gameOptionSettings;

    [Header("노드 설정")]
    /// <summary>
    /// 현재 대화 노드
    /// </summary>
    public DialogueNode currentNode;

    /// <summary>
    /// 시작 대화 노드
    /// </summary>
    [SerializeField] private DialogueNode startNode;

    [Header("UI 설정")]
    //[SerializeField] private DialogueUI dialogueUI;

    // 이름 텍스트 필드
    [SerializeField] private TextMeshProUGUI nameText;
    // 대화 텍스트 필드
    [SerializeField] private TextMeshProUGUI dialogueText;
    // 캐릭터 이미지 필드
    [SerializeField] private Image characterImage;
    // 배경 이미지 필드
    [SerializeField] private Image backgroundImage;

    [Header("캐릭터 데이터 배열")]

    [SerializeField] private CharacterData[] characterDataArray;

    [Header("대사 관련 변수들")]

    /// <summary>
    /// 현재 대사 배열 인덱스
    /// </summary>
    public int index = 0;
    /// <summary>
    /// 대사 스킵 여부
    /// </summary>
    public bool isSkip = false;

    /// <summary>
    /// 자동 재생 여부
    /// </summary>
    public bool isAutoPlay = false;

    /// <summary>
    /// 타이핑 효과가 끝났는지 여부
    /// </summary>
    [SerializeField] private bool typeisEnd = false;
    public override void Init()
    {
        base.Init();
        foreach (CharacterData characterData in characterDataArray)
        {
            characterData.Init();
        }
        StartDialogue();
    }

    void Update()
    {
        /// 마우스 클릭 시 다음 대사로 이동
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    /// <summary>
    /// 대화 시작 메서드
    /// </summary>
    public void StartDialogue()
    {
        currentNode = startNode;
        if (currentNode != null && currentNode.dialogueArray.Length > 0)
        {
            DisplayDialogue(currentNode.dialogueArray[0]);
        }
        else
        {
            Debug.LogWarning("시작 노드가 설정되지 않았거나 대사 배열이 비어 있습니다.");
        }
    }
    /// <summary>
    /// 다음 대사로 이동하는 메서드
    /// </summary>
    public void NextDialogue()
    {
        if (currentNode.dialogueArray.Length > index + 1 && index >= 0)
        {
            index++;
            if (!typeisEnd)
            {
                StopAllCoroutines();
                typeisEnd = true;
                dialogueText.text = currentNode.dialogueArray[index].dialogueText;
                return;
            }
            dialogueText.text = "";
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            NextNode();
        }
    }

    /// <summary>
    /// 다음 노드로 이동하는 메서드
    /// </summary>
    public void NextNode()
    {
        if(currentNode.GetNextNode() == null)
        {
            Debug.LogWarning("다음 노드가 없습니다.");
            return;
        }
        currentNode = currentNode.GetNextNode();
        if (currentNode != null && currentNode.dialogueArray.Length > 0)
        {
            index = 0; // 인덱스 초기화
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            Debug.LogWarning("다음 노드가 없거나 대사 배열이 비어 있습니다.");
        }
    }

    /// <summary>
    /// 대사 데이터를 UI에 표시하는 메서드
    /// </summary>
    /// <param name="data">표시할 대사 데이터</param>
    public void DisplayDialogue(DialogueData data)
    {
        // 이름 텍스트 업데이트
        nameText.text = data.characterData.characterName;
        // 대화 텍스트 업데이트
        dialogueText.text = data.dialogueText;
        // 캐릭터 이미지 업데이트
        characterImage.sprite = data.characterData.GetCharImg(data.emotionType);
        // 배경 이미지 업데이트
        backgroundImage.sprite = data.backgroundImage;

        StartCoroutine(TypeText(data.dialogueText));
    }

    /// <summary>
    /// 대사 텍스트에 타이핑 효과를 적용하는 코루틴
    /// </summary>
    /// <param name="text">타이핑할 텍스트</param>
    /// <returns></returns>
    IEnumerator TypeText(string text)
    {
        typeisEnd = false;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(gameOptionSettings.textSpeed); // 글자 사이의 딜레이
        }
        typeisEnd = true;
    }


}
