using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
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

    [SerializeField] GameObject MainMenuPanel,GamePanel,EndingPanel;
    [SerializeField] Button playButton, endButton, restartButton;
    [SerializeField] ActionButton actionButton;

    public ActionButton ActionButton { get => actionButton;}


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
        if (endButton) endButton.onClick.AddListener(ChangeToEnding);
        if (restartButton) restartButton.onClick.AddListener(ChangeToMainmenu);
    }

    private void OnDisable()
    {
        if (playButton) playButton.onClick.RemoveListener(ChangeToGame);
        if (endButton) endButton.onClick.RemoveListener(ChangeToEnding);
        if (restartButton) restartButton.onClick.RemoveListener(ChangeToMainmenu);
    }
    public void ChangeToMainmenu()
    {
        Debug.Log("ChangeToMainmenu");
        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        EndingPanel.SetActive(false);
    }
    public void ChangeToGame()
    {
        Debug.Log("ChangeToGame");
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        EndingPanel.SetActive(false);
    }
    public void ChangeToEnding()
    {
        Debug.Log("ChangeToEnding");
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        EndingPanel.SetActive(true);
    }
   
}
