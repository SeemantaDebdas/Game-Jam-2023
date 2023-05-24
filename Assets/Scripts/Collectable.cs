using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 toPosition = transform.position + Vector3.up * 1f;

        transform.DOMove(toPosition, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.IncreaseCollectible();
            GetComponent<SphereCollider>().enabled = false;

            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => {
                transform.DOKill();
                Destroy(gameObject);
            });
        }
    }
}
