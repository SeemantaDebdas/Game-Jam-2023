using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }
    public event Action<int> OnLevelLoaded;

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
        
    }

    public void LoadNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        LevelTransitionUI.Instance.CircleScaleUp(() => LoadLevel(buildIndex + 1));
    }

    public void LoadRoomLevel()
    {
        LevelTransitionUI.Instance.CircleScaleUp(() => LoadLevel(0));
    }

    void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        OnLevelLoaded?.Invoke(buildIndex);
        LevelTransitionUI.Instance.CircleScaleDown(default);
    }
}
