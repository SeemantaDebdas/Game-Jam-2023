using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] string interactText = "Open Door";
    [SerializeField] Transform pivot = null;
    [SerializeField] float targetAngle = -90f;

    QuestGiver questGiver = null;

    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        //open the door
        DOVirtual.Float(0, targetAngle, 0.5f,
                       (angle) => pivot.eulerAngles = new Vector3(pivot.eulerAngles.x, angle, pivot.eulerAngles.z)).SetEase(Ease.InOutBounce)
                       .OnComplete(() => LoadNextLevel());


        questGiver.GiveQuest();
    }

    private void LoadNextLevel()
    {
        LevelLoader.Instance.LoadNextLevel();
    }
}
