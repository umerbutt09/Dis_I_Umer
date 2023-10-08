using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{

    public static GameTimer Instance;
    bool RunMemoryTimer;
    public float MemoryTimer;
    public float MemoryTimerCount;

    private void Awake()
    {
        Instance = this;
    }

    public void StartMemoryTimer ()
    {
        MemoryTimerCount = MemoryTimer;
        RunMemoryTimer = true;
    }
    private void Update()
    {
        if (RunMemoryTimer)
        {
            MemoryTimerCount = MemoryTimerCount - Time.deltaTime;
            if (MemoryTimerCount <= 0f)
            {
                RunMemoryTimer = false;
                ExhaustMemoryTimer();
            }

            UIManager.Instance.UpdateTimerText(Mathf.RoundToInt(MemoryTimerCount));
        }
    }

    public void ExhaustMemoryTimer ()
    {
        GameManager.Instance.HideAllTiles();
        GameManager.Instance.StartGame();
        Debug.Log("Memory Timer Out");
    }
}
