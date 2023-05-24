using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionUI : MonoBehaviour
{
    public static LevelTransitionUI Instance { get; private set; }

    [SerializeField] Transform circleUI;
    [SerializeField] float enlargedScale = 2f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CircleScaleUp(Action onComplete)
    {
        DOVirtual.Float(0, enlargedScale, 0.5f, (val) => circleUI.localScale = Vector3.one * val).SetEase(Ease.InOutBounce).OnComplete(() => onComplete?.Invoke());
    }

    public void CircleScaleDown(Action onComplete)
    {
        Debug.Log("called");
        DOVirtual.Float(enlargedScale, 0, 0.5f, (val) => circleUI.localScale = Vector3.one * val).SetEase(Ease.InOutBounce).OnComplete(() => onComplete?.Invoke());
    }
}
