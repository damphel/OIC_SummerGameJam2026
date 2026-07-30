using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                Debug.LogWarningFormat("Accesing {0} before its Awake phase", typeof(UIManager).Name);
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] GameObject MainMenuPanel, GamePanel, EndingPanel;
    [SerializeField] FadeAnimation MainMenuFader,EndingMenuFader;
    [SerializeField] Button playButton, restartButton;
    [SerializeField] ActionButton actionButton;
    [SerializeField] Image endingImage;
    [SerializeField] AudioSource mainMusicSource;
    [SerializeField] AudioSource gameMusicSource;

    [SerializeField] Image progressFillBar;

    public ActionButton ActionButton { get => actionButton; }
    public Button PlayButton { get => playButton; }

    private void Awake()
    {
        if (_instance != null && _instance != this || FindObjectsByType<UIManager>(FindObjectsSortMode.InstanceID).Length > 1)
        {
            Debug.LogWarningFormat("Please make sure there is only one {0} in the scene", typeof(UIManager).Name);
            Destroy(this);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        ChangeToMainmenu();
    }

    private void OnEnable()
    {
        if (playButton) playButton.onClick.AddListener(ChangeToGame);
        if (restartButton) restartButton.onClick.AddListener(ChangeToMainmenu);
    }


    private void OnDisable()
    {
        if (playButton) playButton.onClick.RemoveListener(ChangeToGame);
        if (restartButton) restartButton.onClick.RemoveListener(ChangeToMainmenu);
    }

    public void DoOnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Paused:
                ActionButton.ThisButton.interactable = false;
                break;
            case GameState.Waiting:
                ActionButton.ThisButton.interactable = false;
                ActionButton.ResetPointerDownTimer();
                break;
            case GameState.Playing:
                ActionButton.ThisButton.interactable = true;
                break;
            case GameState.GameOver:
                // Show the Game Over Screen with a fade
                ChangeToEnding();
                ActionButton.ThisButton.interactable = false;
                break;
            default:
                break;
        }
    }

    public void ChangeToMainmenu()
    {
        Debug.Log("ChangeToMainmenu");
        GameManager.Instance.ChangeState(GameManager.GameState.Paused);
        mainMusicSource.Play();
        MainMenuPanel.SetActive(true);
        gameMusicSource.Stop();
        GamePanel.SetActive(false);
        EndingPanel.SetActive(false);
    }

    public void ChangeToGame()
    {
        Debug.Log("ChangeToGame");
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);

        mainMusicSource.Stop();

        MainMenuFader.FadeOut(
            () =>
            {
                gameMusicSource.Play();
                GamePanel.SetActive(true);
            });
        EndingPanel.SetActive(false);
    }

    public void ChangeToEnding()
    {
        Debug.Log("ChangeToEnding");
        gameMusicSource.Stop();
        GamePanel.SetActive(false);
        EndingMenuFader.FadeIn(() => EndingPanel.SetActive(true), 1f);
    }

    public void UpdateProgressFillAmmmount(float value)
    {
        progressFillBar.fillAmount = value;
    }

    public void UpdateEndingImageScreen(Sprite endingSprite)
    {
        endingImage.sprite = endingSprite;
    }
}