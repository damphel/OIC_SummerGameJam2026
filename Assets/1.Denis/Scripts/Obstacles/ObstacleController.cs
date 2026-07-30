using UnityEngine;
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private int minIdle = 10;   
    [SerializeField] private int maxIdle = 50;   
    [SerializeField] private int MaxChance = 100; 
    [SerializeField] private float alertTime = 2f;
    [SerializeField] private float checkInterval = 1f;

    [SerializeField] private Sprite endingImage;
    [SerializeField] private Animator obstacleAnimator;

    [Header("----- SFX/Music -----")]
    [SerializeField] AudioSource obstacleSource;
    [SerializeField] AudioSource obstacleIdleSource;
    [SerializeField] AudioClip obstacleIdleSFX;
    [SerializeField] AudioClip obstacleAlertSFX;
    [SerializeField] AudioClip obstacleCatchSFX;


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
                if(obstacleIdleSFX != null) obstacleIdleSource.PlayOneShot(obstacleIdleSFX);

                Debug.Log($"Idle Prob{minIdle}to{maxIdle}");

                //animation change
                if(obstacleAnimator != null)
                {
                    obstacleAnimator.ResetTrigger("Notice");
                    obstacleAnimator.ResetTrigger("Bark");
                    obstacleAnimator.SetTrigger("Idle");
                }

                break;

            case ObstacleState.Alert:
                timer = alertTime;
                if(obstacleAlertSFX != null) obstacleSource.PlayOneShot(obstacleAlertSFX);
                Debug.Log("Checking for button");
                //animation change
                if (obstacleAnimator != null)
                {
                    obstacleAnimator.ResetTrigger("Idle");
                    obstacleAnimator.ResetTrigger("Bark");
                    obstacleAnimator.SetTrigger("Notice");
                }
                break;

            case ObstacleState.Catch:
                Debug.Log("Button Pressed");
                if(obstacleCatchSFX != null) obstacleSource.PlayOneShot(obstacleCatchSFX);
                OnPlayerCaught();

                if (obstacleAnimator != null)
                {
                    obstacleAnimator.ResetTrigger("Notice");
                    obstacleAnimator.ResetTrigger("Idle");
                    obstacleAnimator.SetTrigger("Bark");
                }
                break;
        }
    }

    private void UpdateIdleState()
    {
        if (currentState != ObstacleState.Idle)
            return;

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        timer = timer - Time.deltaTime;

        if (timer > 0) return;
        timer = checkInterval;

        float increasingProb = Mathf.Clamp01((float)timesChecked / MaxChance);
        float currentAlertChance = Mathf.Lerp(minIdle, maxIdle, increasingProb);

        int randomnum = Random.Range(1, 101);
        timesChecked++;

        Debug.Log($"number we have to ìñÇΩÇÈ {randomnum} / prob of the theÅ@Ç†ÇΩÇÈî‘çÜ {currentAlertChance:F1}% / {timesChecked}");
        if (randomnum <= currentAlertChance)
        {
            Debug.Log("ìñÇΩÇËÅIAlertÇ…êÿÇËë÷Ç¶ÇÈ");
            SetState(ObstacleState.Alert);
        }
    }

    private void UpdateAlertState()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        Debug.Log($"Obstacle {this.gameObject.name} is in Alert State", this.gameObject);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = checkInterval;

            if (UIManager.Instance.ActionButton.isPressed)
            {
                if(endingImage != null) UIManager.Instance.UpdateEndingImageScreen(endingImage);
                SetState(ObstacleState.Catch);
                return;
            }
            else
            {
                Debug.Log(" Didn't see anything. Returning to sleep.");
                SetState(ObstacleState.Idle);
            }
        }
    }

    private void OnPlayerCaught()
    {
        Debug.Log("Player caught! Game Over.");
        GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        //animation change 
    }
}
