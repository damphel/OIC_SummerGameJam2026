using System;
using UnityEngine;
using DG.Tweening;

public class FadeAnimation : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private Tween fadeTween;

  
    public void FadeOut(Action onComplete = null)
    {
        fadeTween?.Kill();
        fadeTween = fadeCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() => DoOnFadeOutComplete(onComplete));
    }

    private void DoOnFadeOutComplete(Action onComplete)
    {
        fadeCanvasGroup.blocksRaycasts = false;
        fadeCanvasGroup.interactable = false;
        gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    public void FadeIn(Action onComplete = null, float delay = 0)
    {
        gameObject.SetActive(true);
        fadeTween?.Kill();
        fadeTween = fadeCanvasGroup.DOFade(1f, fadeDuration).From(0f).SetDelay(delay).OnComplete(() => DoOnFadeInComplete(onComplete));
    }

    private void DoOnFadeInComplete(Action onComplete)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        fadeCanvasGroup.interactable = true;
        onComplete?.Invoke();
    }


    private void OnDestroy()
    {
        fadeTween?.Kill();
    }
}