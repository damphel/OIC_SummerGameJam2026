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

    public ActionButton actionButton;


    public SceneController CurrentScene { get; private set; }
    public SceneController NextScene { get; private set; }

    // Actions
    public Action<GameState> onChangeState;

    // Getters and Setters  
    public GameState CurrentState { get; set; }

    private void OnEnable()
    {
        onChangeState += DoOnStateChange;
        actionButton.onHoldButton += DoOnActionButtonPressed;
        actionButton.onReleaseButton += DoOnActionButtonReleased;
    }


    private void OnDisable()
    {
        onChangeState -= DoOnStateChange;
        actionButton.onHoldButton -= DoOnActionButtonPressed;
        actionButton.onReleaseButton -= DoOnActionButtonReleased;
    }

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
        onChangeState?.Invoke(gameState);
    }

    public void InitializeScenes()
    {
        if (availableScenes == null || availableScenes.Count == 0) return;

        SceneController selectedScene = availableScenes[UnityEngine.Random.Range(0, availableScenes.Count)];
        CurrentScene = Instantiate(selectedScene, Vector3.zero, Quaternion.identity);

        CurrentScene.onCompleteScene -= CreateNextScene;
        CurrentScene.onCompleteScene += CreateNextScene;
    }

    public void CreateNextScene()
    {
        if (availableScenes == null || availableScenes.Count == 0) return;

        if (availableScenes.Count == 1)
        {
            NextScene = Instantiate(availableScenes[0], Vector3.right * availableScenes[0].SceneSize.x, Quaternion.identity);
            NextScene.DoOnSceneInstantiate();
            return;
        }

        SceneController selectedScene;

        do
        {
            selectedScene = availableScenes[UnityEngine.Random.Range(0, availableScenes.Count)];
        } while (CurrentScene != null && selectedScene.ID == CurrentScene.ID);

        Vector3 nextSceneInitPosition = Vector3.right * selectedScene.SceneSize.x;
        NextScene = Instantiate(selectedScene, nextSceneInitPosition, Quaternion.identity);
        NextScene.DoOnSceneInstantiate();
    }


    private void DoOnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Paused:
                break;
            case GameState.Waiting:
                break;
            case GameState.Playing:
                break;
            default:
                break;
        }
    }

    private void DoOnActionButtonPressed(float time)
    {
        CurrentScene.DoOnActionButtonPressed(time);
    }

    private void DoOnCurrentSceneProgressChange(float progress)
    {
        // update the bar visuals
        // do the player movement to Taget
    }

    private void DoOnActionButtonReleased()
    {
        CurrentScene.DoOnActionButtonReleased();
    }


    [ContextMenu("TEST CHANGE TO PLAY")]
    public void TESTFORCEPLAY()
    {
        ChangeState(GameState.Playing);
    }
}
