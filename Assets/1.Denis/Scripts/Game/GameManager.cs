using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Waiting, // Cinematics, before and after playing
        Playing
    }

    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarningFormat("Accesing {0} before its Awake phase", typeof(GameManager).Name);
            }

            return _instance;
        }
    }
    #endregion

    //Å@ïœêî
    [SerializeField] List<SceneController> availableScenes;

    public SceneController CurrentScene { get; private set; }
    public SceneController NextScene { get; private set; }

    // Actions
    public Action<GameState> OnChangeState;

    // Getters and Setters  
    public GameState CurrentState { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this || FindObjectsByType<GameManager>(FindObjectsSortMode.InstanceID).Length > 1)
        {
            Debug.LogWarningFormat("Please make sure there is only one {0} in the scene", typeof(GameManager).Name);
            Destroy(this);
            return;
        }
        else
        {
            _instance = this;
            InitializeGame();
            InitializeScenes();
        }
    }

    public void InitializeGame()
    {
        CurrentState = GameState.Paused;
    }

    public void ChangeState(GameState gameState)
    {
        CurrentState = gameState;
        OnChangeState?.Invoke(gameState);
    }

    public void InitializeScenes()
    {
        SceneController selectedScene = availableScenes[UnityEngine.Random.Range(0, availableScenes.Count)];

        CurrentScene = Instantiate(selectedScene, Vector3.zero, Quaternion.identity); ;

    }

    public void ChangeScene()
    {
        SceneController selectedScene;

        do
        {
            selectedScene = availableScenes[UnityEngine.Random.Range(0, availableScenes.Count)];
        } while (selectedScene.ID == CurrentScene.ID);

        NextScene = selectedScene;
    }

    [ContextMenu("TEST CHANGE TO PLAY")]
    public void TESTFORCEPLAY()
    {
        ChangeState(GameState.Playing);
    }
}
