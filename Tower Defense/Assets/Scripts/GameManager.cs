using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //Game state
    public static event Action<GameState> OnGameStateChanged;
    public GameState state;

    public WaypointsSO waypointSO;
    
    //Stage Management
    [Header("Stage Management")]
    public MonsterSpawner monsterSpawner;
    public StageListSO stageList;
    
    public int delayBetweenStages = 5;

    private int stage = 0;

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
            case GameState.WaitForStage:
                StartCoroutine(WaitForStage()); //Czeka 5 sekund po końcu stage'u
                break;
            case GameState.StagePlay:
                StartStage(); //Updatuje nr stage'u i uruchamia kolejny
                break;
            case GameState.CheckIfEnd:
                CheckEndGame();
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
        UpdateGameState(GameState.WaitForStage);
    }

    public IEnumerator WaitForStage()
    {
        yield return new WaitForSeconds(delayBetweenStages);
        UpdateGameState(GameState.StagePlay);
    }

    public void StartStage()
    {
        monsterSpawner.UpdateStage();
        monsterSpawner.StartStage();
    }

    
    
    public void CheckEndGame()
    {
        int currentStage = monsterSpawner.GetStageNumber();
        if (currentStage > stageList.stageInfoList.Count)
        {
            UpdateGameState(GameState.Victory);
        }
        else
        {
            UpdateGameState(GameState.WaitForStage);
        }
    }
    
    #endregion
    
    public enum GameState
    {
        Initiate,
        WaitForStage,
        StagePlay,
        CheckIfEnd,
        Victory,
        Lose
    }
}