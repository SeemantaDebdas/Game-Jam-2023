using System;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject questPanel;
    [SerializeField] TextMeshProUGUI questText;
    private void Start()
    {
        QuestManager.Instance.OnQuestUpdated += QuestManager_OnQuestAccepted;
        LevelLoader.Instance.OnLevelLoaded += LevelLoader_OnLevelLoaded;
    }

    private void OnDisable()
    {
        QuestManager.Instance.OnQuestUpdated -= QuestManager_OnQuestAccepted;
        LevelLoader.Instance.OnLevelLoaded -= LevelLoader_OnLevelLoaded;
    }

    private void LevelLoader_OnLevelLoaded()
    {
        questPanel.SetActive(true);
    }

    private void QuestManager_OnQuestAccepted(QuestSO quest)
    {
        questText.text = quest.Description;
    }
}
