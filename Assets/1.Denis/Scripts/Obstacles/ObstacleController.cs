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
                // To change to alert, it has to have like a random stuff, 
                break;
            case ObstacleState.Alert:
                // To change to Idle again, you need to finish the animation
                // To change to Catch, after the animaion time finish, then
                // if the player is pressing the button, then... it catch him. (DoCatchStuffs)
                // but if not, just return to Idle
                break;
            case ObstacleState.Catch:
                // No Sensors, just a safer or a flag.
                break;
            default:
                break;
        }
    }

    protected void DoOnIdle()
    {

    }
}
