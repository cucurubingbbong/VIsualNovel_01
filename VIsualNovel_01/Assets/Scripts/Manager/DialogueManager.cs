using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : ManagerBase
{
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

    public bool isSkip = false;



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
        if(Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

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
    public void NextDialogue()
    {
        if(currentNode.dialogueArray.Length > index && index >= 0)
        {
            DisplayDialogue(currentNode.dialogueArray[index]);
        }
        else
        {
            Debug.LogWarning("유효하지 않은 대사 인덱스입니다.");
        }
    }

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



    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 글자 사이의 딜레이
        }
    }


}
