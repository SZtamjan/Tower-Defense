using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    //Game state
    [Header("Main Game Stuff")]
    private int timeSpeedUp = 1;
    public static event Action<GameState> OnGameStateChanged;

    [Header("UI Elements")] 
    public GameObject canvas;
    private UIUpdate uiUpdate;
    public PlayerCash playerCash;

    //Stage Management
    [Header("Stage Management")]
    public GameState state;
    public MonsterSpawner monsterSpawner;
    public StageListSO stageList;
    
    public int delayBetweenStages = 5;
    private int stage = 0;
    
    [Header("Towers")]
    public WaypointsSO waypointSO;
    public TowerListSO towerList;
    private BuildTower buildScript;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerCash = GetComponent<PlayerCash>();
        uiUpdate = canvas.GetComponent<UIUpdate>();
        UpdateGameState(GameState.Initiate);
    }

    #region TimeEvent

    public void UpdateTime()
    {
        float timeScale = Time.timeScale;
        timeScale++;
        
        if (timeScale > 4.1f) // That 0.1f is just an offset, cuz float is not really accurate
        {
            Time.timeScale = 1f;
            timeSpeedUp = 1;
        }
        else
        {
            Time.timeScale = timeScale;
            timeSpeedUp++;
        }
        uiUpdate.UpdateTimeText(timeSpeedUp.ToString());
    }
    
    #endregion
    
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
        buildScript = GetComponent<BuildTower>();
        buildScript.InitiateTowerData(towerList);
        UpdateGameState(GameState.CheckAndWait);
    }

    private void UpdateStage()
    {
        monsterSpawner.UpdateStage();
        stage = monsterSpawner.GetStageNumber();
        canvas.GetComponent<UIUpdate>().UpdateStageNoOnUI(stage.ToString());
    }

    public IEnumerator CheckIfWinAndWaitForStage()
    {
        stage = monsterSpawner.GetStageNumber();
        if (stage > stageList.stageInfoList.Count)
        {
            UpdateGameState(GameState.Victory);
            Debug.Log("Victory");
        }
        else
        {
            yield return new WaitForSeconds(delayBetweenStages);
            UpdateGameState(GameState.StagePlay);
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