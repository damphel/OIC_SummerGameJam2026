using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAudio : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private AudioSource audioSource;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        if (button != null)
        {
            button.onClick.AddListener(PlayAudio);
        }
    }

    void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }


}
