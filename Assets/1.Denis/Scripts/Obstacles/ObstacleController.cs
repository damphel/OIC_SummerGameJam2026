
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    
    [SerializeField] private ActionButton actionButton;
    [SerializeField] private float minIdleTime = 3f;
    [SerializeField] private float maxIdleTime = 7f;
    [SerializeField] private float alertDuration = 2f;

    public enum ObstacleState
    {
        Idle,
        Alert,
        Catch
    }

    [SerializeField] private ObstacleState currentState = ObstacleState.Idle;
    private float timer;

    private void Start()
    {
        
        SetState(ObstacleState.Idle);
    }

    private void Update()
    {
        switch (currentState)
        {
            case ObstacleState.Idle:
                // To change to alert, it has to have like a random stuff, 
                //I dont have the access to the sceneController, so I will use the GameManager.Instance.CurrentScene.TimeRequieredToComplete as a reference for the random value.
                UpdateIdleState();
                break;

            case ObstacleState.Alert:
                // To change to Idle again, you need to finish the animation
                // To change to Catch, after the animaion time finish, then
                // if the player is pressing the button, then... it catch him. (DoCatchStuffs)
                // but if not, just return to Idle
                UpdateAlertState();
                break;

            case ObstacleState.Catch:
                // No Sensors, just a safer or a flag.
                break;
        }
    }

    private void SetState(ObstacleState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case ObstacleState.Idle:
            
                timer = Random.Range(minIdleTime, maxIdleTime);
                Debug.Log($"In Idle State {timer:F1} seconds.");
                //animation change
                break;

            case ObstacleState.Alert:
                timer = alertDuration;
                Debug.Log("Checking for button");
                //animation change
                break;

            case ObstacleState.Catch:
                Debug.Log("Button Pressed");
                OnPlayerCaught();
                //animation change
                break;
        }
    }

    private void UpdateIdleState()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetState(ObstacleState.Alert);
        }
    }

    private void UpdateAlertState()
    {
        timer -= Time.deltaTime;

        
        if (actionButton.isPressed)
        {
            SetState(ObstacleState.Catch);
            return;
        }
        if (timer <= 0f)
        {
            Debug.Log(" Didn't see anything. Returning to sleep.");
            SetState(ObstacleState.Idle);
        }
    }

    private void OnPlayerCaught()
    {
        //animation change 
        //restart game
    }
}