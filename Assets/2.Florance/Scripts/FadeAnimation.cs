using UnityEngine;
using DG.Tweening;

public class FadeAnimation : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private Tween fadeTween;

  
    public void FadeOut(System.Action onComplete = null)
    {
       
        fadeTween?.Kill();
        fadeTween = fadeCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() => OnFadeOutComplete(onComplete));
    }

    private void OnFadeOutComplete(System.Action onComplete)
    {
        fadeCanvasGroup.blocksRaycasts = false;
        fadeCanvasGroup.interactable = false;
        gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    
    
    private void OnDestroy()
    {
        fadeTween?.Kill();
    }
}