using System;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject questPanel;
    [SerializeField] TextMeshProUGUI questText;
    [SerializeField] TextMeshProUGUI questCounterText;

    private void Start()
    {
        QuestManager.Instance.OnQuestUpdated += QuestManager_OnQuestAccepted;
        QuestManager.Instance.OnCollectableIncreased += QuestManager_OnCollectableIncreased;
        LevelLoader.Instance.OnLevelLoaded += LevelLoader_OnLevelLoaded;
    }


    private void OnDisable()
    {
        QuestManager.Instance.OnQuestUpdated -= QuestManager_OnQuestAccepted;
        QuestManager.Instance.OnCollectableIncreased -= QuestManager_OnCollectableIncreased;
        LevelLoader.Instance.OnLevelLoaded -= LevelLoader_OnLevelLoaded;
    }

    private void LevelLoader_OnLevelLoaded(int index)
    {
        questPanel.SetActive(index != 0);
    }

    private void QuestManager_OnQuestAccepted(QuestSO quest)
    {
        questText.text = quest.Description;
        questCounterText.text = $"0/{quest.Goal.RequiredAmount}";
    }

    private void QuestManager_OnCollectableIncreased(QuestSO quest)
    {
        questCounterText.text = $"{quest.Goal.CurrentAmount}/{quest.Goal.RequiredAmount}";
    }
}
