using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType{Kill, Gather}

[System.Serializable]
public class QuestGoal
{
    public QuestType Type;

    public int RequiredAmount, CurrentAmount;

    public bool IsReached => RequiredAmount <= CurrentAmount;
}

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest", order = 1)]
public class QuestSO  : ScriptableObject
{
    public string Title;
    public string Description;

    public QuestGoal Goal;
}
