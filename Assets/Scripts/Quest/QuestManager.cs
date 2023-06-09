using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{ 
    public event Action<QuestSO> OnQuestUpdated;
    public event Action<QuestSO> OnCollectableIncreased;

    [SerializeField] QuestEventSO bookQuestEvent;
    //[SerializeField] QuestEventSO roachQuestEvent;
    //[SerializeField] QuestEventSO laundryQuestEvent;

    QuestSO currentQuest;

    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        bookQuestEvent.OnQuestAccepted += OnQuestAccepted;
        //roachQuestEvent.OnQuestAccepted += OnQuestAccepted;
        //laundryQuestEvent.OnQuestAccepted += OnQuestAccepted;
    }

    public void OnQuestAccepted(QuestSO quest)
    {
        currentQuest = quest;
        currentQuest.ResetGoal();
        OnQuestUpdated?.Invoke(quest);
    }

    public void IncreaseCollectable()
    {
        currentQuest.Goal.CurrentAmount++;
        OnCollectableIncreased?.Invoke(currentQuest);

        if (currentQuest.Goal.IsReached)
        {
            bookQuestEvent.RaiseOnQuestCompletedEvent(currentQuest);
            LevelLoader.Instance.LoadRoomLevel();
        }
    }
}
