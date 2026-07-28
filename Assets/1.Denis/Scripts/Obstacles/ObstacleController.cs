using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    [SerializeField] ActionButton actionButton;
    [SerializeField] float Max, Min;

    float randomValue,prob;

    public enum ObstacleState
    {
        Idle,
        Alert,
        Catch
    }

    protected ObstacleState currentState=ObstacleState.Idle;
    private void Start()
    {
        randomValue = GenerateRandomValue();
        prob=Random.Range(Min, Max);
    }

    private void Update()
    {
        switch (currentState)
        {
            case ObstacleState.Idle:
                // To change to alert, it has to have like a random stuff, 
                //I dont have the access to the sceneController, so I will use the GameManager.Instance.CurrentScene.TimeRequieredToComplete as a reference for the random value.
                if (randomValue == prob) 
                {
                    Debug.Log("Changing to Alert state.");
                    currentState = ObstacleState.Alert;
                }
                 
                break;
            case ObstacleState.Alert:
                // To change to Idle again, you need to finish the animation
                // To change to Catch, after the animaion time finish, then
                // if the player is pressing the button, then... it catch him. (DoCatchStuffs)
                // but if not, just return to Idle
                ChangeStateConditions();
                break;

            case ObstacleState.Catch:
                // No Sensors, just a safer or a flag.
                break;
            default:
                break;
        }
    }

    public float GenerateRandomValue()
    {
        float a= Random.Range(0f, 100f) % 10; ;
        Debug.Log($"Random value: {a}");
        return a;
    }

    public void ChangeStateConditions()
    {
        if (actionButton.isPressed)
        {
            Debug.Log("Player pressed the button, changing to Catch state.");
            currentState = ObstacleState.Catch;
        }
        else
        {
            Debug.Log("Player did not press the button, returning to Idle state.");
            currentState = ObstacleState.Idle;
            GenerateRandomValue();
        }

    }
    protected void DoOnIdle()
    {

    }
}
