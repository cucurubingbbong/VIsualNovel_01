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
    [SerializeField] private Button[] choiceButtons = new Button[3];

    [Header("캐릭터 데이터 배열")]
    [SerializeField] private CharacterData[] characterDataArray;

    [Header("대사 관련 변수들")]
    public int index = 0;
    public bool isSkip = false;
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
    private void StartDialogue()
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
    private void NextDialogue()
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
            DisplayChoices(); // 선택지가 있는 경우 선택지 표시, 없으면 다음 노드로 이동
        }
    }

    /// <summary>
    /// 다음 노드로 이동
    /// </summary>
    private void NextNode()
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
    private void DisplayDialogue(DialogueData data)
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
    private IEnumerator TypeText(string text)
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

        /// 자동 재생 모드가 활성화된 경우 일정 시간 후 다음 대사로 이동
        if(gameOptionSettings.isAutoPlayEnabled)
        {
            yield return new WaitForSeconds(gameOptionSettings.autoPlayDelay);
            NextDialogue();
        }
    }

    /// <summary>
    /// 선택지 표시
    /// </summary>
    private void DisplayChoices()
    {
        if (currentNode.hasChoices)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentNode.choiceArray.Length)
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choiceArray[i].choiceText;
                    int choiceIndex = i; // 클로저 문제 방지
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }
            NextNode(); // 선택지가 없는 경우 다음 노드로 자동 이동
        }
    }

    /// <summary>
    /// 선택지 선택 시 호출되는 메서드
    /// </summary>
    /// <param name="choiceIndex">선택된 선택지의 인덱스</param>
    private void OnChoiceSelected(int choiceIndex)
    {
        if (currentNode.hasChoices && choiceIndex < currentNode.choiceArray.Length)
        {
            DialogueNode nextNode = currentNode.choiceArray[choiceIndex].nextNode;
            if (nextNode != null)
            {
                currentNode = nextNode;
                index = 0;
                typeisEnd = true;
                DisplayDialogue(currentNode.dialogueArray[index]);
            }
            else
            {
                Debug.LogWarning("선택지에 연결된 다음 노드가 없습니다.");
            }
        }
    }
    
    // 스킵 기능은 보류 생각중임

    // 로그 기능은 4월 20일까진 구현할듯
}