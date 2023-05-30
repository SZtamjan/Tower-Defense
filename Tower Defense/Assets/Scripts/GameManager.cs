using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //Game state
    public static event Action<GameState> OnGameStateChanged;
    public GameState state;

    //Stage Management
    [Header("Stage Management")]
    public MonsterSpawner monsterSpawner;
    public StageListSO stageList;

    public int spawnHowOften = 2;
    public int delayBetweenStages = 5;

    private int stage = 0;

    private void Awake()
    {
        instance = this;
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
                StartCoroutine(WaitForStage());
                break;
            case GameState.StagePlay:
                monsterSpawner.UpdateStageNumber();
                //Start stejdżu
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
        monsterSpawner.InitiateData(stageList,stage,spawnHowOften);
        UpdateGameState(GameState.WaitForStage);
    }

    public IEnumerator WaitForStage()
    {
        yield return new WaitForSeconds(delayBetweenStages);
        UpdateGameState(GameState.StagePlay);
    }
    
    #endregion
    
    public enum GameState
    {
        Initiate,
        WaitForStage,
        StagePlay,
        Victory,
        Lose
    }
}