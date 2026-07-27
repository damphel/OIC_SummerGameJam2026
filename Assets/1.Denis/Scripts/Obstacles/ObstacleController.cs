using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public enum ObstacleState
    {
        Idle,
        Alert,
        Catch
    }

    protected ObstacleState currentState;

    private void Update()
    {
        switch (currentState)
        {
            case ObstacleState.Idle:
                break;
            case ObstacleState.Alert:
                break;
            case ObstacleState.Catch:
                break;
            default:
                break;
        }
    }

    protected void DoOnIdle()
    {

    }
}
