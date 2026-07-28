using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel,GamePanel,EndingPanel;
    [SerializeField] Button PlayButton, EndButton, Restart;
    void Start()
    {
       Mainmenu();

    }

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(() => Game());
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(() => Game());
    }
    public void Mainmenu()
    {
        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
        EndingPanel.SetActive(false);
    }
    public void Game()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        EndingPanel.SetActive(false);
    }
    public void Ending()
    {
        MainMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        EndingPanel.SetActive(true);
    }
   
}
