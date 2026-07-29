using UnityEngine;
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private int minIdle = 10;   
    [SerializeField] private int maxIdle = 50;   
    [SerializeField] private int MaxChance = 100; 
    [SerializeField] private float alertTime = 2f;
    [SerializeField] private float checkInterval = 1f;

    public enum ObstacleState
    {
        Idle,
        Alert,
        Catch
    }

    [SerializeField] private ObstacleState currentState = ObstacleState.Idle;
    private float timer;
    private int timesChecked;

    private void Start()
    {

        SetState(ObstacleState.Idle);
    }

    private void Update()
    {
        switch (currentState)
        {
            case ObstacleState.Idle:
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

                timesChecked = 0;
                timer= checkInterval;
                Debug.Log($"Idle Prob{minIdle}to{maxIdle}");
                //animation change
                break;

            case ObstacleState.Alert:
                timer = alertTime;
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

        timer = timer - Time.deltaTime;
        if (timer > 0) return;
        timer = checkInterval;
        float increasingProb = Mathf.Clamp01((float)timesChecked / MaxChance);
        float currentAlertChance = Mathf.Lerp(minIdle, maxIdle, increasingProb);

        int randomnum = Random.Range(1, 101);
        timesChecked++;

        Debug.Log($"number we have to “–‚½‚é {randomnum} / prob of the the@‚ ‚½‚é”Ô† {currentAlertChance:F1}% / {timesChecked}");
        if (randomnum <= currentAlertChance)
        {
            Debug.Log("“–‚½‚èIAlert‚ÉØ‚è‘Ö‚¦‚é");
            SetState(ObstacleState.Alert);
        }
    }

    private void UpdateAlertState()
    {
        timer -= Time.deltaTime;

        if (UIManager.Instance.ActionButton.isPressed)
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
