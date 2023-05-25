using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    [SerializeField] float endValueDistance = 5f;

    private void Start()
    {
        Vector3 moveDestination = transform.position + moveDirection * endValueDistance;
        transform.DOMove(moveDestination, moveSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuart);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + moveDirection * endValueDistance, 1f);
        Gizmos.DrawLine(transform.position, transform.position + moveDirection * endValueDistance);
    }
}
