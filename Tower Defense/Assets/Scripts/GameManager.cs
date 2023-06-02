using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //Game state
    public static event Action<GameState> OnGameStateChanged;

    [Header("UI Elements")] 
    public TextMeshProUGUI stageNR;
    
    //Stage Management
    [Header("Stage Management")]
    public GameState state;
    public MonsterSpawner monsterSpawner;
    public StageListSO stageList;
    
    public int delayBetweenStages = 5;
    
    [Header("")]
    public WaypointsSO waypointSO;

    private int stage = 0;
    private string stageText = "Stage: ";
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        UpdateGameState(GameState.Initiate);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Initiate:
                SendData(); //Send necessary data to spawner
                break;
            case GameState.CheckAndWait:
                UpdateStage();
                StartCoroutine(CheckIfWinAndWaitForStage()); //Czeka 5 sekund po końcu stage'u
                break;
            case GameState.StagePlay:
                StartStage(); //Updatuje nr stage'u i uruchamia kolejny
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    #region GameStateMethods

    private void SendData()
    {
        monsterSpawner.InitiateData(stageList,stage,waypointSO);
        UpdateGameState(GameState.CheckAndWait);
    }

    private void UpdateStage()
    {
        monsterSpawner.UpdateStage();
        stage = monsterSpawner.GetStageNumber();
        stageNR.text = stageText + stage;//UI Update
    }

    public IEnumerator CheckIfWinAndWaitForStage()
    {
        stage = monsterSpawner.GetStageNumber();
        Debug.Log("Stage" + stage + "ListCount" + stageList.stageInfoList.Count);
        if (stage > stageList.stageInfoList.Count)
        {
            UpdateGameState(GameState.Victory);
            Debug.Log("Victory");
        }
        else
        {
            yield return new WaitForSeconds(delayBetweenStages);
            UpdateGameState(GameState.StagePlay);
            Debug.Log("GameState StagePlay");
        }
    }

    public void StartStage()
    {
        monsterSpawner.StartStage();
    }

    #endregion
    
    public enum GameState
    {
        Initiate,
        CheckAndWait,
        StagePlay,
        Victory,
        Lose
    }
}