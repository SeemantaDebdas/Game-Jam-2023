using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestEventSO", menuName = "ScriptableObjects/QuestEventSO", order = 1)]
public class QuestEventSO : ScriptableObject
{
    public event Action<QuestSO> OnQuestAccepted;
    public event Action<QuestSO> OnQuestCompleted;

    public void RaiseOnQuestAcceptedEvent(QuestSO quest) => OnQuestAccepted?.Invoke(quest);
    public void RaiseOnQuestCompletedEvent(QuestSO quest) => OnQuestCompleted?.Invoke(quest);
}
