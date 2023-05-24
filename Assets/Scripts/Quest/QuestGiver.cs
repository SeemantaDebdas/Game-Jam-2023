using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] QuestSO quest;
    [SerializeField] QuestEventSO questEvent;

    public void GiveQuest()
    {
        questEvent.RaiseOnQuestAcceptedEvent(quest);
    }

}