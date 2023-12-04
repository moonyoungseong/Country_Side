using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.Events;

public class NPCQuests : MonoBehaviour
{
    public TMP_Text ConversationText;
    public Button ConversationButton;
    public Button NPCButton;
    public Button QuestDetail;

    public Button AcceptButton;
    public Button RejectButton;
    public Button CloseButton;

    private string[] conversation;
    private int currentLine = 0;
    private int currentFileIndex;
    [SerializeField]
    private string[] QuestFiles = new string[] { "Quests/Quest1", "Quests/Quest2", "Quests/Quest3" };

    public TMP_Text questTitleText;
    public TMP_Text questText;
    public TMP_Text questRewardText;
    public Button[] questButtons;

    private string QuestIndex;
    private int QuestCount;
    [SerializeField]
    private string[] QuestDetailFiles = new string[] { "QuestDetails/Quest1Detail", "QuestDetails/Quest2Detail", "QuestDetails/Quest3Detail" };

    public int QuestNum;

    public Button QuestButton;
    public Transform Content;

    public List<bool> OnQuestlList = new List<bool>();
    public List<bool> ClearQuestlList = new List<bool>();

    private List<QuestData> activeQuests = new List<QuestData>();
    private Dictionary<Button, int> questButtonIndexes = new Dictionary<Button, int>();

    private UnityAction action;

    void Start()
    {
        NPCButton.onClick.AddListener(ShowButton);
        NPCButton.onClick.AddListener(() => SetQuestNum(1));

        questButtons = new Button[4];

        for (int i = 0; i < questButtons.Length; i++)
        {
            GameObject newButtonObj = Instantiate(QuestButton.gameObject, Content);
            questButtons[i] = newButtonObj.GetComponent<Button>();
            questButtonIndexes.Add(questButtons[i], i);
        }
    }

    void Update()
    {
        NextConversation();
    }

    public void NextConversation()
    {
        if (Input.GetMouseButtonDown(0) && ConversationButton.gameObject.activeSelf)
        {
            if (currentLine >= conversation.Length)
            {
                ConversationText.text = "";
                ConversationButton.gameObject.SetActive(false);
                QuestDetail.gameObject.SetActive(true);
            }
            else
            {
                ConversationText.text = conversation[currentLine];
                currentLine++;
            }
        }
    }

    public void ShowButton()
    {
        ConversationButton.gameObject.SetActive(true);
    }

    public void LoadConversationFile(string fileName = null)
    {
        if (fileName == null)
        {
            fileName = QuestFiles[QuestNum];
        }

        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        var data = JsonUtility.FromJson<conversationData>(jsonFile.text);
        conversation = new string[data.conversation.Length];

        for (int j = 0; j < data.conversation.Length; j++)
        {
            conversation[j] = $"{data.conversation[j].speaker}: {data.conversation[j].line}";
        }

        currentLine = 0;
        ConversationText.text = conversation[currentLine];
        currentLine++;
    }

    public void SetQuestNum(int questNum)
    {
        QuestNum = questNum;
        LoadConversationFile(QuestFiles[questNum]);
        LoadQuestData(QuestDetailFiles[questNum]);
    }

    public void QuestAllow()
    {
        int index = 0;

        while (index < activeQuests.Count && !string.IsNullOrEmpty(activeQuests[index].questTitle))
        {
            index++;
        }

        QuestData newQuest = new QuestData(QuestIndex, questTitleText.text, questText.text, questRewardText.text);
        if (index < activeQuests.Count)
        {
            activeQuests[index] = newQuest;
        }
        else
        {
            activeQuests.Add(newQuest);
        }

        QuestDetail.gameObject.SetActive(false);
        DisplayActiveQuests(index);
        UpdateQuestButtonLabels(index);
    }

    private void DisplayActiveQuests(int questIndex)
    {
        if (questIndex >= 0 && questIndex < activeQuests.Count)
        {
            QuestData quest = activeQuests[questIndex];
            QuestIndex = quest.questIndex;
            QuestCount = int.Parse(QuestIndex);
            questTitleText.text = quest.questTitle;
            questText.text = quest.questText;
            questRewardText.text = quest.questReward;
        }
    }

    private void UpdateQuestButtonLabels(int questIndex)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (i < questButtons.Length)
            {
                questButtons[i].GetComponentInChildren<TMP_Text>().text = activeQuests[i].questTitle;

                questButtons[i].onClick.RemoveAllListeners();
                int tempIndex = i;
                questButtons[i].onClick.AddListener(() => ShowQuestDetails(tempIndex));
            }
        }
    }

    public void ShowQuestDetails(int questIndex)
    {
        if (questIndex >= 0 && questIndex < activeQuests.Count)
        {
            QuestData quest = activeQuests[questIndex];
            questTitleText.text = quest.questTitle;
            questText.text = quest.questText;
            questRewardText.text = quest.questReward;

            AcceptButton.gameObject.SetActive(false);
            RejectButton.gameObject.SetActive(false); ;
            CloseButton.gameObject.SetActive(true);
            QuestDetail.gameObject.SetActive(true);
        }
    }

    public void QuestReject()
    {
        QuestDetail.gameObject.SetActive(false);
    }

    public void LoadQuestData(string fileName = null)
    {
        if (fileName == null)
        {
            return;
        }

        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile == null)
        {
            return;
        }

        var data = JsonUtility.FromJson<QuestData>(jsonFile.text);

        QuestIndex = data.questIndex;
        questTitleText.text = data.questTitle;
        questText.text = data.questText;
        questRewardText.text = data.questReward;
    }

    [Serializable]
    private class conversationData
    {
        public conversationLine[] conversation;
    }

    [Serializable]
    private class conversationLine
    {
        public string speaker;
        public string line;
    }

    [Serializable]
    private class QuestData
    {
        public string questIndex;
        public string questTitle;
        public string questText;
        public string questReward;

        public QuestData(string questIndex, string questTitle, string questText, string questReward)
        {
            this.questIndex = questIndex;
            this.questTitle = questTitle;
            this.questText = questText;
            this.questReward = questReward;
        }
    }
}
